using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

    public class UnitTestPattern
    {
        private readonly ITestOutputHelper output;


        public UnitTestPattern(ITestOutputHelper output)
        {
            this.output = output;

        }
      

    }
       

