using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Results
{
    public class IntResult: Result
    {
        public IntResult(string value) : base(value)
        {
            ParseRawValue();
        }

        public IntResult()
        {
        }

        protected override void ParseRawValue()
        {
            _value = int.Parse(_rawValue);
        }

        public override dynamic GetValue() => _value;

        public override IResult Plus() => this;

        public override IResult Minus()
        {
            int newVal = - GetValue();
            return new IntResult(newVal.ToString());
        }

        private IResult GetResult(dynamic newVal)
        {
            if (newVal is int)
            {
                var res = new IntResult();
                res.SetValue(newVal);
                return res;
            }
            else if (newVal is double)
            {
                var res = new FloatResult();
                res.SetValue(newVal);
                return res;
            }
            else throw new NotImplementedException();
        }

        public override IResult Mul(IResult right)
        {
            var newVal = this.GetValue() * right.GetValue();
            return GetResult(newVal);
        }

        public override IResult Div(IResult right)
        {
            double newVal = (double)this.GetValue() / right.GetValue();
            if (double.IsInfinity(newVal)) throw new DivideByZeroException();
            return GetResult(newVal);
        }

        public override IResult Add(IResult right)
        {
            var newVal = this.GetValue() + right.GetValue();
            return GetResult(newVal);
        }

        public override IResult Sub(IResult right)
        {
            var newVal = this.GetValue() - right.GetValue();
            return GetResult(newVal);
        }
    }
}
