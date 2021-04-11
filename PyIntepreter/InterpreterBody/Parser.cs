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

        private Stack<int> _indents = new Stack<int>();

        // TODO: How to handle if the input will be '3   3';
        public Parser(Tokenizer tokenizer)
        {
            _tokenizer = tokenizer;
            _currentToken = _tokenizer.GetNextToken();

            // pushing first indentLevel
            //_indents.Push(_tokenizer.IndentLevel);
        }

        private void Error() => throw new Exception($"Invalid syntax at line: " +
            $"{_tokenizer.LineNumber + 1} pos: {_tokenizer.LinePos + 1}");

        private void Error(string msg) => throw new Exception($"{msg} at line: " +
            $"{_tokenizer.LineNumber + 1} pos: {_tokenizer.LinePos + 1}");

        private void Eat(TokenType type)
        {
            if (type == _currentToken.Type)
                _currentToken = _tokenizer.GetNextToken();
            else Error();
        }

        private IExpression @Index()
        {
            switch (_currentToken.Type)
            {
                case TokenType.PLUS:
                    Eat(TokenType.PLUS);
                    return new PlusExpr(Index());

                case TokenType.MINUS:
                    Eat(TokenType.MINUS);
                    return new MinusExpr(Index());
            }

            var result = Factor();
            while (_currentToken.Type == TokenType.OPEN_BRACKETS)
            {
                Eat(TokenType.OPEN_BRACKETS);
                result = new IndexExpr(result, Expr());
                Eat(TokenType.CLOSE_BRACKETS);

                //if (_currentToken.Type != TokenType.OPEN_BRACKETS)
                //    break;
            }
            return result;
        }

        /// <summary>
        /// list: LBRACKET (expr COMMA)* RBRACKET
        /// </summary>
        private IExpression @List()
        {
            Eat(TokenType.OPEN_BRACKETS);
            List<IExpression> items = new List<IExpression>();

            // empty list
            if (_currentToken.Type != TokenType.CLOSE_BRACKETS)
            {
                items.Add(Expr());
                while (_currentToken.Type == TokenType.COMMA)
                {
                    Eat(TokenType.COMMA);
                    items.Add(Expr());
                }
            }

            Eat(TokenType.CLOSE_BRACKETS);
            return new ListExpr(items);
        }

        /// <summary>
        /// variable: ID
        /// </summary>
        private IExpression Variable()
        {
            IExpression result = new VariableExpr(_currentToken.Value);
            Eat(TokenType.ID);
            return result;
        }

        /// <summary>
        /// factor: INTEGER_LITERAL
        ///         | FLOAT_LITERAL
        ///         | STRING_LITERAL
        ///         | LPAREN expr RPAREN
        ///         | variable
        ///         | list
        /// </summary>
        private IExpression Factor()
        {
            var token = _currentToken;
            IExpression result = null;
            switch (token.Type)
            {
                case TokenType.INTEGER_LITERAL:
                    Eat(TokenType.INTEGER_LITERAL);
                    result = new LiteralExpr(token);
                    break;

                case TokenType.FLOAT_LITERAL:
                    Eat(TokenType.FLOAT_LITERAL);
                    result = new LiteralExpr(token);
                    break;

                case TokenType.STRING_LITERAL:
                    Eat(TokenType.STRING_LITERAL);
                    result = new LiteralExpr(token);
                    break;

                case TokenType.OPEN_PARANTHESIS:
                    Eat(TokenType.OPEN_PARANTHESIS);
                    result = Expr();
                    Eat(TokenType.CLOSE_PARANTHESIS);
                    break;

                //case TokenType.PLUS:
                //    Eat(TokenType.PLUS);
                //    result = new PlusExpr(Factor());
                //    break;

                //case TokenType.MINUS:
                //    Eat(TokenType.MINUS);
                //    result = new MinusExpr(Factor());
                //    break;

                case TokenType.ID:
                    result = Variable();

                    break;

                case TokenType.OPEN_BRACKETS:
                    result = List();

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

            var result = Index();
            while (operations.Contains(_currentToken.Type))
            {
                var token = _currentToken;
                if (token.Type == TokenType.MUL)
                {
                    Eat(TokenType.MUL);
                    //result *= Factor();
                    result = new MulExpr(result, Factor());
                }
                else if (token.Type == TokenType.DIV)
                {
                    Eat(TokenType.DIV);
                    result = new DivExpr(result, Factor());
                    //result /= Factor();
                }

            }

            return result;
        }

        /// <summary>
        /// compr: expr ((EQUAL 
        ///               | NOT_EQUAL
        ///               | GREATER
        ///               | LESSER
        ///               | GREATER_EQUAL
        ///               | LESSER_EQUAL) expr)*
        /// </summary>
        private IExpression Compr()
        {
            TokenType[] operations = {
                TokenType.EQUAL, TokenType.NOT_EQUAL,
                TokenType.GREATER, TokenType.LESSER,
                TokenType.GREATER_EQUAL, TokenType.LESSER_EQUAL,
            };
            var result = Expr();

            while (operations.Contains(_currentToken.Type))
            {
                var token = _currentToken;
                if (token.Type == TokenType.EQUAL)
                {
                    Eat(TokenType.EQUAL);
                    result = new EqualExpr(result, Expr());
                }
                else if (token.Type == TokenType.NOT_EQUAL)
                {
                    Eat(TokenType.NOT_EQUAL);
                    result = new NotEqualExpr(result, Expr());
                }
                else if (token.Type == TokenType.GREATER)
                {
                    Eat(TokenType.GREATER);
                    result = new GreaterExpr(result, Expr());
                }
                else if (token.Type == TokenType.LESSER)
                {
                    Eat(TokenType.LESSER);
                    result = new LesserExpr(result, Expr());
                }
                else if (token.Type == TokenType.GREATER_EQUAL)
                {
                    Eat(TokenType.GREATER_EQUAL);
                    result = new GreaterEqualExpr(result, Expr());
                }
                else if (token.Type == TokenType.LESSER_EQUAL)
                {
                    Eat(TokenType.LESSER_EQUAL);
                    result = new LesserEqualExpr(result, Expr());
                }
            }
            return result;
        }

        /// <summary>
        /// Arithmetic expression parser.
        /// Rules:
        /// program: statement_list
        /// compound_statement: if_statement
        /// if_statement: IF compr COLON statement_list 
        ///               (ELIF compr COLON statement_list)*
        ///               (ELSE compr COLON statement_list)*
        /// statement_list: statement | statement NEWLINE statement_list
        /// statement: compound_statement | assignment_statement | empty
        /// assignment_statement: variable ASSIGN compr
        /// empty:
        /// compr: expr ((EQUAL 
        ///               | NOT_EQUAL
        ///               | GREATER
        ///               | LESSER
        ///               | GREATER_EQUAL
        ///               | LESSER_EQUAL) expr)*
        /// expr: term ((PLUS | MINUS) term)*
        /// term: index ((MUL | DIV) index)*
        /// index: PLUS index
        ///        | MINUS index
        ///        | factor (LBRACKET expr RBRACKET)*
        /// factor: INTEGER_LITERAL
        ///         | FLOAT_LITERAL
        ///         | STRING_LITERAL
        ///         | LPAREN expr RPAREN
        ///         | variable
        ///         | list
        /// variable: ID
        /// list: LBRACKET (expr COMMA)* RBRACKET
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
                    Eat(TokenType.PLUS);
                    result = new AddExpr(result, Term());
                }
                else if (token.Type == TokenType.MINUS)
                {
                    Eat(TokenType.MINUS);
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
        /// assignment_statement: variable ASSIGN compr
        /// </summary>
        private IExpression AssignmentStatement()
        {
            var token = _currentToken;
            var name = Variable();
            Eat(TokenType.ASSIGN);
            var expr = Compr();
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
            else if (_currentToken.Type == TokenType.IF)
            {
                result = CompoundStatement();
            }
            else if (_currentToken.Type == TokenType.EOF
                     || _currentToken.Type == TokenType.ENDLINE)
            {
                result = Empty();
            }
            else Error("Expected statement(assign, blank line)");

            return result;
        }

        /// <summary>
        /// statement_list: statement | statement NEWLINE statement_list
        /// </summary>
        private IExpression StatementList()
        {
            var result = Statement();
            List<IExpression> results = new List<IExpression> { result };
            // TODO: syntax checking
            while (_currentToken.Type == TokenType.ENDLINE 
                   && _tokenizer.IndentLevel == _indents.Peek())/*(_currentToken.Type == TokenType.ENDLINE)*/
            {
                Eat(TokenType.ENDLINE);
                //if (_currentToken.Type == TokenType.ELIF
                //    || _currentToken.Type == TokenType.ELSE)
                //{
                //    return new StatementListExpr(results);
                //}
                // TODO: u can exit here if EOF 
                results.Add(Statement());
            }

            if (_currentToken.Type != TokenType.EOF
                && _currentToken.Type != TokenType.ENDLINE)
            {
                Error("Expected operator");
            }

            return new StatementListExpr(results);
        }

        /// <summary>
        /// block: NEWLINE INDENT statement_list DEDENT
        /// </summary>
        /// <returns></returns>
        private IExpression Block()
        {
            var indentLvl = _tokenizer.IndentLevel;
            Eat(TokenType.ENDLINE);

            // INDENT
            if (indentLvl == _tokenizer.IndentLevel)
                Error("Expected an indented block");

            _indents.Push(_tokenizer.IndentLevel);

            var statements = StatementList();
            if (_indents.Peek() < _tokenizer.IndentLevel)
                Error("Unexpected indent");

            while (_indents.Peek() > _tokenizer.IndentLevel)
                _indents.Pop();

            return statements;

        }

        /// <summary>
        /// if_statement: IF compr COLON NEWLINE statement_list 
        ///               (ELIF compr COLON NEWLINE statement_list)*
        ///               (ELSE COLON NEWLINE statement_list)*
        /// </summary>
        /// <returns></returns>
        private IExpression IfStatement()
        {
            List<IExpression> comprs = new List<IExpression>();
            List<IExpression> statements = new List<IExpression>();
            void ifBlock()
            {
                Eat(TokenType.COLON);
                //Eat(TokenType.ENDLINE);
                //statements.Add(StatementList());
                statements.Add(Block());
            }

            Eat(TokenType.IF);
            comprs.Add(Compr());
            ifBlock();

            while (_currentToken.Type == TokenType.ELIF)
            {
                Eat(TokenType.ELIF);
                comprs.Add(Compr());
                ifBlock();
            }

            if(_currentToken.Type == TokenType.ELSE)
            {
                Eat(TokenType.ELSE);
                ifBlock();
            }

            return new IfExpr(comprs, statements);
        }

        private IExpression CompoundStatement()
        {
            IExpression result = null;
            switch (_currentToken.Type)
            {
                case TokenType.IF:
                    result =  IfStatement();
                    break;
            }

            return result;
        }

        /// <summary>
        /// program: statement_list
        /// </summary>
        private IExpression Program()
        {
            _indents.Push(_tokenizer.IndentLevel);

            var statements = StatementList();

            if (_indents.Peek() != _tokenizer.IndentLevel)
                Error("Unexpected indent");
            //if (_currentToken.Type == TokenType.ELIF
            //    || _currentToken.Type == TokenType.ELSE)
            //{
            //    Error("Closest 'if' wasn't found");
            //}

            return new ProgramExpr(statements);
        }

        public IExpression Parse()
        {
            var program = Program();
            _tokenizer.PrintLexems();
            return program;
        }
    }
}
