using PyInterpreter.InterpreterBody.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.SymbTable
{
    public class Variable
    {
        public string Name { get; }

        public string Type { get; }

        public IResult Value { get; }

        public Variable(string name, IResult value)
        {
            Name = name;
            Value = value;
            if (Value.GetType() == typeof(IntResult))
            {
                Type = "int";
            }
            else if (Value.GetType() == typeof(FloatResult))
            {
                Type = "float";
            }
            else if (Value.GetType() == typeof(BoolResult))
            {
                Type = "bool";   
            }
            else
            {
                Type = "list";
            }
        }
    }
}
