using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    internal class Calculator
    {
        private Dictionary<string, IOperation> operations;

        public Calculator()
        {
            operations = new Dictionary<string, IOperation>(StringComparer.OrdinalIgnoreCase);
            InitializeOperations();
        }

        private void InitializeOperations()
        {
            AddOperation("+", (args) => args.Sum(), 2);
            AddOperation("*", (args) => args.Aggregate((x, y) => x * y), 2);
            AddOperation("/", (args) => args.Skip(1).Aggregate(args.First(), (x, y) => x / y), 2);
            AddOperation("-", (args) => args.Aggregate((x, y) => x - y), 2);
            AddOperation("Sin", args => Math.Sin(args[0]), 1);
            AddOperation("Cos", args => Math.Cos(args[0]), 1);
            AddOperation("Tg", args => Math.Tan(args[0]), 1);
            AddOperation("Ctg", args => 1/Math.Tan(args[0]), 1);
            AddOperation("Arctg", args => Math.Atan(args[0]), 1);
            AddOperation("Exp", args => Math.Exp(args[0]), 1);
            AddOperation("Ln", args => Math.Log(args[0]), 1);
            AddOperation("Sqrt", args => Math.Sqrt(args[0]), 1);
            AddOperation("Pow", args => Math.Pow(args[0], args[1]), 2);
            AddOperation("^", args => Math.Pow(args[0], args[1]), 2);
            AddOperation("Log", args => Math.Log(args[0], args[1]), 2);
            AddOperation("Abs", args => Math.Abs(args[0]), 1);
        }

        private void AddOperation(string name, Func<double[], double> operation, int expectedParameters)
        {
            operations.Add(name, new MathOperation(operation, expectedParameters));
        }

        public void Run()
        {
            while (true)
            {
                Console.Write("op: ");
                var operatorOrFunction = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(operatorOrFunction))
                    break;

                if (operations.TryGetValue(operatorOrFunction, out IOperation operation))
                {
                    Console.Write("args: ");
                    var argsInput = Console.ReadLine();
                    double[] args = argsInput.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(double.Parse).ToArray();

                    double result = operation.Call(args);
                    if(result == 999999999999999999)
                        Console.WriteLine("Результат: unknown");
                    else
                        Console.WriteLine($"Результат: {result}");
                }
                else
                    Console.WriteLine($"Оператор или математическая функция '{operatorOrFunction}' не найдена.");
            }
        }
    }
}
