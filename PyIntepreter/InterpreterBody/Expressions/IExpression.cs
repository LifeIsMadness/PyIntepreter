using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public interface IExpression
    {
        public int Interpret();
    }
}
