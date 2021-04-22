using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class OrExpr : IExpression
    {
        public IExpression Left { get; }
        public IExpression Right { get; }

        public OrExpr(IExpression left, IExpression right)
        {
            Left = left;
            Right = right;
        }

        public void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitOrExpr(this);
        }

        public IResult Eval(IResult left, IResult right)
        {
            return new BoolResult(left.Value || right.Value);
        }
    }
}
