using PyInterpreter.InterpreterBody;
using System;

namespace PyInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Expression>");

                    var scanner = new Tokenizer(Console.ReadLine());
                    var interpreter = new Interpreter(scanner);

                    var tree = interpreter.Expr();

                    Console.WriteLine(tree.Interpret());
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
