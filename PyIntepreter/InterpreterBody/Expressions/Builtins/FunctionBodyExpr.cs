using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions.Builtins
{
    public class FunctionBodyExpr : IExpression
    {
        protected int _argCount = 0;
        public int ArgCount { get => _argCount; }

        public IList<IResult> Args { get; set; }

        public virtual void Accept(IVisitor expressionVisitor)
        {
            throw new NotImplementedException();
        }
    }
}
