using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Results
{
    public class FloatResult : Result
    {
        public FloatResult(string value) : base(value)
        {
            ParseRawValue();
        }

        public FloatResult()
        {
        }

        protected override void ParseRawValue()
        {
            _value = double.Parse(_rawValue);
        }

        public override dynamic GetValue() => _value;

        private FloatResult GetResult(double newVal)
        {
            var result = new FloatResult();
            result.SetValue(newVal);
            return result;

        }

        public override IResult Plus() => this;

        public override IResult Minus()
        {
            int newVal = -GetValue();
            return GetResult(newVal);
        }

        public override IResult Mul(IResult right)
        {
            double newVal = this.GetValue() * right.GetValue();
            return GetResult(newVal);
        }

        public override IResult Div(IResult right)
        {
            double newVal = this.GetValue() / right.GetValue();
            if (double.IsInfinity(newVal)) throw new DivideByZeroException();
            return GetResult(newVal);
        }

        public override IResult Add(IResult right)
        {
            double newVal = this.GetValue() + right.GetValue();
            return GetResult(newVal);
        }

        public override IResult Sub(IResult right)
        {
            double newVal = this.GetValue() - right.GetValue();
            return GetResult(newVal);
        }
    }
}
