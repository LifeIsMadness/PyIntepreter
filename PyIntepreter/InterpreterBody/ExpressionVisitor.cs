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

        public void VisitEqualExpr(EqualExpr expr)
        {
            expr._left.Accept(this);
            var left = Result;
            expr._right.Accept(this);
            var right = Result;

            Result = expr.Eval(left, right);
        }

        internal void VisitIfExpr(IfExpr expr)
        {
            List<IResult> conditions = new List<IResult>();
            foreach (var conditionExpr in expr.Conditions)
            {
                conditionExpr.Accept(this);
                conditions.Add(Result);
            }

            //List<IResult> statements = new List<IResult>();
            //foreach (var statementList in expr.Statements)
            //{
            //    statementList.Accept(this);
            //    statements.Add(Result);
            //}

            var way = expr.Eval(conditions);
            way.Accept(this);
        }

        public void VisitNotEqualExpr(NotEqualExpr expr)
        {
            expr._left.Accept(this);
            var left = Result;
            expr._right.Accept(this);
            var right = Result;

            Result = expr.Eval(left, right);
        }

        public void VisitGreaterExpr(GreaterExpr expr)
        {
            expr._left.Accept(this);
            var left = Result;
            expr._right.Accept(this);
            var right = Result;

            Result = expr.Eval(left, right);
        }

        public void VisitLesserExpr(LesserExpr expr)
        {
            expr._left.Accept(this);
            var left = Result;
            expr._right.Accept(this);
            var right = Result;

            Result = expr.Eval(left, right);
        }

        public void VisitGreaterEqualExpr(GreaterEqualExpr expr)
        {
            expr._left.Accept(this);
            var left = Result;
            expr._right.Accept(this);
            var right = Result;

            Result = expr.Eval(left, right);
        }

        public void VisitLesserEqualExpr(LesserEqualExpr expr)
        {
            expr._left.Accept(this);
            var left = Result;
            expr._right.Accept(this);
            var right = Result;

            Result = expr.Eval(left, right);
        }

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
            expr.StatementList.Accept(this);
        }

        public void VisitStatementListExpr(StatementListExpr expr)
        {
            foreach (var statement in expr.Statements)
            {
                statement.Accept(this);
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

        public void VisitNumberExpr(LiteralExpr expr)
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
