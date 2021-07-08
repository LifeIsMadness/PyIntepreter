using PyInterpreter.InterpreterBody.Expressions;
using PyInterpreter.InterpreterBody.Expressions.Builtins;
using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.SymbTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Visitors
{
    public class TreeDrawingVisitor: IVisitor
    {
        private string _treeLevel = string.Empty;

        public SymbolTable SymbolTable { get; set; }

        public Dictionary<string, FunctionBodyExpr> Builtins { get; set; }

        public void VisitAndExpr(AndExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "and");
            expr.Left.Accept(this);
            _treeLevel = lvl;
            expr.Right.Accept(this);
        }

        public void VisitOrExpr(OrExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "or");
            expr.Left.Accept(this);
            _treeLevel = lvl;
            expr.Right.Accept(this);
        }

        //public IResult Result { get; set; }


        /////////////////////////////////////////////
        // Visit builtin functions.
        public void VisitInputExpr(InputFunctionExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "input");
        }

        public void VisitPrintExpr(PrintFunctionExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "print()");
        }

        public void VisitRangeExpr(RangeFunctionExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "range()");
        }

        public void VisitIntExpr(IntFunctionExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "int()");
        }

        public void VisitLenExpr(LenFunctionExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "len()");
        }
        /////////////////////////////////////////////

        public void VisitEqualExpr(EqualExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "==");
            expr._left.Accept(this);
            _treeLevel = lvl;
            expr._right.Accept(this);
        }

        public void VisitFunctionExpr(FunctionExpr expr)
        {
            // TODO: if not callable
            var name = ((VariableExpr)expr.Name).Eval().Value;
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "function_call");
            foreach (var arg in expr.Args)
            {
                arg.Accept(this);
                _treeLevel = lvl;
            }

            if (Builtins.TryGetValue(name, out FunctionBodyExpr func))
            {
                func.Accept(this);
            }
            else throw new Exception("No such function");
        }

        public void VisitWhileExpr(WhileExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "while");
            expr.Condition.Accept(this);
            expr.Statements.Accept(this);
        }

        public void VisitForExpr(ForExpr expr)
        {

            // expr.Variable.Accept(this);
            //var name = Result;
            var name = ((VariableExpr)expr.Variable).Eval();
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "for");
            expr.Iterable.Accept(this);


            expr.Statements.Accept(this);
            
        }

        public void VisitIfExpr(IfExpr expr)
        {
            //List<IResult> conditions = new List<IResult>();
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "if");
            foreach (var conditionExpr in expr.Conditions)
            {
                conditionExpr.Accept(this);
                _treeLevel = lvl;
            }

            //var way = expr.Eval(conditions);
            //way.Accept(this);
        }

        public void VisitNotEqualExpr(NotEqualExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "!=");
            expr._left.Accept(this);
            _treeLevel = lvl;
            expr._right.Accept(this);
        }

        public void VisitGreaterExpr(GreaterExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + ">");
            expr._left.Accept(this);
            _treeLevel = lvl;
            expr._right.Accept(this);
        }

        public void VisitLesserExpr(LesserExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "<");
            expr._left.Accept(this);
            _treeLevel = lvl;
            expr._right.Accept(this);
        }

        public void VisitGreaterEqualExpr(GreaterEqualExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + ">=");
            expr._left.Accept(this);
            _treeLevel = lvl;
            expr._right.Accept(this);
        }

        public void VisitLesserEqualExpr(LesserEqualExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "<=");
            expr._left.Accept(this);
            _treeLevel = lvl;
            expr._right.Accept(this);
        }

        public TreeDrawingVisitor()
        {
        }

        public void VisitIndexExpr(IndexExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "Index");
            expr._list.Accept(this);
            _treeLevel = lvl;
            expr._indexValue.Accept(this);
        }

        public void VisitProgramExpr(ProgramExpr expr)
        {
            var lvl = _treeLevel;
            Console.WriteLine("Program");
            expr.StatementList.Accept(this);
            _treeLevel = lvl;
        }

        public void VisitStatementListExpr(StatementListExpr expr)
        {
            var lvl = _treeLevel;
            foreach (var statement in expr.Statements)
            {
                statement.Accept(this);
                _treeLevel = lvl;
            }
        }

        public void VisitListExpr(ListExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "list");
            foreach (var item in expr.items)
            {
                item.Accept(this);
                _treeLevel = lvl;
            }
        }

        public void VisitNumberExpr(LiteralExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + expr.Token.Value);
        }

        public void VisitEmptyExpr(EmptyExpr expr)
        {
        }

        public void VisitMinusExpr(MinusExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "-");
            expr._right.Accept(this);
        }

        public void VisitPlusExpr(PlusExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "+");
            expr._right.Accept(this);
        }

        // TODO: BinExpr class.
        public void VisitAddExpr(AddExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "+");
            expr._left.Accept(this);
            _treeLevel = lvl;
            expr._right.Accept(this);
        }

        public void VisitSubExpr(SubExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "-");
            expr._left.Accept(this);
            _treeLevel = lvl;
            expr._right.Accept(this);
        }

        public void VisitMulExpr(MulExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "*");
            expr._left.Accept(this);
            _treeLevel = lvl;
            expr._right.Accept(this);
        }

        public void VisitDivExpr(DivExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "/");
            expr._left.Accept(this);
            _treeLevel = lvl;
            expr._right.Accept(this);
        }

        public void VisitVariableExpr(VariableExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            string varName = expr.Eval().Value;
            Console.WriteLine("|" + _treeLevel + varName);
        }

        public void VisitAssignExpr(AssignExpr expr)
        {
            _treeLevel += "\\";
            var lvl = _treeLevel;
            Console.WriteLine("|" + _treeLevel + "=");
            
            if (expr._left is VariableExpr)
            {
                expr._left.Accept(this);
                _treeLevel = lvl;
                expr._right.Accept(this);
            }
            else if (expr._left is IndexExpr)
            {
                expr._left.Accept(this);
                _treeLevel = lvl;
                expr._right.Accept(this);

                //var indexes = new Stack<IExpression>();
                //var list = ((IndexExpr)expr._left)._list;
                //indexes.Push(((IndexExpr)expr._left)._indexValue);
                //while (list is IndexExpr)
                //{
                //    indexes.Push(((IndexExpr)list)._indexValue);
                //    list = ((IndexExpr)list)._list;
                //}

                //list.Accept(this);
                //_treeLevel = lvl;
                //expr._right.Accept(this);
                //_treeLevel = lvl;
                //while (indexes.Count > 1)
                //{
                //    indexes.Pop().Accept(this);
                //    _treeLevel = lvl;
                //}

                //indexes.Pop().Accept(this);
            }
        }
    }

}
