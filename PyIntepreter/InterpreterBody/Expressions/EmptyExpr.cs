using PyInterpreter.InterpreterBody.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class EmptyExpr : IExpression
    {
        public IResult Interpret()
        {
            return new NoResult();
        }
    }
}
