using System;
using Xunit;
using Xunit.Abstractions;

namespace lssXUnit3
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper output;



        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;

        }

        [Fact]
        public void TestCurry()
        {
            Func<int, Func<int, int>> curriedAdd = x => y => x + y;
            int b = curriedAdd(2)(3);
           
            output.WriteLine($"{b} ");

        }
    }
}
