using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions.Builtins
{
    public class IntFunctionExpr: FunctionBodyExpr
    {
        public IntFunctionExpr()
        {
            _argCount = 1;
        }

        public override void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitIntExpr(this);
        }

        public IResult Eval()
        {
            if (Args.Count != ArgCount)
            {
                throw new Exception($"Expected {ArgCount} args but got {Args.Count}");
            }

            var value = Args[0].Value;
            if (value.GetType() == typeof(string))
            {
                return new IntResult(int.Parse(value));
            }
            else return new IntResult((int)value);

        }
    }
}
