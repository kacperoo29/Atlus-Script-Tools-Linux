﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using AtlusScriptLibrary.Common.IO;

namespace AtlusScriptLibrary.MessageScriptLanguage.BinaryModel.IO
{
    public sealed class MessageScriptBinaryReader : IDisposable
    {
        private bool mDisposed;
        private readonly long mPositionBase;
        private readonly EndianBinaryReader mReader;
        private BinaryFormatVersion mVersion;

        public MessageScriptBinaryReader(Stream stream, BinaryFormatVersion version, bool leaveOpen = false)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            mPositionBase = stream.Position;
            mReader = new EndianBinaryReader(stream, Encoding.GetEncoding(932), leaveOpen, version.HasFlag(BinaryFormatVersion.BigEndian) ? Endianness.BigEndian : Endianness.LittleEndian);
            mVersion = version;
        }

        public MessageScriptBinary ReadBinary()
        {
            var binary = new MessageScriptBinary
            {
                mHeader = ReadHeader()
            };

            binary.mDialogHeaders = ReadDialogHeaders(binary.mHeader.DialogCount);
            binary.mSpeakerTableHeader = ReadSpeakerTableHeader();
            binary.mFormatVersion = mVersion;

            return binary;
        }

        public BinaryHeader ReadHeader()
        {
            var header = new BinaryHeader();

            // Check if the stream isn't too small to be a proper file
            if (mReader.BaseStreamLength < BinaryHeader.SIZE)
            {
                throw new InvalidDataException("Stream is too small to be valid");
            }

            header.FileType = mReader.ReadByte();
            header.Format = mReader.ReadByte();
            header.UserId = mReader.ReadInt16();
            header.FileSize = mReader.ReadInt32();
            header.Magic = mReader.ReadBytes(4);
            header.ExtSize = mReader.ReadInt32();
            header.RelocationTable.Offset = mReader.ReadInt32();
            header.RelocationTableSize = mReader.ReadInt32();
            header.DialogCount = mReader.ReadInt32();
            header.IsRelocated = mReader.ReadInt16() != 0;
            header.Version = mReader.ReadInt16();

            // swap endianness
            if (header.Magic.SequenceEqual(BinaryHeader.MAGIC_V1) || header.Magic.SequenceEqual(BinaryHeader.MAGIC_V0))
            {
                if (mVersion.HasFlag(BinaryFormatVersion.BigEndian))
                {
                    SwapHeader(ref header);
                    mReader.Endianness = Endianness.LittleEndian;
                }

                mVersion = BinaryFormatVersion.Version1;
            }
            else if (header.Magic.SequenceEqual(BinaryHeader.MAGIC_V1_BE))
            {
                if (!mVersion.HasFlag(BinaryFormatVersion.BigEndian))
                {
                    SwapHeader(ref header);
                    mReader.Endianness = Endianness.BigEndian;
                }

                mVersion = BinaryFormatVersion.Version1BigEndian;
            }
            else
            {
                throw new InvalidDataException("Header magic value does not match");
            }

            if (header.RelocationTable.Offset != 0)
            {
                mReader.EnqueuePositionAndSeekBegin(mPositionBase + header.RelocationTable.Offset);
                header.RelocationTable.Value = mReader.ReadBytes(header.RelocationTableSize);
                mReader.SeekBeginToDequedPosition();
            }

            return header;
        }

        private void SwapHeader(ref BinaryHeader header)
        {
            EndiannessHelper.Swap(ref header.UserId);
            EndiannessHelper.Swap(ref header.FileSize);
            EndiannessHelper.Swap(ref header.ExtSize);
            EndiannessHelper.Swap(ref header.RelocationTable.Offset);
            EndiannessHelper.Swap(ref header.RelocationTableSize);
            EndiannessHelper.Swap(ref header.DialogCount);
            EndiannessHelper.Swap(ref header.Version);
        }

        public BinaryDialogHeader[] ReadDialogHeaders(int count)
        {
            BinaryDialogHeader[] headers = new BinaryDialogHeader[count];

            for (int i = 0; i < headers.Length; i++)
            {
                ref var header = ref headers[i];
                header.Kind = (BinaryDialogKind)mReader.ReadInt32();
                header.Data.Offset = mReader.ReadInt32();

                if (header.Data.Offset != 0)
                    header.Data.Value = ReadDialog(header.Kind, header.Data.Offset);
            }

            return headers;
        }

        public BinarySpeakerTableHeader ReadSpeakerTableHeader()
        {
            BinarySpeakerTableHeader header;

            header.SpeakerNameArray.Offset = mReader.ReadInt32();
            header.SpeakerCount = mReader.ReadInt32();
            header.ExtDataOffset = mReader.ReadInt32();
            header.Reserved = mReader.ReadInt32();

            if (header.SpeakerNameArray.Offset != 0)
                header.SpeakerNameArray.Value = ReadSpeakerNames(header.SpeakerNameArray.Offset, header.SpeakerCount);
            else
                header.SpeakerNameArray.Value = null;

            if (header.ExtDataOffset != 0)
                Debug.WriteLine($"{nameof(BinarySpeakerTableHeader)}.{nameof(header.ExtDataOffset)} = {header.ExtDataOffset}");

            if (header.Reserved != 0)
                Debug.WriteLine($"{nameof(BinarySpeakerTableHeader)}.{nameof(header.Reserved)} = {header.Reserved}");

            return header;
        }

        public OffsetTo<List<byte>>[] ReadSpeakerNames(int address, int count)
        {
            mReader.SeekBegin(mPositionBase + BinaryHeader.SIZE + address);

            var speakerNameAddresses = mReader.ReadInt32s(count);
            var speakerNames = new OffsetTo<List<byte>>[count];

            for (int i = 0; i < speakerNameAddresses.Length; i++)
            {
                ref int speakerNameAddress = ref speakerNameAddresses[i];

                mReader.SeekBegin(mPositionBase + BinaryHeader.SIZE + speakerNameAddress);
                var bytes = new List<byte>();
                while (true)
                {
                    byte b = mReader.ReadByte();
                    if (b == 0)
                        break;

                    bytes.Add(b);
                }

                speakerNames[i] = new OffsetTo<List<byte>>(speakerNameAddress, bytes);
            }

            return speakerNames;
        }

        private object ReadDialog(BinaryDialogKind type, int address)
        {
            object dialog;

            mReader.EnqueuePositionAndSeekBegin(mPositionBase + BinaryHeader.SIZE + address);

            switch (type)
            {
                case BinaryDialogKind.Message:
                    dialog = ReadMessageDialog();
                    break;

                case BinaryDialogKind.Selection:
                    dialog = ReadSelectionDialog();
                    break;

                default:
                    throw new InvalidDataException($"Unknown message type: {type}");
            }

            mReader.SeekBeginToDequedPosition();

            return dialog;
        }

        public BinaryMessageDialog ReadMessageDialog()
        {
            BinaryMessageDialog message;

            message.Name = mReader.ReadString(StringBinaryFormat.FixedLength, BinaryMessageDialog.IDENTIFIER_LENGTH);
            message.PageCount = mReader.ReadInt16();
            message.SpeakerId = mReader.ReadUInt16();

            if (message.PageCount > 0)
            {
                message.PageStartAddresses = mReader.ReadInt32s(message.PageCount);
                message.TextBufferSize = mReader.ReadInt32();
                message.TextBuffer = mReader.ReadBytes(message.TextBufferSize);
            }
            else
            {
                message.PageStartAddresses = null;
                message.TextBufferSize = 0;
                message.TextBuffer = null;
            }

            return message;
        }

        public BinarySelectionDialog ReadSelectionDialog()
        {
            BinarySelectionDialog message;

            message.Name = mReader.ReadString(StringBinaryFormat.FixedLength, BinaryMessageDialog.IDENTIFIER_LENGTH);
            message.Ext = mReader.ReadInt16();
            message.OptionCount = mReader.ReadInt16();
            message.Pattern = (BinarySelectionDialogPattern)mReader.ReadInt16();
            message.Reserved = mReader.ReadInt16();
            message.OptionStartAddresses = mReader.ReadInt32s(message.OptionCount);
            message.TextBufferSize = mReader.ReadInt32();
            message.TextBuffer = mReader.ReadBytes(message.TextBufferSize);

            if (message.Ext != 0)
                Debug.WriteLine($"{nameof(BinarySelectionDialog)}.{nameof(message.Ext)} = {message.Ext}");

            if (message.Pattern != 0)
                Debug.WriteLine($"{nameof(BinarySelectionDialog)}.{nameof(message.Pattern)} = {message.Pattern}");

            if (message.Reserved != 0)
                Debug.WriteLine($"{nameof(BinarySelectionDialog)}.{nameof(message.Reserved)} = {message.Reserved}");

            return message;
        }

        public void Dispose()
        {
            if (mDisposed)
                return;

            mReader.Dispose();

            mDisposed = true;
        }
    }
}
