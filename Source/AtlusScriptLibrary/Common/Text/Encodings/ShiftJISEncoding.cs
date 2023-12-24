﻿using System.Text;

namespace AtlusScriptLibrary.Common.Text.Encodings
{
#pragma warning disable S101 // Types should be named in PascalCase
    public static class ShiftJISEncoding
#pragma warning restore S101 // Types should be named in PascalCase
    {
        public static Encoding Instance
        {
            get
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                return Encoding.GetEncoding(932);
            }
        }
    }
}
