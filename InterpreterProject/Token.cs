using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterProject
{
    public enum TokenType
    {
        INTEGER,        // int 
        CHAR,           // Char
        PLUS,           // '+' Plus
        MINUS,          // '-' Minus
        MUL,            // '*' Multiply
        DIV,            // '/'  Division
        LPAREN,         // '(' left paranthesis
        RPAREN,         // ')' right paranthesis
        LBRACKET,       // '{' Left curly bracket
        RBRACKET,       // '}' right curly bracket
        GRT,            // '>' greater than 
        LSS,            // '<' lesser than      // büyük küçük olayı değilmiş bu print olarak çalışıyor.
        EQL,            // '=' Equal
        SEMICOLON,      // ';' semicolon
        MOD,            // '%' Mod
        BECOMES,        // ':' iki nokta 
        QMARK,          // '?' Question mark
        EXPO,           // '^' Exponent (üs)
        LSQUARE,        // '[' Sol köşeli parantez
        RSQUARE,        // ']' Sağ köşeli parantez
        EOF,            // end of function 
        WHITESPACE,
        UNDEFINED       // Bulamadığımız tokenler için
    }
    public class Token
    {
        public TokenType type;
        public string value;
        public int index;
        public Token(TokenType type, string value,int pos)
        {
            this.type = type;
            this.value = value;
            this.index = pos;
        }
    }
}
