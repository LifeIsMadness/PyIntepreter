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

        public IResult Interpret()
        {         
            if (_token.Type == TokenType.INTEGER)
            {
                return new IntResult(_token.Value);
            }
            else
                return null;
        }
    }
}
