using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    class EditionByCopiesCountComparer : IComparer<Edition>
    {
        public int Compare(Edition x, Edition y)
        {
            return x.CopiesCount.CompareTo(y.CopiesCount);
        }
    }
}
