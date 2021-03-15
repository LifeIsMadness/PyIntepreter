using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody
{
    public enum TokenType
    {
        // types
        INTEGER_LITERAL,
        FLOAT_LITERAL,
        // Numerical operations
        PLUS,
        MINUS,
        MUL,
        DIV,
        OPEN_PARANTHESIS,
        CLOSE_PARANTHESIS,

        // Keywords

        // Variables
        ID,
        ASSIGN,
        // Others
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
