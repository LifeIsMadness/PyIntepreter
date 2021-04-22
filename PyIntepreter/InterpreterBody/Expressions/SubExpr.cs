using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class SubExpr : IExpression
    {
        public IExpression _left;
        public IExpression _right;

        public SubExpr(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitSubExpr(this);
        }

        public IResult Eval(IResult left, IResult right)
        {
            return left.Sub(right);
        }
    }
}
