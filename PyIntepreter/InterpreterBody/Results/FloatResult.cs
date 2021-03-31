using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Results
{
    public class FloatResult : Result
    {
        //public FloatResult(string value) : base(value)
        //{
        //    ParseRawValue();
        //}

        public FloatResult(double value): base(value)
        {
            //Value = value;
        }

        //protected override void ParseRawValue()
        //{
        //    _value = double.Parse(_rawValue);
        //}

        //public override dynamic GetValue() => _value;

        private FloatResult GetResult(double newval)
        {
            var result = new FloatResult(newval);
            //result.setvalue(newval);
            return result;

        }

        public override IResult Plus() => this;

        public override IResult Minus()
        {
            double newval = -Value;
            return GetResult(newval);
        }

        public override IResult Mul(IResult right)
        {
            double newVal = Value * right.Value;
            return GetResult(newVal);
        }

        public override IResult Div(IResult right)
        {
            double newVal = Value / right.Value;
            if (double.IsInfinity(newVal)) throw new DivideByZeroException();
            return GetResult(newVal);
        }

        public override IResult Add(IResult right)
        {
            double newVal = Value + right.Value;
            return GetResult(newVal);
        }

        public override IResult Sub(IResult right)
        {
            double newVal = Value - right.Value;
            return GetResult(newVal);
        }
    }
}
