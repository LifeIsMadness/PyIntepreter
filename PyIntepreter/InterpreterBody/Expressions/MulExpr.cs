using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class MulExpr : IExpression
    {
        private IExpression _left;
        private IExpression _right;

        public MulExpr(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public int Interpret()
        {
            return _left.Interpret() * _right.Interpret();
        }
    }
}
