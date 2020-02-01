using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using NReco;
using lssWebApi2.ObserverMediator;
using lssWebApi2.Enumerations;

namespace ConsoleApp1
{
    
    public interface IClassA : IEntity, IObservableMediator
    {
       new bool MessageFromObserver(IObservableAction message);
    }
    public class ClassA : IClassA
    {


        public bool MessageFromObserver(IObservableAction message)
        {
            bool retVal = true;
            string className = this.GetType().Name;
            string process = "";

            var action = message.Actions.Where(e => e.targetByName == className).FirstOrDefault<MessageAction>();

            if (action != null)
            {
                if (action.command_action == TypeOfObservableAction.InsertData)
                {
                    process = "insert";
                }
                else if (action.command_action == TypeOfObservableAction.UpdateData)
                {
                    process = "update";
                }
                else if (action.command_action == TypeOfObservableAction.DeleteData)
                {
                    process = "delete";
                }
                Console.WriteLine($"Class A: {process}");

            }

          
            return retVal;
        }
    }
    public interface IClassB: IEntity, IObservableMediator
    {
       new bool MessageFromObserver(IObservableAction message);
    }
    public class ClassB: IClassB
    {

        public bool MessageFromObserver(IObservableAction message)
        {
            bool retVal = true;
            string className = this.GetType().Name;
            string process = "";

            var action = message.Actions.Where(e => e.targetByName == className).FirstOrDefault<MessageAction>(); 

            if (action != null)
            {
                if (action.command_action == TypeOfObservableAction.InsertData)
                {
                    process = "insert";
                }
                else if (action.command_action == TypeOfObservableAction.UpdateData)
                {
                    process = "update";
                }
                else if (action.command_action == TypeOfObservableAction.DeleteData)
                {
                    process = "delete";
                }
                Console.WriteLine($"Class B: {process}");

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
            mediator.SubscribeToObserver(outputManager, outputManager.MessageFromObserver);

            IClassB dbManager = new ClassB();
            mediator.SubscribeToObserver(dbManager, dbManager.MessageFromObserver);

            IObservableAction observedAction = new ObservableAction();
            MessageAction action = new MessageAction
            {

                targetByName = nameof(ClassB),
                command_action = TypeOfObservableAction.InsertData
            };
            observedAction.Actions.Add(action);
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
