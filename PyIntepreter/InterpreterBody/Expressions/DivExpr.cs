using PyInterpreter.InterpreterBody.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class DivExpr : IExpression
    {
        public IExpression _left;
        public IExpression _right;

        public DivExpr(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public void Accept(ExpressionVisitor visitor)
        {
            visitor.VisitDivExpr(this);
        }

        public IResult Eval(IResult left, IResult right)
        {
            return left.Div(right);
        }
    }
}
