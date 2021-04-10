using PyInterpreter.InterpreterBody.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class ProgramExpr : IExpression
    {
        public readonly IExpression StatementList;

        public ProgramExpr(IExpression statements)
        {
            StatementList = statements;
        }

        public void Accept(ExpressionVisitor expressionVisitor)
        {
            expressionVisitor.VisitProgramExpr(this);
        }
    }
}
