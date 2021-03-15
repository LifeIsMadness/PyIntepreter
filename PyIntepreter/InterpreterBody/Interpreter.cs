using PyInterpreter.InterpreterBody.Expressions;
using PyInterpreter.InterpreterBody.SymbTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PyInterpreter.InterpreterBody
{
    public class Interpreter: ExpressionVisitor
    {
        private readonly Parser _parser;

        public Interpreter(Parser parser): base()
        {
            _parser = parser;
            parser.SymbolTable = _symbolTable;
        }

        public dynamic Interpret()
        {
            var expr = _parser.Parse();
            //var result = expr.Interpret();
            expr.Accept(this);
            return null;
            // return result.GetValue();
        }
    }
}
