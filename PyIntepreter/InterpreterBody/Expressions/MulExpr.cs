using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class MulExpr : IExpression
    {
        private int _left;
        private int _right;

        public MulExpr(int left, int right)
        {
            _left = left;
            _right = right;
        }

        public int Interpret()
        {
            return _left * _right;
        }
    }
}
