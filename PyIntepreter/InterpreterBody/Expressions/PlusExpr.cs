using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class PlusExpr : IExpression
    {
        private IExpression _right;

        public PlusExpr(IExpression right)
        {
            _right = right;
        }

        public int Interpret()
        {
            return + _right.Interpret();
        }
    }
}
