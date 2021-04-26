using PyInterpreter.InterpreterBody.Results;
using System;
using System.Collections.Generic;
using System.Text;
using PyInterpreter.InterpreterBody.SymbTable;
using PyInterpreter.InterpreterBody.Visitors;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class AssignExpr : IExpression
    {
        public IExpression _left, _right;

        public AssignExpr(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitAssignExpr(this);
        }

        public int LineNumber { get; set; }
        public IResult Eval(string name, IResult expr)
        {
            return new NoResult();
        }
    }
}
