using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody
{
    public enum TokenType
    {
        // Numerical operations
        INTEGER,
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
