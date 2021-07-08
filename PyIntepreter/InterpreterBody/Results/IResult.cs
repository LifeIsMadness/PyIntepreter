using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Results
{
    /// <summary>
    /// Result of any expression.
    /// </summary>
    public interface IResult
    {
        //public dynamic GetValue();

        //public void SetValue(dynamic val);
        public dynamic Value { get; set; }

        public string PythonTypeName { get; }

        public IResult Plus();

        public IResult Minus();

        public IResult Mul(IResult right);

        public IResult Div(IResult right);

        public IResult Add(IResult right);

        public IResult Sub(IResult right);

        public IResult Equal(IResult right);

        public IResult NotEqual(IResult right);

        public IResult Greater(IResult right);

        public IResult Lesser(IResult right);

        public IResult GreaterEqual(IResult right);

        public IResult LesserEqual(IResult right);
    }
}
