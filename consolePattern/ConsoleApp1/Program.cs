using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using NReco;

namespace ConsoleApp1
{
    public interface ICustomerInfoByHeader : IEntity
    {
        string customerCode { get; set; }
        string companyName { get; set; }
        string CurrentDate { get; set; }
    }
    public class CustomerInfoByHeader : ICustomerInfoByHeader
    {
        public string customerCode { get; set; }
        public string companyName { get; set; }
        public string CurrentDate { get; set; }
    }
    public interface IEntity
    {
    }

    public interface IObservableAction : IEntity
    {
        string observed_action { get; set; }
        string command_action { get; set; }
        string targetByName { get; set; }
    }

    public class ObservableAction : IObservableAction
    {
        public string observed_action { get; set; }
        public string command_action { get; set; }
        public string targetByName { get; set; }
    }

    public class Observer
    {
        Dictionary<object, Func<IObservableAction, bool>> _subscriberContainer = new Dictionary<object, Func<IObservableAction, bool>>();

        public Observer()
        {
        }
        public void SubscribeToMediator(IEntity entity, Func<IObservableAction, bool> callbackFunction)
        {
            _subscriberContainer.Add(entity, callbackFunction);
        }
        public void TransmitMessage(IObservableAction message)
        {
            foreach (KeyValuePair<object, Func<IObservableAction, bool>> item in _subscriberContainer)
            {
                item.Value.Invoke(message);
            }
        }
    }
    public interface IObservableMediator
    {

        bool MessageFromObservableMediator(IObservableAction message);

    }

    public interface IClassA: IEntity, IObservableMediator {
        bool MessageFromObservableMediator(IObservableAction message);
    }
    public class ClassA: IClassA
    {

        private void ProcessCommands(IObservableAction message)
        {
            switch (message.command_action)
            {
                case "InsertData":
                    break;
            }

        }
        public bool MessageFromObservableMediator(IObservableAction message)
        {
            bool retVal = true;
            string className = this.GetType().Name;

            if (message.targetByName == "")
            {
                Console.WriteLine($"Class A: {message.observed_action}");
                ProcessCommands(message);
            }
            else if (message.targetByName == className)
            {
                Console.WriteLine($"Class A: {message.observed_action}");
                ProcessCommands(message);
            }

            return retVal;
        }
    }
    public interface IClassB: IEntity, IObservableMediator
    {
       bool MessageFromObservableMediator(IObservableAction message);
    }
    public class ClassB: IClassB
    {

        private void ProcessCommands(IObservableAction message)
        {
            switch (message.command_action)
            {
                case "InsertData":
                    break;
            }

        }
        public bool MessageFromObservableMediator(IObservableAction message)
        {
            bool retVal = true;
            string className = this.GetType().Name;

            if (message.targetByName == "")
            {
                Console.WriteLine($"Class B: {message.observed_action}");
                ProcessCommands(message);
            }
            else if (message.targetByName == className)
            {
                Console.WriteLine($"Class B: {message.observed_action}");
                ProcessCommands(message);
            }

            return retVal;
        }
       
    }

    class Program
    {
        
        static void Main(string[] args)
        {

        Observer mediator = new Observer();

            IClassA outputManager = new ClassA();
            mediator.SubscribeToMediator(outputManager, outputManager.MessageFromObservableMediator);

            IClassB dbManager = new ClassB();
            mediator.SubscribeToMediator(dbManager, dbManager.MessageFromObservableMediator);

            IObservableAction observedAction = new ObservableAction();
            observedAction.observed_action = "Ready to Add";
            observedAction.targetByName = nameof(ClassA);
            observedAction.command_action = "InsertData";
            mediator.TransmitMessage(observedAction);

            /*

            Equation equation1 = new Equation("$B1", "Sqrt($A1) + Cos($A2)");

            SpreadsheetInJson ss = new SpreadsheetInJson();
            ss.calcInfo += equation1.ParseExpression;

            ss.RegisterEquation(equation1);

            //Invokes the delegate
            ss.NewValue("$A1", 4);
            ss.NewValue("$A2", 4);

            Console.WriteLine("{0}",equation1.GetExpression());
            Console.WriteLine("{0}", ss.GetValue("$B1"));
            */
            //Console.WriteLine("{0}", ss.Squared(4));

            /*
            StringTypes mystring = new StringTypes(new StringBuilderType());
            mystring.Append("Hello World");
            mystring.Append("Hello Boise");

            Console.WriteLine(mystring.ToString());

            StringTypes myString2 = new StringTypes(new charType());
            myString2.Append('a');
            myString2.Append('b');
            myString2.Append('c');

            Console.WriteLine(myString2.ToString());

            MDArray.MultidimensionalArray();
            */
            Console.ReadLine();
        }
    }
}
