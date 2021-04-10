using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Results
{
    public class IntResult: Result
    {
        //public IntResult(string value) : base(value)
        //{
        //    ParseRawValue();
        //}

        public IntResult(int value): base(value)
        {
            //Value = value;
        }

        //protected override void ParseRawValue()
        //{
        //    _value = int.Parse(_rawValue);
        //}

        //public override dynamic GetValue() => _value;

        public override IResult Plus() => this;

        public override IResult Minus()
        {
            //int newVal = - GetValue();
            //return new IntResult(newVal.ToString());
            int newVal = -Value;
            return new IntResult(newVal);
        }

        private IResult GetResult(dynamic newVal)
        {
            if (newVal is int)
            {
                var res = new IntResult(newVal);
                //res.SetValue(newVal);
                return res;
            }
            else if (newVal is double)
            {
                var res = new FloatResult(newVal);
                //res.SetValue(newVal);
                return res;
            }
            else throw new NotImplementedException();
        }

        public override IResult Mul(IResult right)
        {
            var newVal = Value * right.Value;
            return GetResult(newVal);
        }

        public override IResult Div(IResult right)
        {
            double newVal = (double)Value / right.Value;
            if (double.IsInfinity(newVal)) throw new DivideByZeroException();
            return GetResult(newVal);
        }

        public override IResult Add(IResult right)
        {
            var newVal = Value + right.Value;
            return GetResult(newVal);
        }

        public override IResult Sub(IResult right)
        {
            var newVal = Value - right.Value;
            return GetResult(newVal);
        }

        public override IResult Equal(IResult right)
        {
            return new BoolResult(Value == right.Value);
        }

        public override IResult Greater(IResult right)
        {
            return new BoolResult(Value > right.Value);
        }

        public override IResult Lesser(IResult right)
        {
            return new BoolResult(Value < right.Value);
        }

        public override IResult GreaterEqual(IResult right)
        {
            return new BoolResult(Value >= right.Value);
        }

        public override IResult LesserEqual(IResult right)
        {
            return new BoolResult(Value <= right.Value);
        }

        public override IResult NotEqual(IResult right)
        {
            return new BoolResult(Value != right.Value);
        }
    }
}
