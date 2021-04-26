using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
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

        public int LineNumber { get; set; }

        public void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitProgramExpr(this);
        }
    }
}
