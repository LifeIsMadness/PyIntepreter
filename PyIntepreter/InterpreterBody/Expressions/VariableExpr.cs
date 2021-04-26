using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.SymbTable;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class VariableExpr: IExpression
    {
        private string _name;

        public VariableExpr(string name)
        {
            _name = name;
        }

        public int LineNumber { get; set; }

        public void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitVariableExpr(this);
        }

        public IResult Eval()
        {
            // TODO: change VariableResult to only return name
            // (its possible to return StringResult).
            return new VariableResult(_name);
        }
    }
}
