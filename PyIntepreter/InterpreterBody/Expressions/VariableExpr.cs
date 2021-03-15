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

        public IResult Interpret()
        {
            var variable = _vars.GetVariable(_name);

            var res =  new VariableResult();
            res.SetValue(variable.Value);
            return res;
        }


    }
}
