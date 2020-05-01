using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterProject
{
    // To - Do : 
        // token type == olanlar != e çevirilicek
        // while komutu implemente edilecek,
        // 0 yada daha fazla komutu implemente edilecek
        // if içerisinde position eksiltilebilir orada bir şey yapmak gerekiyor gibi.
        // if içinde eğer ':' ifadesi yoksa else yapmıyoruz sadece if bırakıyoruz. Nasıl yapılacak çözmek lazım.

    public class Parser
    {
        public Token token;
        public string errorMessage;
        public Lexer lexer;
        public bool isErrorRaised;
        public string outputText="";
        public Dictionary<string, int> intVariables;
        public Dictionary<string, string> stringVariables;
        public Parser()
        {
            isErrorRaised = false;
        }
        // Program
        public void P()
        {
            if (!isErrorRaised)
            {
                // bir yada daha fazla komutu
                if (token.type == TokenType.EOF)
                {
                    // program biter.
                }
            }
        }
        // Cümle
        public void C()
        {
            if (!isErrorRaised)
            {
                if (token.type == TokenType.LSQUARE)
                {
                    I();
                }
                else if (token.type == TokenType.LBRACKET)
                {
                    W();
                }
                else if (token.type == TokenType.CHAR)
                {
                    A();
                }
                else if (token.type == TokenType.LSS)
                {
                    Ç();
                }
                else if (token.type==TokenType.GRT)
                {
                    G();
                }
            }
        }
        // If cümlesi
        public void I()
        {
            int statement = 0;

            if (!isErrorRaised)
            {
                if(token.type != TokenType.LSQUARE)
                {
                    isErrorRaised = true;
                    errorMessage += "Syntax error at: " + lexer.position + "  Char : '[' is missing";
                }
                token = lexer.getToken();
                statement = E().Value;
                if (token.type != TokenType.QMARK)
                {
                    isErrorRaised = true;
                    errorMessage += "Syntax error at: " + lexer.position + "  Char : '?' is missing";
                }

                if (token.type != TokenType.QMARK)
                {
                    isErrorRaised = true;
                    errorMessage += "Syntax error at: " + lexer.position + "  Char : '?' is missing";
                }
                if (statement != 0)
                {
                    C();
                }
                else
                {
                    C();
                }

            }
        }
        // While döngüsü
        public void W()
        {
            int turn=0;
            if (!isErrorRaised)
            {
                if (token.type != TokenType.LBRACKET)
                {
                    isErrorRaised = true;
                    errorMessage += "Syntax error at: " + lexer.position + "  Char : '{' is missing";
                }
                token = lexer.getToken();
                turn = E().Value;
                if (token.type != TokenType.QMARK)
                {
                    isErrorRaised = true;
                    errorMessage += "Syntax error at: " + lexer.position + "  Char : '?' is missing";
                }
                token = lexer.getToken();

                C();
                // buraya bir yada daha fazla c() fonksiyonu gelicek.
                // Ayrıca while E() eklenicek
            }
        }
        // Atama cümlesi
        public void A()
        {
            string name="";
            int intValue=0;

            if (!isErrorRaised)
            {
                name = K().Value;

                if (token.type != TokenType.EQL)
                {
                    isErrorRaised = true;
                    errorMessage += "Syntax error at: " + lexer.position+"  Char : '=' is missing";
                }
                token = lexer.getToken();
                
                intValue = E().Value;

                if (token.type != TokenType.SEMICOLON)
                {
                    isErrorRaised = true;
                    errorMessage += "Syntax error at: " + lexer.position + "  Char : ';' is missing";
                }
                token = lexer.getToken();
                intVariables.Add(name,intValue);
            }
        }
        // Çıktı cümlesi
        public void Ç()
        {
            int result = 0;
            if (!isErrorRaised)
            {
                if (token.type == TokenType.LSS)
                {
                    token = lexer.getToken();
                    result = E().Value;
                    outputText += result.ToString() + Environment.NewLine;
                }
            } 
        }
        // Girdi cümlesi
        public void G()
        {
            if (!isErrorRaised)
            {

            }
        }
        // Aritmetik ifade
        public Result<int> E()
        {
            int result = 0;
            if (!isErrorRaised)
            {
                result = T().Value;
                if (token.type == TokenType.PLUS)
                {
                    token = lexer.getToken();
                    result = result + T().Value;

                    return new Result<int>() { Value = result };
                }
                else if (token.type == TokenType.MINUS)
                {
                    token = lexer.getToken();
                    result = result + T().Value;

                    return new Result<int>() { Value = result };
                }
            }
            return new Result<int>();
        }
        // Çarpma - bölme - mod terimi 
        public Result<int> T()
        {
            int result = 0;
            if (!isErrorRaised)
            {
                result = U().Value;
                if (token.type == TokenType.MUL)
                {
                    token = lexer.getToken();
                    result = result * U().Value;

                    return new Result<int>() { Value = result};
                }
                else if (token.type == TokenType.DIV)
                {
                    token = lexer.getToken();
                    result = result / U().Value;

                    return new Result<int>() { Value = result };
                }
                else if (token.type == TokenType.MOD) 
                {
                    token = lexer.getToken();
                    result = result % U().Value;

                    return new Result<int>() { Value = result };
                }
                return new Result<int>();
            }
            return new Result<int>();
        }
        // Üslü ifade
        public Result<int> U()
        {
            int result =0;
            if (!isErrorRaised)
            {
                result = F().Value;
                if (token.type ==TokenType.EXPO)
                {
                    result = (int)Math.Pow((double)result,(double)U().Value);
                    return new Result<int>() { Value = result };
                }
                else
                {
                    return new Result<int>() { Value= result};
                }
            }
            return new Result<int>();
        }
        // Gruplama ifadesi
        public Result<int> F()
        {
            if (!isErrorRaised)
            {
                if (token.type == TokenType.LPAREN)
                {
                    token = lexer.getToken();
                    E();
                    
                    if (token.type != TokenType.RPAREN)
                    {
                        errorMessage += "syntax error on "+ lexer.position;
                        isErrorRaised = true;
                    }
                    token = lexer.getToken();
                    return new Result<int>();
                }
                else if (token.type==TokenType.CHAR)
                {
                    token = lexer.getToken();
                    K();
                    return new Result<int>();
                }
                else if (token.type == TokenType.INTEGER)
                {
                    token = lexer.getToken();
                    R();
                    return new Result<int>();
                }
            }
            return new Result<int>();
        }

        // Karakterler fonksiyonu
        public Result<string> K()
        {
            if (!isErrorRaised)
            {
                if (token.type == TokenType.CHAR)
                {
                    token = lexer.getToken();
                    return new Result<string>() { Value = token.value };
                }
            }
            return new Result<string>();
        }
        // Rakamlar fonksiyonu
        public Result<int> R()
        {
            if (!isErrorRaised)
            {
                if (token.type == TokenType.INTEGER)
                {
                    token = lexer.getToken();
                    return new Result<int>() { Value=Convert.ToInt32(token.value)};
                }
            }
            return new Result<int>();
        }
        /// <summary>
        /// // Beklenen token tipi eğer şu anki tokenle uyuşmuyorsa error verir. Uyuşuyorsa yeni tokene geçer
        /// </summary>
        public void eat(TokenType tokenType)
        {
            if (token.type == tokenType)
            {
                this.token = lexer.getToken();
            }
            else
            {
                errorMessage += "\n Syntax error";
                isErrorRaised = true;
            }
        }

    }
}
