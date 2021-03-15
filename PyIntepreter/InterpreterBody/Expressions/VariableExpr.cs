using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.SymbTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class VariableExpr: IExpression
    {
        private string _name;
        private SymbolTable _vars;

        public VariableExpr(string name, SymbolTable vars)
        {
            _name = name;
            _vars = vars;
        }

        public void Accept(ExpressionVisitor expressionVisitor)
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
