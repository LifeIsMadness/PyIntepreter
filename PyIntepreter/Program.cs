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
                    var parser = new Parser(scanner);
                    var interpreter = new Interpreter(parser);

                    var result = interpreter.Interpret();

                    Console.WriteLine(result);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
