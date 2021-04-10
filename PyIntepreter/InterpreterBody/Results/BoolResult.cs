using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Results
{
    //TODO: bool
    public class BoolResult: Result
    {
        public BoolResult(bool value) : base(value)
        {
        }

        public override IResult Add(IResult right)
        {
            throw new Exception("Operator '+' cannot be applied " +
                "to operands of type 'bool'");
        }

        public override IResult Div(IResult right)
        {
            throw new Exception("Operator '/' cannot be applied " +
                "to operands of type 'bool'");
        }

        public override IResult Equal(IResult right)
        {
            return new BoolResult(Value == right.Value);
        }

        public override IResult Greater(IResult right)
        {
            throw new Exception("Operator '>' cannot be applied " +
                "to operands of type 'bool'");
        }

        public override IResult GreaterEqual(IResult right)
        {
            throw new Exception("Operator '>=' cannot be applied " +
                "to operands of type 'bool'");
        }

        public override IResult Lesser(IResult right)
        {
            throw new Exception("Operator '<' cannot be applied " +
                "to operands of type 'bool'");
        }

        public override IResult LesserEqual(IResult right)
        {
            throw new Exception("Operator '<=' cannot be applied " +
                "to operands of type 'bool'");
        }

        public override IResult Minus()
        {
            throw new Exception("Operator unary '-' cannot be applied " +
                "to operands of type 'bool'");
        }

        public override IResult Mul(IResult right)
        {
            throw new Exception("Operator '*' cannot be applied " +
                "to operands of type 'bool'");
        }

        public override IResult NotEqual(IResult right)
        {
            return new BoolResult(Value != right.Value);
        }

        public override IResult Plus()
        {
            throw new Exception("Operator unary '+' cannot be applied " +
                "to operands of type 'bool'");
        }

        public override IResult Sub(IResult right)
        {
            throw new Exception("Operator '-' cannot be applied " +
                "to operands of type 'bool'");
        }
    }
}
