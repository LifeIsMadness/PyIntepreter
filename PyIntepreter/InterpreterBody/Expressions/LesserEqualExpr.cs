using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class LesserEqualExpr : IExpression
    {
        public IExpression _left;
        public IExpression _right;

        public LesserEqualExpr(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitLesserEqualExpr(this);
        }

        public IResult Eval(IResult left, IResult right)
        {
            return left.LesserEqual(right);
        }
    }
}
