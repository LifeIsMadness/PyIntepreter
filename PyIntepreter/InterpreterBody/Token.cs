using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody
{
    public enum TokenType
    {
        INTEGER,
        PLUS,
        MINUS,
        MUL,
        DIV,
        EOF,
        OPEN_PARANTHESIS,
        CLOSE_PARANTHESIS,

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
