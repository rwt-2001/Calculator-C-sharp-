﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public abstract class Operation
    {
        public int OperandCount { get; set; }

        public double Evaluate(double[] operands)
        {
            if (operands == null || operands.Length != OperandCount)
            {
                throw new NotImplementedException();
            }

            return Calculate(operands);
        }


        protected abstract double Calculate(double[] operands);

    }
}