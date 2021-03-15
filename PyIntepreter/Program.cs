using PyInterpreter.InterpreterBody;
using PyInterpreter.InterpreterBody.SymbTable;
using System;
using System.Collections.Generic;

namespace PyInterpreter
{
    class Program
    {
        static void PrintTable(SymbolTable table)
        {
            Console.WriteLine("Name\tType");
            foreach (var pair in table.Dict)
            {
                Console.WriteLine($"{pair.Key}\t{pair.Value.Type}");
            }
        }

        static void Main(string[] args)
        {
            //while (true)
            //{
            //    try
            //    {
            //        Console.WriteLine("Expression>");

            //        var scanner = new Tokenizer(Console.ReadLine());
            //        var parser = new Parser(scanner);
            //        var interpreter = new Interpreter(parser);

            //        var result = interpreter.Interpret();

            //        Console.WriteLine(result);
            //    }
            //    catch(Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}

            try
            {
                string text = "a = 1\n b = a * 10\n";

                var scanner = new Tokenizer(text);
                var parser = new Parser(scanner);
                var interpreter = new Interpreter(parser);

                var result = interpreter.Interpret();

                PrintTable(interpreter.SymbolTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
