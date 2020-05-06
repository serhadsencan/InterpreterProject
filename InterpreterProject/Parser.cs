using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterpreterProject
{
    public class Parser
    {
        public Token token;
        public string ErrorMessage="";
       

        public Lexer lexer;
        public bool isErrorRaised;
        public Control output;
        public string outputText="";
        public Dictionary<string, int> intVariables;
        public Dictionary<string, string> stringVariables;
        List<TokenType> cList = new List<TokenType>() { TokenType.LSQUARE, TokenType.LBRACKET, TokenType.CHAR, TokenType.WRITE, TokenType.READ };

        public Parser(Lexer lexer, Control output)
        {
            this.lexer = lexer;
            isErrorRaised = false;
            intVariables = new Dictionary<string, int>();
            stringVariables = new Dictionary<string, string>();
            this.output = output;
        }
        // Program
        public void P()
        {
            token = lexer.getToken();
            while (cList.Contains(token.type))
            {
                C();
            }
            if (token.type == TokenType.EOF)
            {
                if (string.IsNullOrWhiteSpace(ErrorMessage))
                    output.Text = outputText;
                else
                    output.Text = ErrorMessage;
            }
            else
            {
                ErrorMessage += "Syntax error at: " + lexer.position + "  Char : '.' is missing";
                RaiseError();
                return;
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
                else if (token.type == TokenType.WRITE)
                {
                    Ç();
                }
                else if (token.type==TokenType.READ)
                {
                    G();
                }
                else
                {
                    ErrorMessage += "Syntax error at: " + lexer.position + " unexpected token ";
                    RaiseError();
                    return;
                }
            }
        }

        // If cümlesi
        public void I()
        {
            int statement = 0;
            int qmarkIndex =0;
            TokenType type;
            if (!isErrorRaised)
            {
                if(token.type != TokenType.LSQUARE)
                {
                    ErrorMessage += "Syntax error at: " + lexer.position + "  Char : '[' is missing";
                    RaiseError();
                    return;
                }
                token = lexer.getToken();
                statement = E().Value;
                if (token.type != TokenType.QMARK)
                {
                    ErrorMessage += "Syntax error at: " + lexer.position + "  Char : '?' is missing";
                    RaiseError();
                    return;
                }
                qmarkIndex = token.index;
                token = lexer.getToken();
                while (token.type!=TokenType.BECOMES||token.type!=TokenType.RSQUARE||lexer.position<lexer.text.Length)
                {
                    token = lexer.getToken();
                }
                type = token.type;
                if (type!=TokenType.BECOMES||type!=TokenType.RSQUARE)
                {
                    ErrorMessage += "Syntax error at: " + qmarkIndex+1 + " could not find ':' or ']'";
                    RaiseError();
                    return;
                }
                lexer.GotoIndex(qmarkIndex);
                token = lexer.getToken();

                if (type == TokenType.BECOMES)
                {
                    if (statement != 0)
                    {
                        C();
                        while (cList.Contains(token.type))
                        {
                            C();
                        }
                    }
                    else
                    {
                        if (token.type != TokenType.BECOMES)
                        {
                            ErrorMessage += "Syntax error at: " + lexer.position +" Line :"+lexer.Line+ "  Char : ':' is missing";
                            RaiseError();
                            return;
                        }
                        // : karakterini atlamak için yeni token alıyoruz
                        token = lexer.getToken();
                        C();
                        while (cList.Contains(token.type))
                        {
                            C();
                        }
                    }
                }
                else if (type == TokenType.RSQUARE)
                {
                    if (statement != 0)
                    {
                        C();
                        while (cList.Contains(token.type))
                        {
                            C();
                        }
                    }
                }
            }
        }
        // While döngüsü
        public void W()
        {
            int turn=0;
            int start = 0;

            if (!isErrorRaised)
            {
                
                if (token.type != TokenType.LBRACKET)
                {
                    ErrorMessage += "Syntax error at: " + lexer.position + "  Char : '{' is missing";
                    RaiseError();
                    return;
                }
                token = lexer.getToken();
                start = token.index-1;

                turn = E().Value;
                while (turn!=0)
                {
                    lexer.GotoIndex(start);
                    token = lexer.getToken();
                    turn = E().Value;

                    if (token.type != TokenType.QMARK)
                    {
                        ErrorMessage += "Syntax error at: " + lexer.position + "  Char : '?' is missing";
                        RaiseError();
                        return ;
                    }
                    token = lexer.getToken();
                    C();
                    while (cList.Contains(token.type))
                    {
                        C();
                    }
                    if (token.type != TokenType.RBRACKET)
                    {
                        ErrorMessage += "Syntax error at: " + lexer.position + "  Char : '}' is missing";
                        RaiseError();
                        return ;
                    }
                    token = lexer.getToken();
                }
            }
            return ;
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
                    ErrorMessage += "Syntax error at: " + lexer.position + "  Char : '=' is missing";
                    RaiseError();
                    return;
                }
                token = lexer.getToken();
                
                intValue = E().Value;

                if (token.type != TokenType.SEMICOLON)
                {
                    ErrorMessage += "Syntax error at: " + lexer.position + "  Char : ';' is missing";
                    RaiseError();
                    return;
                }
                token = lexer.getToken();
                if (intVariables.Keys.Contains(name))
                {
                    intVariables[name] = intValue;
                }
                else
                {
                    intVariables.Add(name, intValue);
                }
            }
            return;
        }



        // Çıktı cümlesi
        public void Ç()
        {
            int result = 0;
            if (!isErrorRaised)
            {
                if (token.type != TokenType.WRITE)
                {
                    ErrorMessage += "syntax error on " + lexer.position + " Line : "+lexer.Line+ "missing '<'" ;
                    RaiseError();
                    return;
                }
                token = lexer.getToken();
                result = E().Value;
                if (token.type != TokenType.SEMICOLON)
                {
                    ErrorMessage += "syntax error on " + lexer.position+ " Line : " + lexer.Line+" missing ';'  ";
                    RaiseError();
                    return;
                }
                token = lexer.getToken();
                outputText += result.ToString() + Environment.NewLine;
            } 
        }


        // Girdi cümlesi
        public void G()
        {
            var result="";
            if (!isErrorRaised)
            {
                if (token.type != TokenType.READ)
                {
                    ErrorMessage += "syntax error on " + lexer.position + " Line : " + lexer.Line;
                    RaiseError();
                    return;
                }
                token = lexer.getToken();
                result = K().Value;
                if (token.type != TokenType.SEMICOLON)
                {
                    ErrorMessage += "syntax error on " + lexer.position + " Line : " + lexer.Line;
                    RaiseError();
                    return;
                }
                token = lexer.getToken();
            }
        }


        // Aritmetik ifade

        public Result<int> E()
        {
            int result = 0;
            if (!isErrorRaised)
            {
                result = T().Value;
                while (token.type == TokenType.PLUS||token.type==TokenType.MINUS)
                {
                    if (token.type == TokenType.PLUS)
                    {
                        token = lexer.getToken();
                        result = result + T().Value;
                    }
                    else if (token.type == TokenType.MINUS)
                    {
                        token = lexer.getToken();
                        result = result - T().Value;
                    }
                }
                return new Result<int>() { Value = result };

            }
            return new Result<int>() { Value = result};
        }

        // Çarpma - bölme - mod terimi 
        public Result<int> T()
        {
            int result = 0;
            if (!isErrorRaised)
            {
                result = U().Value;
                while (token.type==TokenType.MUL||token.type==TokenType.DIV||token.type==TokenType.MOD)
                {
                    if (token.type == TokenType.MUL)
                    {
                        token = lexer.getToken();
                        result = result * U().Value;
                    }
                    else if (token.type == TokenType.DIV)
                    {
                        token = lexer.getToken();
                        result = result / U().Value;
                    }
                    else if (token.type == TokenType.MOD)
                    {
                        token = lexer.getToken();
                        result = result % U().Value;
                    }

                }
                return new Result<int>() { Value = result};
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
                    token = lexer.getToken();
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
            int val = 0;
            if (!isErrorRaised)
            {
                if (token.type == TokenType.LPAREN)
                {
                    token = lexer.getToken();
                    E();
                    
                    if (token.type != TokenType.RPAREN)
                    {
                        ErrorMessage += "syntax error on "+ lexer.position;
                        RaiseError();
                        return new Result<int> ();
                    }
                    token = lexer.getToken();
                    return new Result<int>();
                }
                else if (token.type==TokenType.CHAR)
                {
                    val = GetVariableVal(K());
                    return new Result<int>() { Value = val };
                }
                else if (token.type == TokenType.INTEGER)
                {
                    val = R().Value;
                    return new Result<int>() { Value = val };
                }
            }
            return new Result<int>();
        }

        // Karakterler fonksiyonu
        public Result<string> K()
        {
            string val="";
            if (!isErrorRaised)
            {
                if (token.type != TokenType.CHAR)
                {
                    ErrorMessage += "Syntax error at: " + lexer.position + "  Char : is not char type";
                    RaiseError(); 
                    return new Result<string>();
                }
                val = token.value;
                token = lexer.getToken();
                return new Result<string>() { Value = val };
            }
            return new Result<string>();
        }
        
        // Rakamlar fonksiyonu
        public Result<int> R()
        {
            int val = 0;
            if (!isErrorRaised)
            {
                if (token.type != TokenType.INTEGER)
                {
                    ErrorMessage += "Syntax error at: " + lexer.position + "  Char : is not integer type";
                    RaiseError();
                    return new Result<int>();
                }
                val = Convert.ToInt32(token.value);
                token = lexer.getToken();
                return new Result<int>() { Value = val };
            }
            return new Result<int>();
        }
       
        public void RaiseError()
        {
            isErrorRaised = true;
            lexer.position = lexer.text.Length-1;
            token = lexer.GetEndofFunction();
            output.Text = ErrorMessage;
        }
        public int GetVariableVal(Result<string> name)
        {
            int myVal = intVariables.FirstOrDefault(x => x.Key == name.Value).Value;
            return myVal;
        }

    }
}
