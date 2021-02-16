using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class DivExpr : IExpression
    {
        private int _left;
        private int _right;

        public DivExpr(int left, int right)
        {
            _left = left;
            _right = right;
        }

        public int Interpret()
        {
            return _left / _right;
        }
    }
}
