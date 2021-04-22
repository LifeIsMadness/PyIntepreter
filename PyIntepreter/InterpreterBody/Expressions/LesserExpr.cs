using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class LesserExpr : IExpression
    {
        public IExpression _left;
        public IExpression _right;

        public LesserExpr(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitLesserExpr(this);
        }

        public IResult Eval(IResult left, IResult right)
        {
            return left.Lesser(right);
        }
    }
}
