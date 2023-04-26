using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParralelBubleSorting.WPF.Models.Status
{
    public interface ISortingProgress
    {
        public string StatusOneTask { get; set; }
        public string StatusTwoTask { get; set; }
        public string StatusThreeTask { get; set; }
        public void UpdateStatus(Task taskOne, Task taskTwo, Task taskThree);
    }
}
