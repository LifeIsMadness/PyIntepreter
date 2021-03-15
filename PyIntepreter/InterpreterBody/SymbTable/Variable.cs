using PyInterpreter.InterpreterBody.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.SymbTable
{
    public class Variable
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public IResult Value { get; set; }
    }
}
