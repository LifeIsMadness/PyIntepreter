using PyInterpreter.InterpreterBody.Expressions;
using PyInterpreter.InterpreterBody.Expressions.Builtins;
using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.SymbTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Visitors
{
    public class ExpressionVisitor: IVisitor
    {

        public void VisitAndExpr(AndExpr expr)
        {
            expr.Left.Accept(this);
            var left = Result;
            if (left.Value == false)
            {
                Result = expr.Eval(left);
            }
            else
            {
                expr.Right.Accept(this);
                var right = Result;

                Result = expr.Eval(left, right);
            }
        }

        public void VisitOrExpr(OrExpr expr)
        {
            expr.Left.Accept(this);
            var left = Result;
            expr.Right.Accept(this);
            var right = Result;

            Result = expr.Eval(left, right);
        }

        public SymbolTable SymbolTable { get; set; }

        public Dictionary<string, FunctionBodyExpr> Builtins { get; set; }

        public IResult Result { get; set; }

        /////////////////////////////////////////////
        // Visit builtin functions.
        public void VisitInputExpr(InputFunctionExpr expr)
        {
            Result = expr.Eval();
        }

        public void VisitPrintExpr(PrintFunctionExpr expr)
        {
            Result = expr.Eval();
        }

        public void VisitRangeExpr(RangeFunctionExpr expr)
        {
            Result = expr.Eval();
        }

        public void VisitIntExpr(IntFunctionExpr expr)
        {
            Result = expr.Eval();
        }

        public void VisitLenExpr(LenFunctionExpr expr)
        {
            Result = expr.Eval();
        }
        /////////////////////////////////////////////

        public void VisitEqualExpr(EqualExpr expr)
        {
            expr._left.Accept(this);
            var left = Result;
            expr._right.Accept(this);
            var right = Result;

            Result = expr.Eval(left, right);
        }

        public void VisitFunctionExpr(FunctionExpr expr)
        {
            // TODO: if not callable
            var name = ((VariableExpr)expr.Name).Eval().Value;
            
            List<IResult> args = new List<IResult>();
            foreach (var arg in expr.Args)
            {
                arg.Accept(this);
                args.Add(Result);
            }

            if (Builtins.TryGetValue(name, out FunctionBodyExpr func))
            {
                func.Args = args;
                func.Accept(this);
            }
            else throw new Exception("No such function");
        }

        public void VisitWhileExpr(WhileExpr expr)
        {

            expr.Condition.Accept(this);
            var condition = Result;
            while (expr.Eval(condition).Value)
            {
                expr.Statements.Accept(this);
                expr.Condition.Accept(this);
                condition = Result;
            }
        }

        public void VisitForExpr(ForExpr expr)
        {

            // expr.Variable.Accept(this);
            //var name = Result;
            var name = ((VariableExpr)expr.Variable).Eval();
            expr.Iterable.Accept(this);
            var iterable = Result;
            
            foreach (var item in iterable.Value)
            {
                var variable = new Variable(name.Value, item);
                SymbolTable.SetVariable(name.Value, variable);
                expr.Statements.Accept(this);
            }
        }

        public void VisitIfExpr(IfExpr expr)
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
            
        }

        public void VisitIndexExpr(IndexExpr expr)
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
            try
            {
                Result = SymbolTable.GetVariable(varName).Value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Name {varName} is not defined");
            }
        }

        public void VisitAssignExpr(AssignExpr expr)
        {
            if (expr._left is VariableExpr)
            {
                Result = ((VariableExpr)expr._left).Eval();
                string name = Result.Value;
                expr._right.Accept(this);

                var res = Result.Value;
                var variable = new Variable(name, Result);

                SymbolTable.SetVariable(name, variable);
            }
            else if (expr._left is IndexExpr)
            {
                var indexes = new Stack<IExpression>();
                var list = ((IndexExpr)expr._left)._list;
                indexes.Push(((IndexExpr)expr._left)._indexValue);
                while (list is IndexExpr)
                {
                    indexes.Push(((IndexExpr)list)._indexValue);
                    list = ((IndexExpr)list)._list;
                }

                list.Accept(this);
                var _list = Result;
                expr._right.Accept(this);
                var value = Result;

                while (indexes.Count > 1)
                {
                    indexes.Pop().Accept(this);
                    _list = _list.Value[Result.Value];
                }

                indexes.Pop().Accept(this);
                _list.Value[Result.Value] = value;
            }
        }
    }
}
