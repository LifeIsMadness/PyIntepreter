using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody
{
    public class Keywords
    {
        public static Dictionary<string, TokenType> ReservedWords => new Dictionary<string, TokenType>
        {

        };

        public static TokenType GetKeyword(string keyword) => 
            ReservedWords.GetValueOrDefault(keyword, TokenType.ID);
    }
}
