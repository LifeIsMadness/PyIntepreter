using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class LiteralExpr : IExpression
    {
        private readonly Token _token;

        public Token Token => _token;

        public LiteralExpr(Token token)
        {
            _token = token;
        }

        public void Accept(IVisitor visitor)
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
            else if (_token.Type == TokenType.TRUE)
                return new BoolResult(true);
            else if (_token.Type == TokenType.FALSE)
                return new BoolResult(false);
            else return new StringResult(_token.Value);
        }
    }
}
