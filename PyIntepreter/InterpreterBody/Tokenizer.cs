using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PyInterpreter.InterpreterBody
{
    /// <summary>
    /// Tokenizer or scanner does lexical analysis of text,
    /// then generates a stream of tokens.
    /// </summary>
    public class Tokenizer
    {

        private readonly string _text;

        private int _pos = 0;

        private char _currentChar;

        public Tokenizer(string text)
        {
            _text = text;
            _currentChar = _text[_pos];
        }

        private void Error() => throw new Exception("Invalid character");

        private bool IsEOF() => _pos > _text.Length - 1;

        private bool IsEOF(int nextPos) => nextPos > _text.Length - 1;

        /// <summary>
        /// Indicates if there are some characters left and sets current character.
        /// </summary>
        private void Move()
        {
            _pos++;
            if (!IsEOF()) _currentChar = _text[_pos];
        }
            
        private char Peek()
        {
            var nextPos = _pos + 1;
            return _text[nextPos];
        }

        public void skipWhiteSpaces()
        {
            while (!IsEOF() && char.IsWhiteSpace(_currentChar))
            {
                Move();
            }
        }

        private string GetInteger()
        {
            string result = string.Empty;
            while (!IsEOF() && char.IsDigit(_currentChar))
            {
                result += _currentChar;
                Move();
            }

            return result;
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
            return token;
        }

        private bool IsAssignment()
        {
            var nextPos = _pos + 1;
            return (!IsEOF(nextPos) && _text[nextPos] != '=');
        }

        public Token GetNextToken()
        {          
            while(!IsEOF())
            {
                if (_currentChar == '\n')
                {
                    Move();
                    return new Token(TokenType.ENDLINE, "\n");
                }

                if (char.IsWhiteSpace(_currentChar))
                {
                    skipWhiteSpaces();
                    continue;
                }

                if (char.IsDigit(_currentChar))
                    return new Token(TokenType.INTEGER, GetInteger());

                if (_currentChar == '+')
                {
                    Move();
                    return new Token(TokenType.PLUS, "+");
                }

                if (_currentChar == '-')
                {
                    Move();
                    return new Token(TokenType.MINUS, "-");
                }

                if (_currentChar == '*')
                {
                    Move();
                    return new Token(TokenType.MUL, "*");
                }

                if (_currentChar == '/')
                {
                    Move();
                    return new Token(TokenType.DIV, "/");
                }

                if (_currentChar == '(')
                {
                    Move();
                    return new Token(TokenType.OPEN_PARANTHESIS, "(");
                }

                if (_currentChar == ')')
                {
                    Move();
                    return new Token(TokenType.CLOSE_PARANTHESIS, ")");
                }

                if (char.IsLetter(_currentChar))
                    return GetKeywordOrVariable();

                if (_currentChar == '=' && IsAssignment())
                {
                    Move();
                    return new Token(TokenType.ASSIGN, "=");
                }

                Error();
            }

            return new Token(TokenType.EOF, string.Empty);
        }
    }
}
