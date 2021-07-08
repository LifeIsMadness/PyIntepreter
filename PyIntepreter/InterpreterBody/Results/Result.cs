using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Results
{
    public abstract class Result: IResult
    {
        // python lexem;
        //protected string _rawValue = string.Empty;

        //protected dynamic _value;
        protected string _pythonTypeName;

        public string PythonTypeName { get => _pythonTypeName; }

        public dynamic Value { get; set; }

        public Result() { }

        public Result(dynamic value) => Value = value;

        protected virtual void Error(string op)
        {
            throw new Exception($"TypeError: Unsupported operand type(s) for '{op}'");
        }

        //public Result(string value)
        //{
        //    _rawValue = value;
        //    ParseRawValue();
        //}
        //protected virtual void ParseRawValue()
        //{
        //    _value = _rawValue;
        //} 

        //public virtual dynamic GetValue() => _value;

        //public virtual void SetValue(dynamic val)
        //{
        //    _value = val;
        //}

        public virtual IResult Minus()
        {
            //SetValue(-_value);
            throw new NotImplementedException();
        }

        public virtual IResult Plus()
        {
            //SetValue(+_value);
            throw new NotImplementedException();
        }

        public virtual IResult Add(IResult right)
        {
            //var res = GetValue() + right.GetValue();
            //var newRes = new Result();

            //newRes.SetValue(res);
            //return newRes;
            throw new NotImplementedException();
        }

        public virtual IResult Sub(IResult right)
        {
            //var res = GetValue() - right.GetValue();
            //var newRes = new Result();

            //newRes.SetValue(res);
            //return newRes;
            throw new NotImplementedException();
        }

        public virtual IResult Mul(IResult right)
        {
            //var res = GetValue() * right.GetValue();
            //var newRes = new Result();

            //newRes.SetValue(_value);
            //return newRes;
            throw new NotImplementedException();
        }

        public virtual IResult Div(IResult right)
        {
            //var res = GetValue() / (double)right.GetValue();
            //var newRes = new Result();

            //newRes.SetValue(_value);
            //return newRes;
            throw new NotImplementedException();
        }

        public virtual IResult Equal(IResult right)
        {
            throw new NotImplementedException();
        }

        public virtual IResult Greater(IResult right)
        {
            throw new NotImplementedException();
        }

        public virtual IResult Lesser(IResult right)
        {
            throw new NotImplementedException();
        }

        public virtual IResult GreaterEqual(IResult right)
        {
            throw new NotImplementedException();
        }

        public virtual IResult LesserEqual(IResult right)
        {
            throw new NotImplementedException();
        }

        public virtual IResult NotEqual(IResult right)
        {
            throw new NotImplementedException();
        }
    }
}
