using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public interface IExpression
    {
        public int LineNumber { get; set; }

        void Accept(IVisitor expressionVisitor);
    }
}
