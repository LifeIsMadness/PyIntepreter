using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class IfExpr : IExpression
    {
        public IList<IExpression> Conditions { get; }
        public IList<IExpression> Statements { get; }

        public IfExpr(IList<IExpression> conditions,
                      IList<IExpression> statements)
        {
            Conditions = conditions;
            Statements = statements;
        }

        public void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitIfExpr(this);
        }

        public IExpression Eval(List<IResult> conditions)
        {
            int i = 0;
            for (; i < conditions.Count; i++)
            {
                if (conditions[i].Value == true)
                {
                    return Statements[i];
                }
            }

            // 'else' не имеет условного выражения
            if (Conditions.Count < Statements.Count)
                return Statements[i];

            return new EmptyExpr();

        }
    }
}
