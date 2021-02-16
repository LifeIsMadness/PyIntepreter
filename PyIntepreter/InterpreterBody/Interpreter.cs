using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PyInterpreter.InterpreterBody
{
    public class Interpreter
    {
        private Tokenizer _tokenizer;

        private Token _currentToken;

        // TODO: How to handle if the input will be '3   3';
        // private Token _prevToken;

        public Interpreter(Tokenizer tokenizer)
        {
            _tokenizer = tokenizer;
            _currentToken = tokenizer.GetNextToken();
        }

        private void Error() => throw new Exception("Invalid syntax");

        private void eat(TokenType type)
        {
            if (type == _currentToken.Type)
                _currentToken = _tokenizer.GetNextToken();
            else Error();
        }

        /// <summary>
        /// factor: INTEGER | LPARAN expr RPAREN
        /// </summary>
        private int Factor()
        {
            var token = _currentToken;
            int result = 0;
            if (token.Type == TokenType.INTEGER)
            {
                eat(TokenType.INTEGER);
                result = int.Parse(token.Value);
            }
            else if (token.Type == TokenType.OPEN_PARANTHESIS)
            {
                eat(TokenType.OPEN_PARANTHESIS);
                result = Expr();
                eat(TokenType.CLOSE_PARANTHESIS);
            }
            // TODO: if the input is `3 + ` or `+ 3`
            else Error();

            return result;
        }

        /// <summary>
        /// term: factor ((MUL | DIV) factor)*
        /// </summary>
        private int Term()
        {
            TokenType[] operations = { TokenType.MUL, TokenType.DIV };

            var result = Factor();
            while (operations.Contains(_currentToken.Type))
            {
                var token = _currentToken;
                if (token.Type == TokenType.MUL)
                {
                    eat(TokenType.MUL);
                    result *= Factor();
                }
                else if (token.Type == TokenType.DIV)
                {
                    eat(TokenType.DIV);
                    result /= Factor();
                }
            }

            return result;
        }

        /// <summary>
        /// Arithmetic expression parser.
        /// Rules:
        /// expr: term ((PLUS | MINUS) term)*
        /// term: factor ((MUL | DIV) factor)*
        /// factor: INTEGER
        /// </summary>
        public int Expr()
        {
            TokenType[] operations =  { TokenType.PLUS, TokenType.MINUS };
            
            var result = Term();
            while (operations.Contains(_currentToken.Type))
            {
                var token = _currentToken;
                if (token.Type == TokenType.PLUS)
                {
                    eat(TokenType.PLUS);
                    result += Term();
                }
                else if (token.Type == TokenType.MINUS)
                {
                    eat(TokenType.MINUS);
                    result -= Term();
                }
            }

            return result;
        }
    }
}
