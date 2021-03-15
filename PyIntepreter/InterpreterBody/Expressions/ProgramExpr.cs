using PyInterpreter.InterpreterBody.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class ProgramExpr : IExpression
    {
        public readonly IList<IExpression> nodes;

        public ProgramExpr(IList<IExpression> nodes)
        {
            this.nodes = nodes;
        }

        public void Accept(ExpressionVisitor expressionVisitor)
        {
            expressionVisitor.VisitProgramExpr(this);
        }
    }
}
