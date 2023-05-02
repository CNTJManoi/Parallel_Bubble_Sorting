using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel_Bubble_Sorting.Algorithm
{
    public class ErrorSortException : Exception
    {
        public string Message { get; set; }
        public ErrorSortException(string message)
        {
            Message = message;
        }
    }
}
