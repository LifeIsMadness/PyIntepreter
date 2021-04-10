using PyInterpreter.InterpreterBody.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class GreaterExpr : IExpression
    {
        public IExpression _left;
        public IExpression _right;

        public GreaterExpr(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public void Accept(ExpressionVisitor visitor)
        {
            visitor.VisitGreaterExpr(this);
        }

        public IResult Eval(IResult left, IResult right)
        {
            return left.Greater(right);
        }
    }
}
