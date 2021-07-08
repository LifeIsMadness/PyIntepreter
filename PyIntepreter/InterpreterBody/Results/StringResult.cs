using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Results
{
    public class StringResult : Result
    {
        public StringResult(string value) : base(value)
        {
            _pythonTypeName = "str";
        }

        //private BoolResult ParseComparisonValue(int value)
        //{

        //}

        public override IResult Add(IResult right)
        {
            return new StringResult(Value + right.Value);
        }

        public override IResult Div(IResult right)
        {
            throw new Exception("Not supported");
        }

        public override IResult Equal(IResult right)
        {
            return new BoolResult(Value == right.Value);
        }

        public override IResult Greater(IResult right)
        {
            int result = string.Compare(Value, right.Value);
            if (result == -1 || result == 0)
                return new BoolResult(false);
            else return new BoolResult(true);
        }

        public override IResult GreaterEqual(IResult right)
        {
            int result = string.Compare(Value, right.Value);
            if (result == -1)
                return new BoolResult(false);
            else return new BoolResult(true);
        }

        public override IResult Lesser(IResult right)
        {
            int result = string.Compare(Value, right.Value);
            if (result == 1 || result == 0)
                return new BoolResult(false);
            else return new BoolResult(true);
        }

        public override IResult LesserEqual(IResult right)
        {
            int result = string.Compare(Value, right.Value);
            if (result == 1)
                return new BoolResult(false);
            else return new BoolResult(true);
        }

        public override IResult Minus()
        {
            throw new Exception("Not supported");
        }

        public override IResult Mul(IResult right)
        {
            throw new Exception("Not supported");
        }

        public override IResult NotEqual(IResult right)
        {
            return new BoolResult(Value != right.Value);
        }

        public override IResult Plus()
        {
            throw new Exception("Not supported");
        }

        public override IResult Sub(IResult right)
        {
            throw new Exception("Not supported");
        }
    }
}
