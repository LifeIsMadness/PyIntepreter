using PyInterpreter.InterpreterBody.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PyInterpreter.InterpreterBody
{
    public class Parser
    {
        private Tokenizer _tokenizer;

        private Token _currentToken;

        // TODO: How to handle if the input will be '3   3';
        // private Token _prevToken;
        public Parser(Tokenizer tokenizer)
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
        /// 
        /// </summary>
        /// <returns></returns>
        private IExpression Variable()
        {
            return null;
        }

        /// <summary>
        /// factor: (PLUS | MINUS) factor | INTEGER | LPARAN expr RPAREN
        /// </summary>
        private IExpression Factor()
        {
            var token = _currentToken;
            IExpression result = null;
            switch(token.Type)
            {
                case TokenType.INTEGER:
                    eat(TokenType.INTEGER);
                    result = new NumberExpr(int.Parse(token.Value));
                    break;

                case TokenType.OPEN_PARANTHESIS:
                    eat(TokenType.OPEN_PARANTHESIS);
                    result = Expr();
                    eat(TokenType.CLOSE_PARANTHESIS);
                    break;

                case TokenType.PLUS:
                    eat(TokenType.PLUS);
                    result = new PlusExpr(Factor());
                    break;

                case TokenType.MINUS:
                    eat(TokenType.MINUS);
                    result = new MinusExpr(Factor());
                    break;

                case TokenType.ID:
                    result = Variable();
                    break;

                default:
                    Error();
                    break;
            }

            return result;
        }

        /// <summary>
        /// term: factor ((MUL | DIV) factor)*
        /// </summary>
        private IExpression Term()
        {
            TokenType[] operations = { TokenType.MUL, TokenType.DIV };

            var result = Factor();
            while (operations.Contains(_currentToken.Type))
            {
                var token = _currentToken;
                if (token.Type == TokenType.MUL)
                {
                    eat(TokenType.MUL);
                    //result *= Factor();
                    result = new MulExpr(result, Factor());
                }
                else if (token.Type == TokenType.DIV)
                {
                    eat(TokenType.DIV);
                    result = new DivExpr(result, Factor());
                    //result /= Factor();
                }

            }

            return result;
        }

        /// <summary>
        /// Arithmetic expression parser.
        /// Rules:
        /// program: statement_list
        /// statement_list: statement | statement NEWLINE statement_list
        /// statement: assignment_statement | empty
        /// assignment_statement: variable ASSIGN expr
        /// empty:
        /// expr: term ((PLUS | MINUS) term)*
        /// term: factor ((MUL | DIV) factor)*
        /// factor: (PLUS | MINUS) factor 
        ///         | INTEGER 
        ///         | LPARAN expr RPAREN
        ///         | variable
        /// variable: ID
        /// </summary>
        private IExpression Expr()
        {
            TokenType[] operations = { TokenType.PLUS, TokenType.MINUS };

            var result = Term();
            while (operations.Contains(_currentToken.Type))
            {
                var token = _currentToken;
                if (token.Type == TokenType.PLUS)
                {
                    eat(TokenType.PLUS);
                    //result += Term();
                    result = new AddExpr(result, Term());
                }
                else if (token.Type == TokenType.MINUS)
                {
                    eat(TokenType.MINUS);
                    //result -= Term();
                    result = new SubExpr(result, Term());
                }
            }

            return result;
        }



        public IExpression Parse()
        {
            return Expr();
        }
    }
}
