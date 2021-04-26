using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class AndExpr : IExpression
    {
        public IExpression Left { get; }
        public IExpression Right { get; }

        public AndExpr(IExpression left, IExpression right)
        {
            Left = left;
            Right = right;
        }

        public void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitAndExpr(this);
        }

        public int LineNumber { get; set; }
        public IResult Eval(IResult left, IResult right)
        {
            return new BoolResult(left.Value && right.Value);
        }

        // if left is false;
        public IResult Eval(IResult left)
        {
            return new BoolResult(left.Value);
        }
    }
}
