using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterProject
{
    public class Result<T>
    {
        private T data;
        public T Value 
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }

        public bool isNull()
        {
            if (this.Value==null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
