using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Interfaces
{
    public interface IStringDecorator
    {
        void Append(string inputString);
        void Append(char inputChar);
        string ToString();
    }
}
