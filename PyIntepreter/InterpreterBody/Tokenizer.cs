using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PyInterpreter.InterpreterBody
{
    class LexemTable
    {
        public Dictionary<string, string> KeyWords { get; set; } =
            new Dictionary<string, string>();

        public Dictionary<string, string> Operators { get; set; } =
            new Dictionary<string, string>();

        public Dictionary<string, string> Literals { get; set; } =
            new Dictionary<string, string>();

        public void AddLiteral(Token token)
        {
            switch (token.Type)
            {
                case TokenType.INTEGER_LITERAL:
                    Literals.TryAdd(token.Value, "literal of \"int\" type");
                    break;

                case TokenType.FLOAT_LITERAL:
                    Literals.TryAdd(token.Value, "literal of \"float\" type");
                    break;
            }
        }

        public void AddKeyword(Token token)
        {

        }

        public void AddOperator(Token token)
        {
            switch (token.Type)
            {
                case TokenType.PLUS:
                case TokenType.MINUS:
                case TokenType.MUL:
                case TokenType.DIV:
                case TokenType.OPEN_PARANTHESIS:
                case TokenType.CLOSE_PARANTHESIS:
                case TokenType.ASSIGN:
                    Operators.TryAdd(token.Value, "arithmetic operator");
                    break;
            }
        }

        private void PrintRow(KeyValuePair<string, string> row)
        {
            Console.WriteLine($" {row.Key}\t|{row.Value, -30}|");
        }

        public void ShowTables()
        {
            var border = "---------------------------------------";
            Console.WriteLine(border);
            Console.WriteLine("|\tLiterals\t\t       |");
            Console.WriteLine(border);
            foreach (var row in Literals)
            {
                PrintRow(row);
            }
            Console.WriteLine(border);

            Console.WriteLine(border);
            Console.WriteLine("|\tOperators\t\t       |");
            Console.WriteLine(border);
            foreach (var row in Operators)
            {
                PrintRow(row);
            }
            Console.WriteLine(border);

            Console.WriteLine(border);
            Console.WriteLine("|\tKeywords\t\t       |");
            Console.WriteLine(border);
            foreach (var row in KeyWords)
            {
                PrintRow(row);
            }
            Console.WriteLine(border);
        }

    }

    /// <summary>
    /// Tokenizer or scanner does lexical analysis of text,
    /// then generates a stream of tokens.
    /// </summary>
    public class Tokenizer
    {
        private readonly string _text;

        private int _pos = 0;

        private int _linePos = 0;

        private int _lineNumber = 0;

        private char _currentChar;

        private LexemTable _lexemTable = new LexemTable();

        public int LinePos { get => _linePos; }
        public int LineNumber { get => _lineNumber; }

        private int _indentLevel = 0;
        public int IndentLevel { get => _indentLevel; }

        public Tokenizer(string text)
        {
            _text = text;
            _currentChar = _text[_pos];
        }

        public void PrintLexems()
        {
            _lexemTable.ShowTables();
        }

        private void Error()
        {
            throw new Exception($"Invalid character '{_currentChar}' at line: {++_lineNumber} pos: {++_linePos}");
        }

        private void Error(string msg)
        {
            throw new Exception($"{msg} at line: {++_lineNumber} pos: {++_linePos}");
        }

        private bool IsEOF() => _pos > _text.Length - 1;

        private bool IsEOF(int nextPos) => nextPos > _text.Length - 1;

        /// <summary>
        /// Indicates if there are some characters left and sets current character.
        /// </summary>
        private void Move()
        {
            _pos++;
            _linePos++;

            if (_currentChar == '\n')
            {
                _lineNumber++;
                _linePos = 0;
            }

            if (!IsEOF()) _currentChar = _text[_pos];
        }
            
        private char Peek()
        {
            var nextPos = _pos + 1;
            return _text[nextPos];
        }

        public void skipWhiteSpaces()
        {
            while (!IsEOF() && char.IsWhiteSpace(_currentChar)
                   && _currentChar != '\n')
            {
                Move();
            }
        }

        private void SetDigitResult(StringBuilder res)
        {
            while (!IsEOF() && char.IsDigit(_currentChar))
            {
                res.Append(_currentChar);
                Move();
            }
        }

        private Token GetNumber()
        {
            StringBuilder result = new StringBuilder();
            Token token = null;

            SetDigitResult(result);

            if (_currentChar == '.')
            {
                result.Append(_currentChar);
                Move();
                SetDigitResult(result);
                token = new Token(TokenType.FLOAT_LITERAL, result.ToString());
            }
            else
                token = new Token(TokenType.INTEGER_LITERAL, result.ToString());

            _lexemTable.AddLiteral(token);
            return token;
        }

        private Token GetString()
        {
            char terminatingChar = _currentChar == '\'' ? '\'' : '"';
            string result = string.Empty;

            bool isTerminated = false;
            while(!IsEOF())
            {
                Move();

                if (_currentChar == terminatingChar)
                {
                    isTerminated = true;
                    break;
                }
                result += _currentChar;

            }
            Move();

            if (!isTerminated)
                Error("EOL while scanning string literal");

            return new Token(TokenType.STRING_LITERAL, result);
        }

        private Token GetKeywordOrVariable()
        {
            string value = string.Empty;
            while (!IsEOF() && char.IsLetterOrDigit(_currentChar))
            {
                value += _currentChar;
                Move();
            }

            var token = new Token(Keywords.GetKeyword(value), value);
            _lexemTable.AddKeyword(token);
            return token;
        }

        private bool IsAssignment()
        {
            var nextPos = _pos + 1;
            return (!IsEOF(nextPos) && _text[nextPos] != '=');
        }

        private Token GetComparisonOperator()
        {
            string[] lexems = {"==", "!=", ">", "<", ">=", "<="};
            string value = string.Empty;

            while (!IsEOF() && !char.IsWhiteSpace(_currentChar))
            {
                value += _currentChar;
                Move();
            }

            // Лексическая ошибка 1)
            if (!lexems.Contains(value))
                Error($"Expected comparison operator");

            switch (value)
            {
                case "==":
                    return new Token(TokenType.EQUAL, value);
                case "!=":
                    return new Token(TokenType.NOT_EQUAL, value);
                case ">":
                    return new Token(TokenType.GREATER, value);
                case "<":
                    return new Token(TokenType.LESSER, value);
                case ">=":
                    return new Token(TokenType.GREATER_EQUAL, value);
                case "<=":
                        return new Token(TokenType.LESSER_EQUAL, value);
                default:
                    return null;
            }
        }

        private void SetIdentation()
        {
            while(_currentChar == '\t')
            {
                _indentLevel++;
            }

        }

        public Token GetNextToken()
        {          
            while(!IsEOF())
            {
                if(_currentChar == '\t' && _linePos == 0)
                {
                    SetIdentation();
                }

                if (_currentChar == '\n')
                {
                    Move();
                    return new Token(TokenType.ENDLINE, "\n");
                }

                if (char.IsWhiteSpace(_currentChar))
                {
                    skipWhiteSpaces();
                    // check for EOF again
                    continue;
                }

                if (char.IsDigit(_currentChar))
                    return GetNumber();

                if (_currentChar == '\''
                    || _currentChar == '"')
                    return GetString();

                if (_currentChar == '+')
                {
                    Move();
                    var token = new Token(TokenType.PLUS, "+");
                    _lexemTable.AddOperator(token);
                    return token;
                }

                if (_currentChar == '-')
                {
                    Move();
                    var token = new Token(TokenType.MINUS, "-");
                    _lexemTable.AddOperator(token);
                    return token;
                }

                if (_currentChar == '*')
                {
                    Move();
                    var token = new Token(TokenType.MUL, "*");
                    _lexemTable.AddOperator(token);
                    return token;
                }

                if (_currentChar == '/')
                {
                    Move();
                    var token = new Token(TokenType.DIV, "/");
                    _lexemTable.AddOperator(token);
                    return token;
                }

                if (_currentChar == '(')
                {
                    Move();
                    var token = new Token(TokenType.OPEN_PARANTHESIS, "(");
                    _lexemTable.AddOperator(token);
                    return token;
                }

                if (_currentChar == ')')
                {
                    Move();
                    var token = new Token(TokenType.CLOSE_PARANTHESIS, ")");
                    _lexemTable.AddOperator(token);
                    return token;
                }

                if (char.IsLetter(_currentChar))
                    return GetKeywordOrVariable();

                if (_currentChar == '=' && IsAssignment())
                {            
                    Move();
                    var token = new Token(TokenType.ASSIGN, "=");
                    _lexemTable.AddOperator(token);
                    return token;
                }

                if (_currentChar == '='
                    || _currentChar == '!'
                    || _currentChar == '>'
                    || _currentChar == '<')
                {
                    var token = GetComparisonOperator();
                    return token;
                }

                if (_currentChar == '[')
                {
                    Move();
                    return new Token(TokenType.OPEN_BRACKETS, "[");
                }

                if (_currentChar == ']')
                {
                    Move();
                    return new Token(TokenType.CLOSE_BRACKETS, "]");
                }

                if (_currentChar == ',')
                {
                    Move();
                    return new Token(TokenType.COMMA, ",");
                }

                if (_currentChar == ':')
                {
                    Move();
                    return new Token(TokenType.COLON, ":");
                }

                Error();
            }

            return new Token(TokenType.EOF, string.Empty);
        }
    }
}
