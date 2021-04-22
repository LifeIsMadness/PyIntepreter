using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class MinusExpr : IExpression
    {
        public readonly IExpression _right;

        public MinusExpr(IExpression right)
        {
            _right = right;
        }

        public void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitMinusExpr(this);
        }

        public IResult Eval(IResult right)
        {
            return right.Minus();
        }
    }
}
