using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class PlusExpr : IExpression
    {
        public readonly IExpression _right;

        public PlusExpr(IExpression right)
        {
            _right = right;
        }

        public void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitPlusExpr(this);
        }

        public int LineNumber { get; set; }

        public IResult Eval(IResult right)
        {
            return right.Plus();
        }
    }
}
