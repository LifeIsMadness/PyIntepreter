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
    }
}
