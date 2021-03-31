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

        public IResult Plus();

        public IResult Minus();

        public IResult Mul(IResult right);

        public IResult Div(IResult right);

        public IResult Add(IResult right);

        public IResult Sub(IResult right);
    }
}
