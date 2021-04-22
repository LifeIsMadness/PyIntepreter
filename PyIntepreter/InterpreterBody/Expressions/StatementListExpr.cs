using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class StatementListExpr : IExpression
    {
        public IList<IExpression> Statements { get; }

        public StatementListExpr(IList<IExpression> statements)
        {
            Statements = statements;
        }

        public void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitStatementListExpr(this);
        }
    }
}
