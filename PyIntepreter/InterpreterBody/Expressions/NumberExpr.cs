using PyInterpreter.InterpreterBody.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class NumberExpr : IExpression
    {
        private Token _token;

        public NumberExpr(Token token)
        {
            _token = token;
        }

        public void Accept(ExpressionVisitor visitor)
        {
            visitor.VisitNumberExpr(this);
        }

        public IResult Eval()
        {
            if (_token.Type == TokenType.INTEGER_LITERAL)
            {
                return new IntResult(int.Parse(_token.Value));
            }
            else
                return new FloatResult(double.Parse(_token.Value));      
        }
    }
}
