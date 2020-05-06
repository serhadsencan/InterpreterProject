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
        READ,            // '>' Bir input okumak isteğinimizde kullanıyoruz
        WRITE,            // '<' Bir input yazdırmak istediğimizde kullanıyoruz
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
        public int Line;
        public Token(TokenType type, string value,int pos,int line)
        {
            this.type = type;
            this.value = value;
            this.index = pos;
            this.Line = line;
        }
    }
}
