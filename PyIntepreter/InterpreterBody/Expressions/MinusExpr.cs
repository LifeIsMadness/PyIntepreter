using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class MinusExpr : IExpression
    {
        private IExpression _right;

        public MinusExpr(IExpression right)
        {
            _right = right;
        }

        public int Interpret()
        {
            return - _right.Interpret();
        }
    }
}
