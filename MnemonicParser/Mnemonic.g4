grammar Mnemonic;

input
    : line_input (EOL line_input)* EOL? EOF
    ;

line_input
    : mnemonic
    | line_comment
    | trivia
    ;
mnemonic
    : WS* instruction (WS+ operand_list)? trivia
    ;
line_comment
    : COMMENT
    ;
trivia
    : // empty
    | WS+
    | WS* COMMENT
    ;

instruction
    : name=(IDENTIFIER | IDENTIFIER_WITH_SYMBOL) suffix?
    ;
suffix
    : DOT IDENTIFIER     // 存在しないサフィックスは意味解析で弾く
    ;

operand_list
    : operand (WS+ operand)*
    ;
operand
    : indexable_operand COLON indexed_operand           #indexableOperand
    | indexable_operand                                 #noneOperand
    | AT? RELAY_DEVICE                                  #relayDeviceOperand
    | AT? device=(WORD_DEVICE | TM_DEVICE) DOT_NUMBER   #wordBitDeviceOperand
    | SHARP TM_DEVICE DOT_NUMBER?                       #oldIndirectOperand
    | Z_DEVICE                                          #zDeviceOperand
    | literal                                           #noneOperand
    ;
indexable_operand
    : ASTERISK? AT? device=(WORD_DEVICE | TM_DEVICE | TIMER_DEVICE | COUNTER_DEVICE)    #wordDeviceOperand
    | AT RAW_NUMBER                                     #localRelayDeviceOperand
    | RAW_NUMBER                                        #relayDeviceOrLiteralOperand
    | ASTERISK? label                                   #labelOperand
    ;
indexed_operand
    : Z_DEVICE
    | dec_number
    ;

label
    : IDENTIFIER
    | label DOT_NUMBER
    | label DOT IDENTIFIER
    | label OPEN_BRACKET operand (COMMA operand)* CLOSE_BRACKET
    ;

literal
    : dec_number
    | DEC_K_NUMBER
    | HEX_NUMBER
    | REAL_NUMBER
    | DOT_NUMBER
    | STRING
    | NULL
    ;
dec_number
    : DEC_SHARP_NUMBER
    | RAW_NUMBER
    ;

fragment A: [Aa];
fragment B: [Bb];
fragment C: [Cc];
fragment D: [Dd];
fragment E: [Ee];
fragment F: [Ff];
fragment G: [Gg];
fragment H: [Hh];
fragment I: [Ii];
fragment J: [Jj];
fragment K: [Kk];
fragment L: [Ll];
fragment M: [Mm];
fragment N: [Nn];
fragment O: [Oo];
fragment P: [Pp];
fragment Q: [Qq];
fragment R: [Rr];
fragment S: [Ss];
fragment T: [Tt];
fragment U: [Uu];
fragment V: [Vv];
fragment W: [Ww];
fragment X: [Xx];
fragment Y: [Yy];
fragment Z: [Zz];

fragment RELAY_DEVICE_TYPE: R | D R | M R | L R | B | C R;
fragment WORD_DEVICE_TYPE: D M | E M | F M | W | Z F | C M;
fragment TIMER_DEVICE_TYPE: T;
fragment COUNTER_DEVICE_TYPE: C;
fragment Z_DEVICE_TYPE: Z;

fragment NONDIGIT
    : [a-zA-Z_]
    ;
fragment DIGIT
    : [0-9]
    ;
fragment NONZERODIGIT
    : [1-9]
    ;
fragment HEXDIGIT
    : [0-9a-fA-F]
    ;
fragment SYMBOL
    : [!#$%&'*+\-/<=>?@[\]^_`|~]    // 利用されていない記号もできるだけ追加（拡張用）~["(),.:;\\{}]
    ;
fragment SIGN
    : [+\-]
    ;
fragment DEC_SHARP_PREFIX
    : '#'
    ;
fragment DEC_K_PREFIX
    : K
    ;
fragment DEC_PREFIX
    : DEC_SHARP_PREFIX
    | DEC_K_PREFIX
    ;
fragment HEX_PREFIX
    : '$'
    | H
    ;
fragment REAL_NUMBER_PART
    : DIGIT+ (DOT DIGIT*)?
    | DOT DIGIT+
    ;
fragment REAL_EXPONENT_PART
    : E SIGN? DIGIT+
    ;

RELAY_DEVICE
    : RELAY_DEVICE_TYPE (DIGIT+ '_')? DIGIT* ('0'? DIGIT | '1' [0-5])
    ;
WORD_DEVICE
    : WORD_DEVICE_TYPE DIGIT+
    ;
TIMER_DEVICE
    : TIMER_DEVICE_TYPE DIGIT+
    ;
COUNTER_DEVICE
    : COUNTER_DEVICE_TYPE DIGIT+
    ;
Z_DEVICE
    : Z_DEVICE_TYPE DIGIT+
    ;
TM_DEVICE
    : T M DIGIT DOT_NUMBER?
    ;

RAW_NUMBER
    : DIGIT+
    ;
DOT_NUMBER
    : DOT DIGIT+
    ;
DEC_SHARP_NUMBER
    : DEC_SHARP_PREFIX? SIGN? DIGIT+
    ;
DEC_K_NUMBER
    : DEC_K_PREFIX SIGN? DIGIT+
    ;
HEX_NUMBER
    : HEX_PREFIX HEXDIGIT+
    ;
REAL_NUMBER
    : DEC_PREFIX? SIGN? REAL_NUMBER_PART REAL_EXPONENT_PART?
    ;
STRING
    : '"' (~["\r\n] | '""')* '"'
    ;

IDENTIFIER
    : NONDIGIT (NONDIGIT | DIGIT)*
    ;
IDENTIFIER_WITH_SYMBOL
    : IDENTIFIER SYMBOL+
    ;

NULL: '???';
AT: '@';
ASTERISK: '*';
SHARP: '#';
DOT: '.';
COMMA: ',';
COLON: ':';
OPEN_BRACKET: '[';
CLOSE_BRACKET: ']';

EOL: '\r' '\n'? | '\n';
WS: [ \t]+;
COMMENT: ';' ~[\r\n]*;
