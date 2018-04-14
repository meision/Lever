namespace Meision
{
    public interface IExpressible
    {
        void ParseExpression(string expression);
        string ToExpression();
    }
}
