using PyInterpreter.InterpreterBody.Expressions;
using PyInterpreter.InterpreterBody.SymbTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PyInterpreter.InterpreterBody
{
    public class Interpreter
    {
        private readonly Parser _parser;

        private SymbTable.SymbolTable _symbolTable;

        public SymbolTable SymbolTable { get => _symbolTable; }

        public Interpreter(Parser parser)
        {
            _parser = parser;
            _symbolTable = new SymbolTable();
            parser.SymbolTable = _symbolTable;
        }

        public dynamic Interpret()
        {
            var expr = _parser.Parse();
            //var result = expr.Interpret();
            foreach (var node in expr)
            {
                node.Interpret();
            }
            return null;
            // return result.GetValue();
        }
    }
}
