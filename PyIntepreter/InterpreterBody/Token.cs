using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody
{
    public enum TokenType
    {
        // Types
        INTEGER_LITERAL,
        FLOAT_LITERAL,
        STRING_LITERAL,
        // Numerical operators
        PLUS,
        MINUS,
        MUL,
        DIV,
        OPEN_PARANTHESIS,
        CLOSE_PARANTHESIS,
        // Comparison operators
        EQUAL,
        NOT_EQUAL,
        GREATER,
        LESSER,
        GREATER_EQUAL,
        LESSER_EQUAL,

        // Keywords
        IF,
        ELIF,
        ELSE,
        // Variables
        ID,
        ASSIGN,
        // Collections
        OPEN_BRACKETS,
        CLOSE_BRACKETS,
        LIST,
        // Others
        INDENT,
        COMMA,
        COLON,
        ENDLINE,
        EOF,

    }

    public class Token
    {
        public TokenType Type { get; }

        public string Value { get; } 

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
