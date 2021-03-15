using PyInterpreter.InterpreterBody.Expressions;
using PyInterpreter.InterpreterBody.SymbTable;
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

        private SymbTable.SymbolTable _symbolTable;

        public SymbolTable SymbolTable { get => _symbolTable; set => _symbolTable = value; }

        // TODO: How to handle if the input will be '3   3';
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
        /// variable: ID
        /// </summary>
        private IExpression Variable()
        {
            IExpression result = new VariableExpr(_currentToken.Value, _symbolTable);
            eat(TokenType.ID);
            return result;
        }

        /// <summary>
        /// factor: PLUS factor
        ///         | MINUS factor 
        ///         | INTEGER_LITERAL 
        ///         | FLOAT_LITERAL
        ///         | LPARAN expr RPAREN
        ///         | variable
        /// </summary>
        private IExpression Factor()
        {
            var token = _currentToken;
            IExpression result = null;
            switch(token.Type)
            {
                case TokenType.INTEGER_LITERAL:
                    eat(TokenType.INTEGER_LITERAL);
                    result = new NumberExpr(token);
                    break;

                case TokenType.FLOAT_LITERAL:
                    eat(TokenType.FLOAT_LITERAL);
                    result = new NumberExpr(token);
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

        /// <summary>
        /// empty:
        /// </summary>
        /// <returns></returns>

        private IExpression Empty()
        {
            return new EmptyExpr();
        }

        /// <summary>
        /// assignment_statement: variable ASSIGN expr
        /// </summary>
        private IExpression AssignmentStatement()
        {
            var token = _currentToken;
            var name = Variable();
            eat(TokenType.ASSIGN);
            var expr = Expr();
            return new AssignExpr(name, expr, SymbolTable);
        }

        /// <summary>
        /// statement: assignment_statement | empty
        /// </summary>
        private IExpression Statement()
        {
            IExpression result = null;
            if (_currentToken.Type == TokenType.ID)
            {
                result = AssignmentStatement();
            }
            else
            {
                result = Empty();    
            }
            return result;
        }

        /// <summary>
        /// statement_list: statement | statement NEWLINE statement_list
        /// </summary>
        private List<IExpression> StatementList()
        {
            var result = Statement();
            List<IExpression> results = new List<IExpression> { result };
            while (_currentToken.Type == TokenType.ENDLINE)
            {
                eat(TokenType.ENDLINE);
                results.Add(Statement());
            }

            return results;
        }

        /// <summary>
        /// program: statement_list
        /// </summary>
        private IExpression Program()
        {
            return new ProgramExpr(StatementList());
        }

        public IExpression Parse()
        {
            var program = Program();
            _tokenizer.PrintLexems();
            return program;
        }
    }
}
