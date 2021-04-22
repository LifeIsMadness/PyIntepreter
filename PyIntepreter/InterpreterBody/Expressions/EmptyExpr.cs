using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class EmptyExpr : IExpression
    {
        public void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitEmptyExpr(this); 
        }

        public IResult Eval()
        {
            return new NoResult();
        }
    }
}
