using Parallel_Bubble_Sorting.Algorithm;
using System;
using Xunit;

namespace Bubble_Sorting_Tests
{
    public class SortTests
    {
        [Fact]
        public void ParallelSortTest()
        {
            int[] array = new int[] { 312, 123, 654, 1234, 986, 343, 566, 12311, 6555, 87575, 36345, 3463234, 2413534, 123123, 0, 0 };
            int[] sortArray = new int[] {  0, 0, 123, 312, 343, 566, 654, 986, 1234, 6555, 12311, 36345, 87575, 123123, 2413534, 3463234 };
            BubbleSort sort = new BubbleSort();
            var result = sort.ParallelBubbleSortStart(array, 8);
            Assert.Equal(result.SortedArray, sortArray);
        }
        [Fact]
        public void NormalSortTest()
        {
            int[] array = new int[] { 312, 123, 654, 1234, 986, 343, 566, 12311, 6555, 87575, 36345, 3463234, 2413534, 123123, 0, 0 };
            int[] sortArray = new int[] { 0, 0, 123, 312, 343, 566, 654, 986, 1234, 6555, 12311, 36345, 87575, 123123, 2413534, 3463234 };
            BubbleSort sort = new BubbleSort();
            var result = sort.NormalBubbleSortStart(array);
            Assert.Equal(result.SortedArray, sortArray);
        }
        [Fact]
        public void CheckerTestTrue()
        {
            int[] array = new int[] { 312, 123, 654, 1234, 986, 343, 566, 12311, 6555, 87575, 36345, 3463234, 2413534, 123123, 0, 0 };
            int[] sortArray = new int[] { 0, 0, 123, 312, 343, 566, 654, 986, 1234, 6555 };
            MassiveChecker mc = new MassiveChecker();
            var res = mc.CheckArrayForSorting(sortArray);
            Assert.True(res);
        }
        [Fact]
        public void CheckerTestFalse()
        {
            int[] array = new int[] { 312, 123, 654, 1234, 986, 343, 566, 12311, 6555, 87575, 36345, 3463234, 2413534, 123123, 0, 0 };
            MassiveChecker mc = new MassiveChecker();
            var res = mc.CheckArrayForSorting(array);
            Assert.False(res);
        }
        [Fact]
        public void NormalSortEmptyArrayTest()
        {
            int[] array = new int[] { };
            BubbleSort mc = new BubbleSort();
            Assert.Throws<ArgumentException> (() => mc.NormalBubbleSortStart(array));
        }
        [Fact]
        public void NormalSortWithOneElementArrayTest()
        {
            int[] array = new int[] { 5 };
            BubbleSort mc = new BubbleSort();
            var res = mc.NormalBubbleSortStart(array);
            Assert.Equal(res.SortedArray, array);
        }
        [Fact]
        public void CompareResultNormalAndParallelMethodTest()
        {
            int[] array = new int[] { 312, 123, 654, 1234, 986, 343, 566, 12311, 6555, 87575, 36345, 3463234, 2413534, 123123, 0, 0 };
            BubbleSort mc = new BubbleSort();
            var res = mc.NormalBubbleSortStart(array);
            var res2 = mc.ParallelBubbleSortStart(array, 8);
            Assert.Equal(res.SortedArray, res2.SortedArray);
        }
    }
}