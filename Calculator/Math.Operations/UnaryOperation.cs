﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Operations
{
    public abstract class UnaryOperation : Operation
    {
        public UnaryOperation()
        {
            base.OperandCount = 1;
        }
  
    }
}