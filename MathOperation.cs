using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    internal class MathOperation: IOperation
    {
        private Func<double[], double> operation;
        private int expectedParameters;

        public MathOperation(Func<double[], double> operation, int expectedParameters)
        {
            this.operation = operation;
            this.expectedParameters = expectedParameters;
        }

        public double Call(params double[] args)
        {
            if (args.Length < expectedParameters)
            {
                Console.WriteLine($"Необходимо {expectedParameters} параметра/ов, вы написали один {args.Length}.");
                return 999999999999999999;
                //throw new ArgumentException($"Необходимо {expectedParameters} параметра/ов, вы написали один {args.Length}.");
            }
            return operation(args);
        }
    }
}
