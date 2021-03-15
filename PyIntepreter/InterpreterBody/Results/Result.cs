using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Results
{
    public class Result: IResult
    {
        // python lexem;
        protected string _rawValue = string.Empty;

        protected dynamic _value;

        public Result() { }

        public Result(string value)
        {
            _rawValue = value;
            ParseRawValue();
        }
        protected virtual void ParseRawValue()
        {
            _value = _rawValue;
        } 

        public virtual dynamic GetValue() => _value;

        public virtual void SetValue(dynamic val)
        {
            _value = val;
        }

        public virtual IResult Minus()
        {
            SetValue(-_value);
            return this;
        }

        public virtual IResult Plus()
        {
            SetValue(+_value);
            return this;
        }

        public virtual IResult Add(IResult right)
        {
            var res = GetValue() + right.GetValue();
            var newRes = new Result();

            newRes.SetValue(res);
            return newRes;
        }

        public virtual IResult Sub(IResult right)
        {
            var res = GetValue() - right.GetValue();
            var newRes = new Result();

            newRes.SetValue(res);
            return newRes;
        }

        public virtual IResult Mul(IResult right)
        {
            var res = GetValue() * right.GetValue();
            var newRes = new Result();

            newRes.SetValue(_value);
            return newRes;
        }

        public virtual IResult Div(IResult right)
        {
            var res = GetValue() / (double)right.GetValue();
            var newRes = new Result();

            newRes.SetValue(_value);
            return newRes;
        }

    }
}
