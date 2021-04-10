using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody
{
    public class Keywords
    {
        public static Dictionary<string, TokenType> ReservedWords => new Dictionary<string, TokenType>
        {
            ["if"] = TokenType.IF,
            ["elif"] = TokenType.ELIF,
            ["else"] = TokenType.ELSE,
        };

        public static TokenType GetKeyword(string keyword) => 
            ReservedWords.GetValueOrDefault(keyword, TokenType.ID);
    }
}
