using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class NumberExpr : IExpression
    {
        private int _number;

        public NumberExpr(int number)
        {
            _number = number;
        }

        public int Interpret()
        {
            return _number;
        }
    }
}
