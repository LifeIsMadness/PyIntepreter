using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class ListExpr : IExpression
    {
        public readonly IList<IExpression> items;

        public ListExpr(IList<IExpression> items)
        {
            this.items = items;
        }

        public void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitListExpr(this);
        }

        public int LineNumber { get; set; }
        public IResult Eval(List<IResult> items)
        {
            return new ListResult(items);
        }
    }
}
