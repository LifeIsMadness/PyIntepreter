using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class IndexExpr : IExpression
    {
        public readonly IExpression _list;

        public readonly IExpression _indexValue;

        public IndexExpr(IExpression list, IExpression indexValue)
        {
            _list = list;
            _indexValue = indexValue;
        }

        public void Accept(IVisitor expressionVisitor)
        {
            expressionVisitor.VisitIndexExpr(this);
        }

        public IResult Eval(IResult list, IResult index)
        {
            return list.Value[index.Value];
        }
    }
}
