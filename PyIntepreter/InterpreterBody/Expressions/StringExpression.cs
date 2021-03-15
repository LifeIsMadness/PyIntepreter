using PyInterpreter.InterpreterBody.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class StringExpression: IExpression
    {
        private string _val;

        public StringExpression(string val)
        {
            _val = val;
        }

        public IResult Interpret()
        {
            return new StringResult(_val);
        }
    }
}
