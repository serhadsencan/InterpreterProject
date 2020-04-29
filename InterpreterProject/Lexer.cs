using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterProject
{
    public class Lexer
    {
        public string text;
        public int position;
        public char currentChar;

        public Lexer(string text)
        {
            this.text = text;
            this.position = 0;
            this.currentChar=text[position];
        }

        // text'i ilerleterek sıradaki karakteri alır. Eğer text bittiyse karakteri null yapar.
        public void increasePos()
        {
            position += 1;
            if (position < text.Length)
            {
                currentChar = text[position];
            }
            else
            {
                currentChar = '\0';
            }
        }

        public Token getToken()
        {
            //  '\0' karakteri null demek 
            if (currentChar == '.')
            {
                // Eğer char . ise bu programın bittiği anlamına gelir.
                return new Token(TokenType.EOF,".");
            }
            else if (char.IsLetter(currentChar))
            {
                return new Token(TokenType.CHAR,currentChar.ToString());
            }
            else if (char.IsDigit(currentChar))
            {
                return new Token(TokenType.INTEGER, currentChar.ToString());
            }
            else if (currentChar=='+')
            {
                return new Token(TokenType.PLUS, "+");
            }
            else if (currentChar=='-')
            {
                return new Token(TokenType.MINUS, "-");
            }
            else if (currentChar=='*')
            {
                return new Token(TokenType.MUL ,"*");
            }
            else if (currentChar=='/')
            {
                return new Token(TokenType.DIV, "/");
            }
            else if (currentChar=='(')
            {
                return new Token(TokenType.LPAREN, "(");
            }
            else if (currentChar==')')
            {
                return new Token(TokenType.RPAREN, ")");
            }
            else if (currentChar=='>')
            {
                return new Token(TokenType.GTR, ">");
            }
            else if (currentChar=='<')
            {
                return new Token(TokenType.LSS, "<");
            }
            else if (currentChar=='=')
            {
                return new Token(TokenType.EQL, "=");
            }
            else if (currentChar==';')
            {
                return new Token(TokenType.SEMICOLON, ";");
            }
            else if (currentChar=='%')
            {
                return new Token(TokenType.MOD, "%");
            }
            else if (currentChar==':')
            {
                return new Token(TokenType.BECOMES, ":");
            }
            else if (currentChar=='?')
            {
                return new Token(TokenType.QMARK, "?");
            }
            else if (char.IsWhiteSpace(currentChar))
            {
                while (char.IsWhiteSpace(currentChar) && currentChar !='\0')
                {
                    increasePos();
                }
                position -=1;
                return new Token(TokenType.WHITESPACE, " ");
            }
            else
            {
                return new Token(TokenType.UNDEFINED,string.Empty);
            }
        }
    }
}
