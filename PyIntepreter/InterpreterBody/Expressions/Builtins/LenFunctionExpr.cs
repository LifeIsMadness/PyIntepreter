using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions.Builtins
{
    public class LenFunctionExpr : FunctionBodyExpr
    {
        public LenFunctionExpr()
        {
            _argCount = 1;
        }

        public override void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitLenExpr(this);
        }

        public IResult Eval()
        {
            if (Args.Count != ArgCount)
            {
                throw new Exception($"Expected {ArgCount} args but got {Args.Count}");
            }
            
            return new IntResult(Args[0].Value.Count);
        }
    }
}
