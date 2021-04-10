using PyInterpreter.InterpreterBody.Results;

namespace PyInterpreter.InterpreterBody.Expressions
{
    public class EqualExpr : IExpression
    {
        public IExpression _left;
        public IExpression _right;

        public EqualExpr(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public void Accept(ExpressionVisitor visitor)
        {
            visitor.VisitEqualExpr(this);
        }

        public IResult Eval(IResult left, IResult right)
        {
            return left.Equal(right);
        }
    }
}
