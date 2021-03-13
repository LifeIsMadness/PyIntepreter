using PyInterpreter.InterpreterBody.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PyInterpreter.InterpreterBody
{
    public class Interpreter
    {
        private readonly Parser _parser;
        public Interpreter(Parser parser)
        {
            _parser = parser;
        }

        public int Interpret()
        {
            var expr = _parser.Parse();
            return expr.Interpret();
        }
    }
}
