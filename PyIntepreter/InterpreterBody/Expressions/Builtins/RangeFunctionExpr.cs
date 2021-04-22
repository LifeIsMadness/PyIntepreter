using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions.Builtins
{
    public class RangeFunctionExpr: FunctionBodyExpr
    {
        public RangeFunctionExpr()
        {
            _argCount = 2;
        }

        public override void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitRangeExpr(this);
        }

        public IResult Eval()
        {
            if (Args.Count > _argCount || Args.Count == 0)
            {
                throw new Exception($"Expected at most {ArgCount} " +
                    $"args but got {Args.Count}");
            }

            if (Args.Count == 1)
            {
                return CreateRange(0, Args[0].Value);
            }
            else
            {
                return CreateRange(Args[0].Value, Args[1].Value);
            }
        }

        private IResult CreateRange(int a, int b)
        {
            List<IResult> range = new List<IResult>();

            for (int i = a; i < b; i++)
            {
                range.Add(new IntResult(i));
            }

            return new ListResult(range);
        }
    }
}
