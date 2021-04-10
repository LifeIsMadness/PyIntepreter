using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Results
{
    public class ListResult: Result
    {
        public ListResult(IList<IResult> results): base(results)
        {
            //Value = results;
        }

        public override IResult Equal(IResult right)
        {
            throw new Exception("Not supported");
        }

        public override IResult Greater(IResult right)
        {
            throw new Exception("Not supported");
        }

        public override IResult Lesser(IResult right)
        {
            throw new Exception("Not supported");
        }

        public override IResult GreaterEqual(IResult right)
        {
            throw new Exception("Not supported");
        }

        public override IResult LesserEqual(IResult right)
        {
            throw new Exception("Not supported");
        }

        public override IResult NotEqual(IResult right)
        {
            throw new Exception("Not supported");
        }
    }
}

