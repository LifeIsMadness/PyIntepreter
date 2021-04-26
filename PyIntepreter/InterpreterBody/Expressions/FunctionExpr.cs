using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class FunctionExpr : IExpression
    {
        public IExpression Name { get; }

        public IList<IExpression> Args { get; }

        public FunctionExpr(IExpression name, IList<IExpression> args)
        {
            Name = name;
            Args = args;
        }

        public int LineNumber { get; set; }
        public void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitFunctionExpr(this);
        }
    }
}
