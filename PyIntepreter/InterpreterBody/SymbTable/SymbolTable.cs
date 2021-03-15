using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.SymbTable
{
    public class SymbolTable
    {
        private Dictionary<string, Variable> _vars = new Dictionary<string, Variable>();

        public Dictionary<string, Variable> Dict { get => _vars; }

        public bool HasVariable(string name)
        {
            return _vars.ContainsKey(name);
        }

        public Variable GetVariable(string name)
        {
            return _vars[name];
        }

        public void SetVariable(string name, Variable variable)
        {
            _vars[name] = variable;
        }
    }
}
