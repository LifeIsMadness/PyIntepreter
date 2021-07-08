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
            _pythonTypeName = "int";
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
            else
            {
                var res = new FloatResult(newVal);
                //res.SetValue(newVal);
                return res;
            }
            //else throw new NotImplementedException();
        }

        public override IResult Mul(IResult right)
        {
            try
            {
                var newVal = Value * right.Value;
                return GetResult(newVal);
            }
            catch (Exception ex)
            {
                Error("*");
            }

            return null;
        }

        public override IResult Div(IResult right)
        {
            double newVal = 0;
            try
            {
                newVal = (double)Value / right.Value;
            }
            catch(Exception ex)
            {
                Error("/");
            }

            if (double.IsInfinity(newVal)) throw new DivideByZeroException();
            return GetResult(newVal);
        }

        public override IResult Add(IResult right)
        {
            try
            {
                var newVal = Value + right.Value;
                return GetResult(newVal);
            }
            catch (Exception ex)
            {
                Error("+");
            }

            return null;
        }

        public override IResult Sub(IResult right)
        {
            try
            {
                var newVal = Value - right.Value;
                return GetResult(newVal);
            }
            catch (Exception ex)
            {
                Error("-");
            }

            return null;
        }

        public override IResult Equal(IResult right)
        {
            try
            {
                return new BoolResult(Value == right.Value);
            }
            catch (Exception ex)
            {
                Error("==");
            }

            return null;
        }

        public override IResult Greater(IResult right)
        {
            try
            {
                return new BoolResult(Value > right.Value);
            }
            catch (Exception ex)
            {
                Error(">");
            }

            return null;
        }

        public override IResult Lesser(IResult right)
        {
            try
            {
                return new BoolResult(Value < right.Value);
            }
            catch (Exception ex)
            {
                Error("<");
            }

            return null;
        }

        public override IResult GreaterEqual(IResult right)
        {
            try
            {
                return new BoolResult(Value >= right.Value);
            }
            catch (Exception ex)
            {
                Error(">=");
            }

            return null;
        }

        public override IResult LesserEqual(IResult right)
        {
            try
            {
                return new BoolResult(Value <= right.Value);
            }
            catch (Exception ex)
            {
                Error("<=");
            }

            return null;
        }

        public override IResult NotEqual(IResult right)
        {
            try
            {
                return new BoolResult(Value != right.Value);
            }
            catch (Exception ex)
            {
                Error("!=");
            }

            return null;
        }
    }
}
