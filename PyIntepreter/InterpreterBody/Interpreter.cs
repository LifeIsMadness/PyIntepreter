using PyInterpreter.InterpreterBody.Expressions;
using PyInterpreter.InterpreterBody.Expressions.Builtins;
using PyInterpreter.InterpreterBody.SymbTable;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PyInterpreter.InterpreterBody
{
    public class Interpreter
    {
        private readonly Parser _parser;

        private readonly ExpressionVisitor exprVisitor = new ExpressionVisitor();

        private readonly TreeDrawingVisitor treeDrawingVisitor = new TreeDrawingVisitor();

        protected SymbTable.SymbolTable _symbolTable;

        protected readonly Dictionary<string, FunctionBodyExpr> _builtins
            = new Dictionary<string, FunctionBodyExpr>
            {
                ["input"] = new InputFunctionExpr(),
                ["print"] = new PrintFunctionExpr(),
                ["range"] = new RangeFunctionExpr(),
                ["int"] = new IntFunctionExpr(),
                ["len"] = new LenFunctionExpr(),
            };

        public SymbolTable SymbolTable { get => _symbolTable; }

        public Dictionary<string, FunctionBodyExpr> Builtins => _builtins;

        private void Initialize()
        {
            exprVisitor.Builtins = treeDrawingVisitor.Builtins 
                = _builtins;
            exprVisitor.SymbolTable = _symbolTable;
            treeDrawingVisitor.SymbolTable = new SymbolTable();
        }

        public Interpreter(Parser parser): base()
        {
            _symbolTable = new SymbolTable();
            Initialize();
            _parser = parser;
            parser.SymbolTable = _symbolTable;
        }

        public dynamic Interpret()
        {
            var program = _parser.Parse();
           
            Console.WriteLine("Tree image:");
            program.Accept(treeDrawingVisitor);

            Console.WriteLine("Program result:");
            program.Accept(exprVisitor);
           
            return null;
        }
    }
}
