using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class AddExpr : IExpression
    {
        public IExpression _left;

        public IExpression _right;

        public AddExpr(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitAddExpr(this);
        }
        public int LineNumber { get; set; }

        public IResult Eval(IResult left, IResult right)
        {
            return left.Add(right);
        }
    }
}
