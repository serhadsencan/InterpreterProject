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
            while (currentChar != '\0')
            {
                if (currentChar == '.')
                {
                    // Eğer char . ise bu programın bittiği anlamına gelir.

                }
                else if (char.IsDigit(currentChar))
                {

                }
                else if (currentChar=='+')
                {

                }
                else if (currentChar=='-')
                {

                }
                else if (currentChar=='*')
                {

                }
                else if (currentChar=='/')
                {

                }
                else if (currentChar=='(')
                {

                }
                else if (currentChar==')')
                {

                }
                else if (currentChar=='>')
                {

                }
                else if (currentChar=='<')
                {

                }
                else if (currentChar=='=')
                {

                }
                else if (currentChar==';')
                {

                }
                else if (currentChar=='%')
                {

                }
                else if (currentChar==':')
                {

                }
                else if (currentChar=='?')
                {

                }
                else if (char.IsWhiteSpace(currentChar))
                {
                    while (char.IsWhiteSpace(currentChar) && currentChar !='\0')
                    {
                        increasePos();
                    }
                }
            }

            return new Token(TokenType.DIV,"");
        }
    }
}
