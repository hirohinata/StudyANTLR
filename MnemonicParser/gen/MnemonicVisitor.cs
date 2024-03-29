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
	/// Visit a parse tree produced by the <c>indexableOperand</c>
	/// labeled alternative in <see cref="MnemonicParser.operand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIndexableOperand([NotNull] MnemonicParser.IndexableOperandContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>noneOperand</c>
	/// labeled alternative in <see cref="MnemonicParser.operand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNoneOperand([NotNull] MnemonicParser.NoneOperandContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>relayDeviceOperand</c>
	/// labeled alternative in <see cref="MnemonicParser.operand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRelayDeviceOperand([NotNull] MnemonicParser.RelayDeviceOperandContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>wordBitDeviceOperand</c>
	/// labeled alternative in <see cref="MnemonicParser.operand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWordBitDeviceOperand([NotNull] MnemonicParser.WordBitDeviceOperandContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>oldIndirectOperand</c>
	/// labeled alternative in <see cref="MnemonicParser.operand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOldIndirectOperand([NotNull] MnemonicParser.OldIndirectOperandContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>zDeviceOperand</c>
	/// labeled alternative in <see cref="MnemonicParser.operand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitZDeviceOperand([NotNull] MnemonicParser.ZDeviceOperandContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>wordDeviceOperand</c>
	/// labeled alternative in <see cref="MnemonicParser.indexable_operand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWordDeviceOperand([NotNull] MnemonicParser.WordDeviceOperandContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>localRelayDeviceOperand</c>
	/// labeled alternative in <see cref="MnemonicParser.indexable_operand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLocalRelayDeviceOperand([NotNull] MnemonicParser.LocalRelayDeviceOperandContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>relayDeviceOrLiteralOperand</c>
	/// labeled alternative in <see cref="MnemonicParser.indexable_operand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRelayDeviceOrLiteralOperand([NotNull] MnemonicParser.RelayDeviceOrLiteralOperandContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>labelOperand</c>
	/// labeled alternative in <see cref="MnemonicParser.indexable_operand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLabelOperand([NotNull] MnemonicParser.LabelOperandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MnemonicParser.indexed_operand"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIndexed_operand([NotNull] MnemonicParser.Indexed_operandContext context);
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
	/// Visit a parse tree produced by <see cref="MnemonicParser.dec_number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDec_number([NotNull] MnemonicParser.Dec_numberContext context);
}
} // namespace MnemonicParser.gen
