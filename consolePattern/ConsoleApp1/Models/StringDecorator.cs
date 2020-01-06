using ConsoleApp1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Models
{
    public abstract class StringDecorator: IStringDecorator
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

            int len = this.charList?.Length ?? 0;

            tempBuffer = new char[len + 1];
            if (this.charList != null)
            {
                Array.Copy(this.charList, tempBuffer, len);
                tempBuffer[len] = val;
                this.charList = new char[len + 1];
                Array.Copy(tempBuffer, this.charList, len + 1);
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
}
