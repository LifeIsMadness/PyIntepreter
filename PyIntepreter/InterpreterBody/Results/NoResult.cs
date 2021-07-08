using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Results
{
    public class NoResult : Result
    {
        public NoResult()
        {
            _pythonTypeName = "NoneType";
            Value = "None";
        }

        //public NoResult(string value) : base(value)
        //{
        //}
    }
}
