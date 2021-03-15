using PyInterpreter.InterpreterBody.Results;
using System;
using System.Collections.Generic;
using System.Text;
using PyInterpreter.InterpreterBody.SymbTable;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class AssignExpr : IExpression
    {
        private IExpression _left, _right;
        private SymbolTable _vars;

        public AssignExpr(IExpression left, IExpression right, SymbolTable symbolTable)
        {
            _left = left;
            _right = right;
            _vars = symbolTable;
        }

        public IResult Interpret()
        {
            var name = _left.Interpret().GetValue();
            var expr = _right.Interpret().GetValue();
            var variable = new Variable
            {
                Name = name,
                Type = expr.GetType().Name,
                Value = expr,
            };
            _vars.SetVariable(name, variable);
            return new NoResult();
        }
    }
}
