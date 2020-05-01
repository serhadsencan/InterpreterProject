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
                return new Token(TokenType.EOF,".",position);
            }
            else if (char.IsLetter(currentChar))
            {
                return new Token(TokenType.CHAR,currentChar.ToString(), position);
            }
            else if (char.IsDigit(currentChar))
            {
                return new Token(TokenType.INTEGER, currentChar.ToString(), position);
            }
            else if (currentChar=='+')
            {
                return new Token(TokenType.PLUS, "+", position);
            }
            else if (currentChar=='-')
            {
                return new Token(TokenType.MINUS, "-", position);
            }
            else if (currentChar=='*')
            {
                return new Token(TokenType.MUL ,"*", position);
            }
            else if (currentChar=='/')
            {
                return new Token(TokenType.DIV, "/", position);
            }
            else if (currentChar=='(')
            {
                return new Token(TokenType.LPAREN, "(", position);
            }
            else if (currentChar==')')
            {
                return new Token(TokenType.RPAREN, ")", position);
            }
            else if (currentChar == '{')
            {
                return new Token(TokenType.RPAREN, ")", position);
            }
            else if (currentChar == '{')
            {
                return new Token(TokenType.RPAREN, ")", position);
            }
            else if (currentChar=='>')
            {
                return new Token(TokenType.GRT, ">", position);
            }
            else if (currentChar=='<')
            {
                return new Token(TokenType.LSS, "<", position);
            }
            else if (currentChar=='=')
            {
                return new Token(TokenType.EQL, "=", position);
            }
            else if (currentChar==';')
            {
                return new Token(TokenType.SEMICOLON, ";", position);
            }
            else if (currentChar=='%')
            {
                return new Token(TokenType.MOD, "%", position);
            }
            else if (currentChar==':')
            {
                return new Token(TokenType.BECOMES, ":", position);
            }
            else if (currentChar=='?')
            {
                return new Token(TokenType.QMARK, "?", position);
            }
            else if (currentChar == '^')
            {
                return new Token(TokenType.EXPO, "^", position);
            }
            else if (currentChar == '[')
            {
                return new Token(TokenType.LSQUARE, "[", position);
            }
            else if (currentChar == ']')
            {
                return new Token(TokenType.RSQUARE, "]", position);
            }
            // Lexer'a text verirken tüm white space 'i siliyoruz gerekirse tekrardan açılır
            //else if (char.IsWhiteSpace(currentChar))
            //{
            //    while (char.IsWhiteSpace(currentChar) && currentChar !='\0')
            //    {
            //        increasePos();
            //    }
            //    position -=1;
            //    return new Token(TokenType.WHITESPACE, " ");
            //}
            else
            {
                throw new Exception("Unknown character at: "+ position.ToString());
            }
        }
    }
}
