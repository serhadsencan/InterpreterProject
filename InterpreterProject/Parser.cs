using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterProject
{
    public class Parser
    {
        public Token token;
        public string errorMessage;
        public void P()
        {

        }
        public void C()
        {

        }
        public void I()
        {

        }
        public void W()
        {

        }
        public void A()
        {

        }
        public void Ç()
        {

        }
        public void G()
        {

        }
        public void E()
        {

        }
        public void T()
        {


        }
        public void U()
        {

        }
        public void F()
        {

        }
        public string K()
        {

            return "";
        }
        public int R()
        {
            int result = 0;
            if (token.type ==TokenType.INTEGER)
            {

            }
            return result;
        }
        public void eat(TokenType tokenType)
        {
            if (token.type != tokenType)
                errorMessage += "\n Syntax error";
        }


    }
}
