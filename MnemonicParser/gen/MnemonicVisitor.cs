//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /Users/azuri_hirohinata/Desktop/ANTLR/StudyANTLR/MnemonicParser/Mnemonic.g4 by ANTLR 4.7.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace MnemonicParser.gen {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="MnemonicParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.2")]
[System.CLSCompliant(false)]
public interface IMnemonicVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.input"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInput([NotNull] MnemonicParser.InputContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.line_input"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLine_input([NotNull] MnemonicParser.Line_inputContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.mnemonic"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMnemonic([NotNull] MnemonicParser.MnemonicContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.line_comment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLine_comment([NotNull] MnemonicParser.Line_commentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.trivia"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTrivia([NotNull] MnemonicParser.TriviaContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.instruction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInstruction([NotNull] MnemonicParser.InstructionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.suffix"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSuffix([NotNull] MnemonicParser.SuffixContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.operand_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperand_list([NotNull] MnemonicParser.Operand_listContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.operand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperand([NotNull] MnemonicParser.OperandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.indexable_operand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIndexable_operand([NotNull] MnemonicParser.Indexable_operandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.relay_device"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRelay_device([NotNull] MnemonicParser.Relay_deviceContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.wordbit_device"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWordbit_device([NotNull] MnemonicParser.Wordbit_deviceContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.word_device"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWord_device([NotNull] MnemonicParser.Word_deviceContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.timer_device"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTimer_device([NotNull] MnemonicParser.Timer_deviceContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.counter_device"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCounter_device([NotNull] MnemonicParser.Counter_deviceContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.label"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLabel([NotNull] MnemonicParser.LabelContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteral([NotNull] MnemonicParser.LiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumber([NotNull] MnemonicParser.NumberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.dec_number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDec_number([NotNull] MnemonicParser.Dec_numberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.hex_number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHex_number([NotNull] MnemonicParser.Hex_numberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.real_number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReal_number([NotNull] MnemonicParser.Real_numberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.string"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitString([NotNull] MnemonicParser.StringContext context);
}
} // namespace MnemonicParser.gen
