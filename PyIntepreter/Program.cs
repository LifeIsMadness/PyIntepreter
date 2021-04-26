using PyInterpreter.InterpreterBody;
using PyInterpreter.InterpreterBody.Results;
using PyInterpreter.InterpreterBody.SymbTable;
using PyInterpreter.InterpreterBody.Visitors;
using System;
using System.Collections.Generic;
using System.IO;

namespace PyInterpreter
{

    class A
    {
        public static A operator +(A a, A b) => new A();
    }

    class B
    {
        public static B operator +(B a, B b) => new B();
    }

    class Program
    {   
        static void PrintTable(Interpreter interpreter)
        {        
            Console.WriteLine("Variables");
            Console.WriteLine("Name\tType\tValue");
            foreach (var pair in interpreter.SymbolTable.Dict)
            {

                if (pair.Value.Type == "list")
                {
                    Console.Write($"{pair.Key}" +
                        $"\t{pair.Value.Type}\t");
                    PrintList(pair.Value.Value.Value);
                    Console.WriteLine();
 
                    continue;
                }
                Console.WriteLine($"{pair.Key}" +
                    $"\t{pair.Value.Type}" +
                    $"\t{pair.Value.Value.Value}");
            }

            Console.WriteLine("\nBuiltins");
            Console.WriteLine("Name\tType");
            foreach (var pair in interpreter.Builtins)
            {
                Console.WriteLine($"{pair.Key}" +
                    "\tbuiltin_function");
            }
        }

        public static void PrintList(IList<IResult> list)
        {
            Console.Write("[");
            foreach (var item in list)
            {
                if (item is ListResult)
                    PrintList(item.Value);
                else   
                    Console.Write($"{item.Value}, ");
            }
            Console.Write(']');
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
            
            string fileName = "input.txt";
            string text = File.ReadAllText(fileName);

            try
            {
                //string text = "for i in range(10):\n\tprint(i)";
                Console.WriteLine($"Program text: \n{text}");
                Console.WriteLine("-----------------------");
                var scanner = new Tokenizer(text);
                var parser = new Parser(scanner);
                var interpreter = new Interpreter(parser);

                var result = interpreter.Interpret();

                //PrintTable(interpreter);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }

        }
    }
}
