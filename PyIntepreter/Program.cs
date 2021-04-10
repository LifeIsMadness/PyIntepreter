using PyInterpreter.InterpreterBody;
using PyInterpreter.InterpreterBody.SymbTable;
using System;
using System.Collections.Generic;
using System.IO;

namespace PyInterpreter
{
    class Program
    {   
        static void PrintTable(SymbolTable table)
        {        
            Console.WriteLine("Variables");
            Console.WriteLine("Name\tType\tValue");
            foreach (var pair in table.Dict)
            {
                Console.WriteLine($"{pair.Key}" +
                    $"\t{pair.Value.Type}" +
                    $"\t{pair.Value.Value.Value}");
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
            
           // string fileName = "input.txt";
           // string text = File.ReadAllText(fileName);

            try
            {
                string text = "a = [] ";
                Console.WriteLine($"Program text: \n{text}");
                Console.WriteLine("-----------------------");
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
