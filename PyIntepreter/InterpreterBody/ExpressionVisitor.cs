using PyInterpreter.InterpreterBody.Expressions;
using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.SymbTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody
{
    public class ExpressionVisitor
    {
        protected SymbTable.SymbolTable _symbolTable;

        public SymbolTable SymbolTable { get => _symbolTable; }

        public IResult Result { get; set; }

        public ExpressionVisitor()
        {
            _symbolTable = new SymbolTable();
        }

        internal void VisitIndexExpr(IndexExpr expr)
        {
            expr._list.Accept(this);
            var list = Result;

            expr._indexValue.Accept(this);
            var index = Result;

            Result = expr.Eval(list, index);
        }

        public void VisitProgramExpr(ProgramExpr expr)
        {
            foreach (var node in expr.nodes)
            {
                node.Accept(this);
            }
        }

        public void VisitListExpr(ListExpr expr)
        {
            List<IResult> items = new List<IResult>();

            foreach (var item in expr.items)
            {
                item.Accept(this);
                items.Add(Result);
            }

            Result = expr.Eval(items);

        }

        public void VisitNumberExpr(NumberExpr expr)
        {
            Result = expr.Eval();
        }

        public void VisitEmptyExpr(EmptyExpr expr)
        {
            Result = expr.Eval();
        }

        public void VisitMinusExpr(MinusExpr expr)
        {
            expr._right.Accept(this);
            Result = expr.Eval(Result);
        }

        public void VisitPlusExpr(PlusExpr expr)
        {
            expr._right.Accept(this);
            Result = expr.Eval(Result);
        }

        // TODO: BinExpr class.
        public void VisitAddExpr(AddExpr expr)
        {
            expr._left.Accept(this);
            var left = Result;
            expr._right.Accept(this);
            Result = expr.Eval(left, Result);
        }

        public void VisitSubExpr(SubExpr expr)
        {
            expr._left.Accept(this);
            var left = Result;
            expr._right.Accept(this);
            Result = expr.Eval(left, Result);
        }

        public void VisitMulExpr(MulExpr expr)
        {
            expr._left.Accept(this);
            var left = Result;
            expr._right.Accept(this);
            Result = expr.Eval(left, Result);
        }

        public void VisitDivExpr(DivExpr expr)
        {
            expr._left.Accept(this);
            var left = Result;
            expr._right.Accept(this);
            Result = expr.Eval(left, Result);
        }

        public void VisitVariableExpr(VariableExpr expr)
        {
            string varName = expr.Eval().Value;
            Result = _symbolTable.GetVariable(varName).Value;
        }

        public void VisitAssignExpr(AssignExpr expr)
        {
            Result = ((VariableExpr)expr._left).Eval();
            string name = Result.Value;
            expr._right.Accept(this);

            var res = Result.Value;
            var variable = new Variable(name, Result);

            _symbolTable.SetVariable(name, variable);
        }
    }
}
