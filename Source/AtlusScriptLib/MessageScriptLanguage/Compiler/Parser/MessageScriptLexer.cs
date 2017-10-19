//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.4
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from D:\Users\smart\Documents\Visual Studio 2017\Projects\AtlusScriptToolchain\Source\AtlusScriptLib\MessageScriptLanguage\Parser\MessageScriptLexer.g4 by ANTLR 4.6.4

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace AtlusScriptLib.MessageScriptLanguage.Compiler.Parser {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.4")]
[System.CLSCompliant(false)]
public partial class MessageScriptLexer : Lexer {
	public const int
		OpenCode=1, CloseText=2, Text=3, MessageDialogTagId=4, SelectionDialogTagId=5, 
		CloseCode=6, OpenText=7, IntLiteral=8, Identifier=9, Whitespace=10;
	public const int MessageScriptCode = 1;
	public static string[] modeNames = {
		"DEFAULT_MODE", "MessageScriptCode"
	};

	public static readonly string[] ruleNames = {
		"OpenCode", "CloseText", "Text", "MessageDialogTagId", "SelectionDialogTagId", 
		"CloseCode", "OpenText", "IntLiteral", "Identifier", "DecIntLiteral", 
		"HexIntLiteral", "Letter", "Digit", "HexDigit", "HexLiteralPrefix", "Sign", 
		"Whitespace"
	};


	public MessageScriptLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, null, null, null, "'dlg'", "'sel'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "OpenCode", "CloseText", "Text", "MessageDialogTagId", "SelectionDialogTagId", 
		"CloseCode", "OpenText", "IntLiteral", "Identifier", "Whitespace"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[System.Obsolete("Use Vocabulary instead.")]
	public static readonly string[] tokenNames = GenerateTokenNames(DefaultVocabulary, _SymbolicNames.Length);

	private static string[] GenerateTokenNames(IVocabulary vocabulary, int length) {
		string[] tokenNames = new string[length];
		for (int i = 0; i < tokenNames.Length; i++) {
			tokenNames[i] = vocabulary.GetLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = vocabulary.GetSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}

		return tokenNames;
	}

	[System.Obsolete("Use IRecognizer.Vocabulary instead.")]
	public override string[] TokenNames
	{
		get
		{
			return tokenNames;
		}
	}

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "MessageScriptLexer.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\fp\b\x1\b\x1\x4"+
		"\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b"+
		"\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4"+
		"\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x3\x2\x3\x2\x3\x2\x3\x2\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x3\x4\x6\x4\x30\n\x4\r\x4\xE\x4\x31\x3\x5\x3\x5\x3\x5"+
		"\x3\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3\b"+
		"\x3\t\x3\t\x5\t\x46\n\t\x3\n\x3\n\x3\n\x6\nK\n\n\r\n\xE\nL\x3\v\x5\vP"+
		"\n\v\x3\v\x6\vS\n\v\r\v\xE\vT\x3\f\x5\fX\n\f\x3\f\x3\f\x6\f\\\n\f\r\f"+
		"\xE\f]\x3\r\x3\r\x3\xE\x3\xE\x3\xF\x3\xF\x5\xF\x66\n\xF\x3\x10\x3\x10"+
		"\x3\x10\x3\x11\x3\x11\x3\x12\x3\x12\x3\x12\x3\x12\x2\x2\x2\x13\x4\x2\x3"+
		"\x6\x2\x4\b\x2\x5\n\x2\x6\f\x2\a\xE\x2\b\x10\x2\t\x12\x2\n\x14\x2\v\x16"+
		"\x2\x2\x18\x2\x2\x1A\x2\x2\x1C\x2\x2\x1E\x2\x2 \x2\x2\"\x2\x2$\x2\f\x4"+
		"\x2\x3\t\x4\x2]]__\x4\x2\x43\\\x63|\x3\x2\x32;\x4\x2\x43H\x63h\x4\x2Z"+
		"Zzz\x4\x2--//\x5\x2\v\f\xF\xF\"\"q\x2\x4\x3\x2\x2\x2\x2\x6\x3\x2\x2\x2"+
		"\x2\b\x3\x2\x2\x2\x3\n\x3\x2\x2\x2\x3\f\x3\x2\x2\x2\x3\xE\x3\x2\x2\x2"+
		"\x3\x10\x3\x2\x2\x2\x3\x12\x3\x2\x2\x2\x3\x14\x3\x2\x2\x2\x3$\x3\x2\x2"+
		"\x2\x4&\x3\x2\x2\x2\x6*\x3\x2\x2\x2\b/\x3\x2\x2\x2\n\x33\x3\x2\x2\x2\f"+
		"\x37\x3\x2\x2\x2\xE;\x3\x2\x2\x2\x10?\x3\x2\x2\x2\x12\x45\x3\x2\x2\x2"+
		"\x14J\x3\x2\x2\x2\x16O\x3\x2\x2\x2\x18W\x3\x2\x2\x2\x1A_\x3\x2\x2\x2\x1C"+
		"\x61\x3\x2\x2\x2\x1E\x65\x3\x2\x2\x2 g\x3\x2\x2\x2\"j\x3\x2\x2\x2$l\x3"+
		"\x2\x2\x2&\'\a]\x2\x2\'(\x3\x2\x2\x2()\b\x2\x2\x2)\x5\x3\x2\x2\x2*+\a"+
		"_\x2\x2+,\x3\x2\x2\x2,-\b\x3\x2\x2-\a\x3\x2\x2\x2.\x30\n\x2\x2\x2/.\x3"+
		"\x2\x2\x2\x30\x31\x3\x2\x2\x2\x31/\x3\x2\x2\x2\x31\x32\x3\x2\x2\x2\x32"+
		"\t\x3\x2\x2\x2\x33\x34\a\x66\x2\x2\x34\x35\an\x2\x2\x35\x36\ai\x2\x2\x36"+
		"\v\x3\x2\x2\x2\x37\x38\au\x2\x2\x38\x39\ag\x2\x2\x39:\an\x2\x2:\r\x3\x2"+
		"\x2\x2;<\a_\x2\x2<=\x3\x2\x2\x2=>\b\a\x3\x2>\xF\x3\x2\x2\x2?@\a]\x2\x2"+
		"@\x41\x3\x2\x2\x2\x41\x42\b\b\x3\x2\x42\x11\x3\x2\x2\x2\x43\x46\x5\x16"+
		"\v\x2\x44\x46\x5\x18\f\x2\x45\x43\x3\x2\x2\x2\x45\x44\x3\x2\x2\x2\x46"+
		"\x13\x3\x2\x2\x2GK\x5\x1A\r\x2HK\x5\x1C\xE\x2IK\a\x61\x2\x2JG\x3\x2\x2"+
		"\x2JH\x3\x2\x2\x2JI\x3\x2\x2\x2KL\x3\x2\x2\x2LJ\x3\x2\x2\x2LM\x3\x2\x2"+
		"\x2M\x15\x3\x2\x2\x2NP\x5\"\x11\x2ON\x3\x2\x2\x2OP\x3\x2\x2\x2PR\x3\x2"+
		"\x2\x2QS\x5\x1C\xE\x2RQ\x3\x2\x2\x2ST\x3\x2\x2\x2TR\x3\x2\x2\x2TU\x3\x2"+
		"\x2\x2U\x17\x3\x2\x2\x2VX\x5\"\x11\x2WV\x3\x2\x2\x2WX\x3\x2\x2\x2XY\x3"+
		"\x2\x2\x2Y[\x5 \x10\x2Z\\\x5\x1E\xF\x2[Z\x3\x2\x2\x2\\]\x3\x2\x2\x2]["+
		"\x3\x2\x2\x2]^\x3\x2\x2\x2^\x19\x3\x2\x2\x2_`\t\x3\x2\x2`\x1B\x3\x2\x2"+
		"\x2\x61\x62\t\x4\x2\x2\x62\x1D\x3\x2\x2\x2\x63\x66\x5\x1C\xE\x2\x64\x66"+
		"\t\x5\x2\x2\x65\x63\x3\x2\x2\x2\x65\x64\x3\x2\x2\x2\x66\x1F\x3\x2\x2\x2"+
		"gh\a\x32\x2\x2hi\t\x6\x2\x2i!\x3\x2\x2\x2jk\t\a\x2\x2k#\x3\x2\x2\x2lm"+
		"\t\b\x2\x2mn\x3\x2\x2\x2no\b\x12\x4\x2o%\x3\x2\x2\x2\r\x2\x3\x31\x45J"+
		"LOTW]\x65\x5\a\x3\x2\x6\x2\x2\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace AtlusScriptLib.MessageScriptLanguage.Compiler.Parser