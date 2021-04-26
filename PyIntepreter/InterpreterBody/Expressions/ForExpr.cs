using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class ForExpr : IExpression
    {
        public IExpression Variable { get; }
        public IExpression Iterable { get; }
        public IExpression Statements { get; }

        public ForExpr(IExpression name,
                       IExpression iterable,
                       IExpression statements)
        {
            Variable = name;
            Iterable = iterable;
            Statements = statements;
        }

        public int LineNumber { get; set; }

        public void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitForExpr(this);
        }
    }
}
