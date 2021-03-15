using PyInterpreter.InterpreterBody.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public interface IExpression
    {
        void Accept(ExpressionVisitor expressionVisitor);
    }
}
