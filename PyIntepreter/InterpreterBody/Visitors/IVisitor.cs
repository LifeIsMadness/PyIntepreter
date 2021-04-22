using PyInterpreter.InterpreterBody.Expressions;
using PyInterpreter.InterpreterBody.Expressions.Builtins;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyInterpreter.InterpreterBody.Visitors
{
    public interface IVisitor
    {
        public void VisitAndExpr(AndExpr expr)
        {

        }

        public void VisitOrExpr(OrExpr expr)
        {
        }

        /////////////////////////////////////////////
        // Visit builtin functions.
        public void VisitInputExpr(InputFunctionExpr expr)
        {

        }

        public void VisitPrintExpr(PrintFunctionExpr expr)
        {

        }

        public void VisitRangeExpr(RangeFunctionExpr expr)
        {
    
        }

        public void VisitIntExpr(IntFunctionExpr expr)
        {

        }

        public void VisitLenExpr(LenFunctionExpr expr)
        {

        }
        /////////////////////////////////////////////

        public void VisitEqualExpr(EqualExpr expr)
        {

        }

        public void VisitFunctionExpr(FunctionExpr expr)
        {

        }

        public void VisitWhileExpr(WhileExpr expr)
        {
        }

        public void VisitForExpr(ForExpr expr)
        {
        }

        public void VisitIfExpr(IfExpr expr)
        {
        }

        public void VisitNotEqualExpr(NotEqualExpr expr)
        {
        }

        public void VisitGreaterExpr(GreaterExpr expr)
        {
        }

        public void VisitLesserExpr(LesserExpr expr)
        {
        }

        public void VisitGreaterEqualExpr(GreaterEqualExpr expr)
        {
        }

        public void VisitLesserEqualExpr(LesserEqualExpr expr)
        {
        }

        public void VisitIndexExpr(IndexExpr expr)
        {

        }

        public void VisitProgramExpr(ProgramExpr expr)
        {
        }

        public void VisitStatementListExpr(StatementListExpr expr)
        {
        }

        public void VisitListExpr(ListExpr expr)
        {

        }

        public void VisitNumberExpr(LiteralExpr expr)
        {
        }

        public void VisitEmptyExpr(EmptyExpr expr)
        {

        }

        public void VisitMinusExpr(MinusExpr expr)
        {
 
        }

        public void VisitPlusExpr(PlusExpr expr)
        {
 
        }

        // TODO: BinExpr class.
        public void VisitAddExpr(AddExpr expr)
        {
        
        }

        public void VisitSubExpr(SubExpr expr)
        {
  
        }

        public void VisitMulExpr(MulExpr expr)
        {
        }

        public void VisitDivExpr(DivExpr expr)
        {

        }

        public void VisitVariableExpr(VariableExpr expr)
        {
    
        }

        public void VisitAssignExpr(AssignExpr expr)
        {
           
        }
    }
}
