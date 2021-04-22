using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions.Builtins
{
    public class InputFunctionExpr : FunctionBodyExpr
    {
        public InputFunctionExpr()
        {
        }

        public override void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitInputExpr(this);
        }

        public IResult Eval()
        {          
            if (Args.Count != ArgCount)
            {
                throw new Exception($"Expected {ArgCount} args but got {Args.Count}");
            }

            return new StringResult(Console.ReadLine());
        }
    }
}
