using PyInterpreter.InterpreterBody.Results;
using System;
using System.Collections.Generic;
using System.Text;
using PyInterpreter.InterpreterBody.SymbTable;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class AssignExpr : IExpression
    {
        public IExpression _left, _right;
        private SymbolTable _vars;

        public AssignExpr(IExpression left, IExpression right, SymbolTable symbolTable)
        {
            _left = left;
            _right = right;
            _vars = symbolTable;
        }

        public void Accept(ExpressionVisitor expressionVisitor)
        {
            expressionVisitor.VisitAssignExpr(this);
        }

        public IResult Eval(string name, IResult expr)
        {
            return new NoResult();
        }
    }
}
