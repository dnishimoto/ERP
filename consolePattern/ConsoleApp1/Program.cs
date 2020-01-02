using System;
using System.Text;

namespace ConsoleApp1
{
    public abstract class StringDecorator
    {
        public abstract void Append(string inputString);
        public abstract void Append(char inputChar);
        public abstract override string ToString();
    }
    
   
    public class StringTypes : StringDecorator
    {
        private TextTypes _textTypes = null;
        public StringTypes(TextTypes textTypes)
        {
            _textTypes = textTypes;
        }
        public override void Append(string inputString)
        {
            _textTypes.Append(inputString);
        }
        public override void Append(char inputChar)
        {
            _textTypes.Append(inputChar);
        }
        public override string ToString()
        {
            return _textTypes.ToString();
        }
    }
    public abstract class TextTypes
    {
        protected StringBuilder sb = new StringBuilder();
        protected char[] charList;
        public virtual void Append(string inputString) { sb.Append(inputString); }
        public virtual void Append(char val) { }
        public abstract override string ToString();
      
    }
    public class charType : TextTypes
    {
        public override void Append(char val)
        {
            char[] tempBuffer;

            int len = this.charList?.Length??0;

            tempBuffer = new char[len + 1];
            if (this.charList != null) {
                    Array.Copy(this.charList, tempBuffer, len);
                    tempBuffer[len ] = val;
                    this.charList = new char[len + 1];
                     Array.Copy(tempBuffer, this.charList, len +1);
            }
            else
            {
                this.charList = new char[1];
                this.charList[0] = val;
            }
           

           
        }
        public override string ToString()
        {
            return String.Join("+", this.charList);
        }

    }
    public class StringBuilderType : TextTypes
    {
     
        public override string ToString()
        {
            return sb.ToString();
        }
    }
    class Program
    {
        public static void MultidimensionalArray()
        {
            long[,] data = new long[2, 4]{
            {1,2,3,4},
            {7,8,9,10}
            };

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write("{0}",data[i,j]);
                }
                Console.WriteLine();

            }
            long[][] data2 = new long[2][]
                {
                    new long[4]{1,2,3,4},
                    new long[4]{7,8,9,10}
                };
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write("{0}", data2[i][j]);
                }
                Console.WriteLine();

            }


            string[] movieStars = new string[] {
                "Harrison Ford",
                "John Wayne",
                "Elizabeth Taylor"
            };
            foreach (var item in movieStars)
            {
                Console.WriteLine("{0}",item);
            }

            char[] charList = new char[3] { 'a', 'b', 'c' };
            Console.WriteLine("{0}", String.Join("+", charList));


        }
        static void Main(string[] args)
        {
            StringTypes mystring = new StringTypes(new StringBuilderType());
            mystring.Append("Hello World");
            mystring.Append("Hello Boise");

            Console.WriteLine(mystring.ToString());

            StringTypes myString2 = new StringTypes(new charType());
            myString2.Append('a');
            myString2.Append('b');
            myString2.Append('c');

            Console.WriteLine(myString2.ToString());




            //MultidimensionalArray();
            Console.ReadLine();
        }
    }
}
