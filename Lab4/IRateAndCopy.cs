using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    interface IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();
    }
}
