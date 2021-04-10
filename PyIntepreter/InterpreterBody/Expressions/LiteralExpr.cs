using PyInterpreter.InterpreterBody.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class LiteralExpr : IExpression
    {
        private Token _token;

        public LiteralExpr(Token token)
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
            else if (_token.Type == TokenType.FLOAT_LITERAL)
                return new FloatResult(double.Parse(_token.Value.Replace('.', ',')));
            else return new StringResult(_token.Value);
        }
    }
}
