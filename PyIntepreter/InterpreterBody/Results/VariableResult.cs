using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Results
{
    public class VariableResult : Result
    {
        public VariableResult()
        {
        }

        public VariableResult(string value) : base(value)
        {
        }
    }
}
