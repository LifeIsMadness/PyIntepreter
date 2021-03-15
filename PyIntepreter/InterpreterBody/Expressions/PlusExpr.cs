using PyInterpreter.InterpreterBody.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class PlusExpr : IExpression
    {
        private readonly IExpression _right;

        public PlusExpr(IExpression right)
        {
            _right = right;
        }

        public IResult Interpret()
        {
            return _right.Interpret().Plus();
        }
    }
}
