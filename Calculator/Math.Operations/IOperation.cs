﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public interface IOperation
    {
        int operandCount { get; set; }
        void Evaluate(double[] operands);
       
    }
}