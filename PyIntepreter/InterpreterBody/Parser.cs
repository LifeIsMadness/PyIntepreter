using PyInterpreter.InterpreterBody.Expressions;
using PyInterpreter.InterpreterBody.SymbTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PyInterpreter.InterpreterBody
{
    /// <summary>
    /// Arithmetic expression parser.
    /// Rules:
    /// program: statement_list
    /// compound_statement: if_statement
    ///                     | for_statement
    ///                     | while_statement
    /// if_statement: IF disjunction COLON block 
    ///               (ELIF disjunction COLON block)*
    ///               (ELSE COLON block)*
    /// for_statement: FOR variable IN disjunction COLON block
    /// while_statement: WHILE disjunction COLON block
    /// block: NEWLINE INDENT statement_list DEDENT
    /// statement_list: (statement NEWLINE)+ | statement EOF
    /// statement: compound_statement 
    ///            | assignment_statement
    ///            | function_call
    ///            | empty
    /// assignment_statement: disjunction (LBRACKET expr RBRACKET)* ASSIGN disjunction
    /// function_call: ID LPAREN (disjunction (COMMA disjunction)*) RPAREN?
    /// empty:
    /// disjuction: conjunction (OR conjunction)*
    /// conjunction: compr (AND compr)*
    /// compr: expr ((EQUAL 
    ///               | NOT_EQUAL
    ///               | GREATER
    ///               | LESSER
    ///               | GREATER_EQUAL
    ///               | LESSER_EQUAL) expr)*
    /// expr: term ((PLUS | MINUS) term)*
    /// term: factor ((MUL | DIV) factor)*
    /// factor: PLUS factor
    ///         | MINUS factor
    ///         | primary
    /// primary: atom (LBRACKET disjunction RBRACKET)*  
    ///          | atom LPAREN (disjunction (COMMA disjunction)*)? RPAREN
    /// atom: INTEGER_LITERAL
    ///         | FLOAT_LITERAL
    ///         | STRING_LITERAL
    ///         | LPAREN disjunction RPAREN
    ///         | variable
    ///         | list
    /// variable: ID
    /// list: LBRACKET (disjunction (COMMA disjunction)*)? RBRACKET
    /// </summary>
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



        /// <summary>
        /// list: LBRACKET (disjunction (COMMA disjunction)*)? RBRACKET
        /// </summary>
        private IExpression @List()
        {
            Eat(TokenType.OPEN_BRACKETS);
            List<IExpression> items = new List<IExpression>();

            // empty list
            if (_currentToken.Type != TokenType.CLOSE_BRACKETS)
            {
                items.Add(Disjunction());
                while (_currentToken.Type == TokenType.COMMA)
                {
                    Eat(TokenType.COMMA);
                    items.Add(Disjunction());
                }
            }
            try
            {
                Eat(TokenType.CLOSE_BRACKETS);
            }
            catch(Exception ex) 
            {
                Error("List should ends with ']'");
            }
            return new ListExpr(items) {LineNumber = _tokenizer.LineNumber};
        }

        /// <summary>
        /// variable: ID
        /// </summary>
        private IExpression Variable()
        {
            IExpression result = new VariableExpr(_currentToken.Value) {LineNumber = _tokenizer.LineNumber};
            Eat(TokenType.ID);
            return result;
        }

        /// <summary>
        /// atom: INTEGER_LITERAL
        ///         | FLOAT_LITERAL
        ///         | STRING_LITERAL
        ///         | TRUE
        ///         | FALSE
        ///         | LPAREN disjunction RPAREN
        ///         | variable
        ///         | list
        /// </summary>
        private IExpression Atom()
        {
            var token = _currentToken;
            IExpression result = null;
            switch (token.Type)
            {

                case TokenType.INTEGER_LITERAL:
                    Eat(TokenType.INTEGER_LITERAL);
                    result = new LiteralExpr(token) {LineNumber = _tokenizer.LineNumber};
                    break;

                case TokenType.FLOAT_LITERAL:
                    Eat(TokenType.FLOAT_LITERAL);
                    result = new LiteralExpr(token) {LineNumber = _tokenizer.LineNumber};
                    break;

                case TokenType.STRING_LITERAL:
                    Eat(TokenType.STRING_LITERAL);
                    result = new LiteralExpr(token) {LineNumber = _tokenizer.LineNumber};
                    break;

                case TokenType.TRUE:
                    Eat(TokenType.TRUE);
                    result = new LiteralExpr(token) {LineNumber = _tokenizer.LineNumber};
                    break;

                case TokenType.FALSE:
                    Eat(TokenType.FALSE);
                    result = new LiteralExpr(token) {LineNumber = _tokenizer.LineNumber};
                    break;

                case TokenType.OPEN_PARANTHESIS:
                    Eat(TokenType.OPEN_PARANTHESIS);
                    result = Disjunction();
                    Eat(TokenType.CLOSE_PARANTHESIS);
                    break;

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
        /// primary: atom (LBRACKET disjunction RBRACKET)*
        ///          | LPAREN (disjunction (COMMA disjunction)*)? RPAREN
        /// </summary>
        private IExpression Primary()
        {
            var result = Atom();

            switch (_currentToken.Type)
            {
                case TokenType.OPEN_BRACKETS:
                    while (_currentToken.Type == TokenType.OPEN_BRACKETS)
                    {
                        Eat(TokenType.OPEN_BRACKETS);
                        result = new IndexExpr(result, Disjunction()) {LineNumber = _tokenizer.LineNumber};
                        Eat(TokenType.CLOSE_BRACKETS);
                    }
                    break;

                case TokenType.OPEN_PARANTHESIS:
                    Eat(TokenType.OPEN_PARANTHESIS);
                    List<IExpression> args = new List<IExpression>();
                    if (_currentToken.Type != TokenType.CLOSE_PARANTHESIS)
                    {
                        args.Add(Disjunction());
                        while (_currentToken.Type == TokenType.COMMA)
                        {
                            Eat(TokenType.COMMA);
                            args.Add(Disjunction());
                        }
                    }

                    Eat(TokenType.CLOSE_PARANTHESIS);
                    result =  new FunctionExpr(result, args) {LineNumber = _tokenizer.LineNumber};
                    break;
            }

            return result;
        }

        /// <summary>
        /// factor: PLUS factor
        ///         | MINUS factor
        ///         | primary
        /// </summary>
        private IExpression Factor()
        {
            var token = _currentToken;
            //IExpression result = null;
            switch (token.Type)
            {
                case TokenType.PLUS:
                    Eat(TokenType.PLUS);
                    return new PlusExpr(Factor()) {LineNumber = _tokenizer.LineNumber};

                case TokenType.MINUS:
                    Eat(TokenType.MINUS);
                    return new MinusExpr(Factor()) {LineNumber = _tokenizer.LineNumber};

                default:
                    return Primary();
            }
           // return result;
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
                    Eat(TokenType.MUL);
                    //result *= Factor();
                    result = new MulExpr(result, Factor()) {LineNumber = _tokenizer.LineNumber};
                }
                else if (token.Type == TokenType.DIV)
                {
                    Eat(TokenType.DIV);
                    result = new DivExpr(result, Factor()) {LineNumber = _tokenizer.LineNumber};
                    //result /= Factor();
                }

            }

            return result;
        }

        /// expr: term ((PLUS | MINUS) term)*
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
                    result = new AddExpr(result, Term()) {LineNumber = _tokenizer.LineNumber};
                }
                else if (token.Type == TokenType.MINUS)
                {
                    Eat(TokenType.MINUS);
                    result = new SubExpr(result, Term()) {LineNumber = _tokenizer.LineNumber};
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
                    result = new EqualExpr(result, Expr()) {LineNumber = _tokenizer.LineNumber};
                }
                else if (token.Type == TokenType.NOT_EQUAL)
                {
                    Eat(TokenType.NOT_EQUAL);
                    result = new NotEqualExpr(result, Expr()) {LineNumber = _tokenizer.LineNumber};
                }
                else if (token.Type == TokenType.GREATER)
                {
                    Eat(TokenType.GREATER);
                    result = new GreaterExpr(result, Expr()) {LineNumber = _tokenizer.LineNumber};
                }
                else if (token.Type == TokenType.LESSER)
                {
                    Eat(TokenType.LESSER);
                    result = new LesserExpr(result, Expr()) {LineNumber = _tokenizer.LineNumber};
                }
                else if (token.Type == TokenType.GREATER_EQUAL)
                {
                    Eat(TokenType.GREATER_EQUAL);
                    result = new GreaterEqualExpr(result, Expr()) {LineNumber = _tokenizer.LineNumber};
                }
                else if (token.Type == TokenType.LESSER_EQUAL)
                {
                    Eat(TokenType.LESSER_EQUAL);
                    result = new LesserEqualExpr(result, Expr()) {LineNumber = _tokenizer.LineNumber};
                }
            }
            return result;
        }

        private IExpression Conjuction()
        {
            var result = Compr();
            while (_currentToken.Type == TokenType.AND)
            {
                Eat(TokenType.AND);
                result = new AndExpr(result, Compr()) {LineNumber = _tokenizer.LineNumber};
            }

            return result;
        }

        private IExpression Disjunction()
        {
            var result = Conjuction();
            while (_currentToken.Type == TokenType.OR)
            {
                Eat(TokenType.OR);
                result = new OrExpr(result, Conjuction()) {LineNumber = _tokenizer.LineNumber};
            }

            return result;
        }

        /// <summary>
        /// empty:
        /// </summary>
        private IExpression Empty()
        {
            return new EmptyExpr();
        }

        /// <summary>
        /// function_call: ID LPAREN (disjunction (COMMA disjunction)*)? RPAREN
        /// </summary>
        private IExpression FunctionCall()
        {
            var token = _currentToken;
            var name = Variable();

            // Eat(TokenType.ID);
            Eat(TokenType.OPEN_PARANTHESIS);

            List<IExpression> args = new List<IExpression>();
            if (_currentToken.Type != TokenType.CLOSE_PARANTHESIS)
            {
                args.Add(Disjunction());
                while (_currentToken.Type == TokenType.COMMA)
                {
                    Eat(TokenType.COMMA);
                    args.Add(Disjunction());
                }
            }

            try
            {
                Eat(TokenType.CLOSE_PARANTHESIS);
            }
            catch (Exception ex)
            {
                Error("Expecting ')' at the end of a function call");
                
            }

            return new FunctionExpr(name, args) {LineNumber = _tokenizer.LineNumber};

        }

        /// <summary>
        /// assignment_statement: disjunction ASSIGN disjunction
        /// </summary>
        private IExpression AssignmentStatement()
        {
            var token = _currentToken;
            var left = Disjunction();
            
            Eat(TokenType.ASSIGN);
            var expr = Disjunction();
            return new AssignExpr(left, expr) {LineNumber = _tokenizer.LineNumber};
        }

        /// <summary>
        /// statement: function_call
        ///            | assignment_statement 
        ///            | empty
        /// </summary>
        private IExpression Statement()
        {
            IExpression result = null;
            if (_currentToken.Type == TokenType.ID
                && _tokenizer.CurrentChar == '(')
            {
                result = FunctionCall();
            }
            else if (_currentToken.Type == TokenType.ID)
            {
                result = AssignmentStatement();
            }
            else if (_currentToken.Type == TokenType.IF
                     || _currentToken.Type == TokenType.FOR
                     || _currentToken.Type == TokenType.WHILE)
            {
                result = CompoundStatement();
            }
            else if (_currentToken.Type == TokenType.EOF
                     || _currentToken.Type == TokenType.ENDLINE)
            {
                result = Empty();
            }
            else Error("Expected statement(assign, compound, function call, blank line)");

            return result;
        }

        /// <summary>
        /// statement_list: (statement NEWLINE)+ | statement EOF
        /// </summary>
        private IExpression StatementList()
        {
            var result = Statement();
            List<IExpression> results = new List<IExpression> { result };
            
            while (_currentToken.Type == TokenType.ENDLINE 
                   && _tokenizer.IndentLevel == _indents.Peek())/*(_currentToken.Type == TokenType.ENDLINE)*/
            {
                Eat(TokenType.ENDLINE);
                results.Add(Statement());
            }

            if (_currentToken.Type != TokenType.EOF
                && _currentToken.Type != TokenType.ENDLINE)
            {
                Error("Expected operator");
            }

            return new StatementListExpr(results) {LineNumber = _tokenizer.LineNumber};
        }

        /// <summary>
        /// block: NEWLINE INDENT statement_list DEDENT
        /// </summary>
        private IExpression Block()
        {
            var outerIndent = _tokenizer.PrevIndentLevel;
            var innerIndent = _tokenizer.IndentLevel;
            Eat(TokenType.ENDLINE);
            
            // INDENT
            if (_indents.Peek() == innerIndent)
                Error("Expected an indented block");

            _indents.Push(innerIndent);

            var statements = StatementList();
            if (_indents.Peek() < _tokenizer.IndentLevel && _currentToken.Type != TokenType.EOF)
                Error("Unexpected indent");

            while (_indents.Peek() > outerIndent)
                _indents.Pop();

            return statements;

        }

        /// <summary>
        /// if_statement: IF disjunction COLON block_statement 
        ///               (ELIF disjunction COLON block_statement)*
        ///               (ELSE COLON block_statement)*
        /// </summary>
        private IExpression IfStatement()
        {
            List<IExpression> comprs = new List<IExpression>();
            List<IExpression> statements = new List<IExpression>();
            void ifBlock()
            {
                Eat(TokenType.COLON);
                statements.Add(Block());
            }

            Eat(TokenType.IF);
            comprs.Add(Disjunction());
            ifBlock();

            while (_currentToken.Type == TokenType.ELIF)
            {
                Eat(TokenType.ELIF);
                comprs.Add(Disjunction());
                ifBlock();
            }

            if(_currentToken.Type == TokenType.ELSE)
            {
                Eat(TokenType.ELSE);
                ifBlock();
            }

            return new IfExpr(comprs, statements) {LineNumber = _tokenizer.LineNumber};
        }

        /// <summary>
        /// for_statement: FOR variable IN disjunction COLON block
        /// </summary>
        private IExpression ForStatement()
        {
            Eat(TokenType.FOR);
            var name = Variable();
            Eat(TokenType.IN);
            var iterable = Disjunction();
            Eat(TokenType.COLON);
            var statements = Block();

            return new ForExpr(name, iterable, statements) {LineNumber = _tokenizer.LineNumber};
        }

        /// <summary>
        /// while_statement: WHILE compr COLON block
        /// </summary>
        private IExpression WhileStatement()
        {
            Eat(TokenType.WHILE);
            var condition = Disjunction();
            Eat(TokenType.COLON);
            var statements = Block();

            return new WhileExpr(condition, statements) {LineNumber = _tokenizer.LineNumber};
        }

        /// <summary>
        /// compound_statement: if_statement
        ///                     | for_statement
        ///                     | while_statement
        /// </summary>
        private IExpression CompoundStatement()
        {
            IExpression result = null;

            switch (_currentToken.Type)
            {
                case TokenType.IF:
                    result = IfStatement();
                    break;

                case TokenType.FOR:
                    result = ForStatement();
                    break;

                case TokenType.WHILE:
                    result = WhileStatement();
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

            if (_indents.Peek() != _tokenizer.IndentLevel 
                && _currentToken.Type != TokenType.EOF)
                Error("Unexpected indent");

            return new ProgramExpr(statements) {LineNumber = _tokenizer.LineNumber};
        }

        public IExpression Parse()
        {
            var program = Program();
            //_tokenizer.PrintLexems();
            return program;
        }
    }
}
