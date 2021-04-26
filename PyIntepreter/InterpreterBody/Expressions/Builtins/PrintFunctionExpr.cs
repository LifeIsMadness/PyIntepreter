using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions.Builtins
{
    public class PrintFunctionExpr : FunctionBodyExpr
    {
        public PrintFunctionExpr()
        {
            _argCount = 1;
        }

        public override void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitPrintExpr(this);
        }

        public IResult Eval()
        {
            if (Args.Count != ArgCount)
            {
                throw new Exception($"Expected {ArgCount} args but got {Args.Count}");
            }

            if (Args[0] is ListResult)
            {
                Program.PrintList(Args[0].Value);
                //Console.Write('[');
                //foreach (var item in Args[0].Value)
                //{
                //    Console.Write($"{item.Value}, ");
                //}
                //Console.Write("]\n");
                Console.WriteLine();
            }
            else
                Console.WriteLine(Args[0].Value);

            return new NoResult();
        }
    }
}
