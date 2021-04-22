using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class WhileExpr : IExpression
    {
        public IExpression Condition { get; }

        public IExpression Statements { get; }

        public WhileExpr(IExpression condition,
                         IExpression statements)
        {
            Condition = condition;
            Statements = statements;
        }

        public void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitWhileExpr(this);
        }

        public IResult Eval(IResult condition)
        {
            return condition.Equal(new BoolResult(true));
        }
    }
}
