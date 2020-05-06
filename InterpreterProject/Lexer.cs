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
        public int Line;

        public Lexer(string text)
        {
            this.text = text;
            this.position = -1;
            this.Line = 1;
            if (position!=-1)
                this.currentChar=text[position];
            else
                this.currentChar=text[0];
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

        public void GotoIndex(int index)
        {
            position=index;
            if (position < text.Length)
            {
                currentChar = text[position];
            }
            else
            {
                currentChar = '\0';
            }
        }
        public Token GetEndofFunction()
        {
            return new Token(TokenType.EOF,".",text.Length,Line);
        }
        public Token getToken()
        {
            increasePos();

            while (currentChar=='\n' || char.IsWhiteSpace(currentChar))
            {
                if (currentChar == '\n')
                {
                    Line += 1;
                    increasePos();
                }
                else
                {
                    increasePos();
                }
            }
            if (currentChar == '.')
            {
                return new Token(TokenType.EOF,".",position, Line);
            }
            else if (char.IsLetter(currentChar))
            {
                return new Token(TokenType.CHAR,currentChar.ToString(), position, Line);
            }
            else if (char.IsDigit(currentChar))
            {
                return new Token(TokenType.INTEGER, currentChar.ToString(), position, Line);
            }
            else if (currentChar=='+')
            {
                return new Token(TokenType.PLUS, "+", position, Line);
            }
            else if (currentChar=='-')
            {
                return new Token(TokenType.MINUS, "-", position,Line);
            }
            else if (currentChar=='*')
            {
                return new Token(TokenType.MUL ,"*", position, Line);
            }
            else if (currentChar=='/')
            {
                return new Token(TokenType.DIV, "/", position, Line);
            }
            else if (currentChar=='(')
            {
                return new Token(TokenType.LPAREN, "(", position, Line);
            }
            else if (currentChar==')')
            {
                return new Token(TokenType.RPAREN, ")", position, Line);
            }
            else if (currentChar == '{')
            {
                return new Token(TokenType.LBRACKET, "{", position, Line);
            }
            else if (currentChar == '}')
            {
                return new Token(TokenType.RBRACKET, "}", position, Line);
            }
            else if (currentChar=='>')
            {
                return new Token(TokenType.READ, ">", position, Line);
            }
            else if (currentChar=='<')
            {
                return new Token(TokenType.WRITE, "<", position, Line);
            }
            else if (currentChar=='=')
            {
                return new Token(TokenType.EQL, "=", position, Line);
            }
            else if (currentChar==';')
            {
                return new Token(TokenType.SEMICOLON, ";", position, Line);
            }
            else if (currentChar=='%')
            {
                return new Token(TokenType.MOD, "%", position, Line);
            }
            else if (currentChar==':')
            {
                return new Token(TokenType.BECOMES, ":", position, Line);
            }
            else if (currentChar=='?')
            {
                return new Token(TokenType.QMARK, "?", position, Line);
            }
            else if (currentChar == '^')
            {
                return new Token(TokenType.EXPO, "^", position, Line);
            }
            else if (currentChar == '[')
            {
                return new Token(TokenType.LSQUARE, "[", position, Line);
            }
            else if (currentChar == ']')
            {
                return new Token(TokenType.RSQUARE, "]", position, Line);
            }
            else if (currentChar == '\0')
            {
                return new Token(TokenType.EOF, ".", position, Line);
            }
            else
            {
                throw new Exception("Unknown character at: "+ position.ToString());
            }
        }
    }
}
