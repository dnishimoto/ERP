using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lssWebApi2.CalculatorDomain
{

    public class CalcInfoEventArgs : EventArgs
    {
        public CalcInfoEventArgs(string fieldName, double? value)
        {
            _fieldName = fieldName;
            _value = value;
        }
        public string _fieldName { get; }
        public double? _value { get; }

    }
    public interface IEquation
    {
        string GetName();
        void EvaluateExpression(CalcInfoEventArgs e);
        void ParseExpression(object sender, CalcInfoEventArgs e);
        string GetExpression();
        double? GetCellValue();
    }
    public class Equation: IEquation
    {
        private string _expression;
        private double? _evaluation;
        private string _cell;
        private Func<double, double> _evaluator=null;

        public Equation(string cell,string expression)
        {
            _expression = expression;
            _cell = cell;
        }
        public string GetName()
        {
            return _cell;
        }
        //private static string Expression { get; set; }

        public void EvaluateExpression(CalcInfoEventArgs e)
        {
            try
            {
                _expression = _expression.Replace(e._fieldName.ToString(), e._value.ToString());



                if (Regex.IsMatch(_expression, @"\$[A-Z]{1,2}[0-9]{1,9999}") == false)
                {
                    Expression<Func<double, double>> expr = x => x * x;
                    /*
                    NReco.Linq.LambdaParser.CompiledExpression compiledExpr;

                    var linqExpr = NReco.Linq.LambdaParser.Parse(_expression.ToString());

                    compiledExpr = new NReco.Linq.LambdaParser.CompiledExpression()
                    {
                        Parameters = NReco.Linq.LambdaParser.GetExpressionParameters(linqExpr)
                    };
                    var lambdaExpr = Expression.Lambda(linqExpr, compiledExpr.Parameters);
                    compiledExpr.Lambda = lambdaExpr.Compile();
                    */

                    _evaluator = expr.Compile();

                    object value = _evaluator(2);
                    string name = value.GetType().Name;

                    if (value == null)
                    {
                        _evaluation = null;
                        return;
                    }
                    if (name == "Int32")
                    {
                        _evaluation = Convert.ToInt32(value);
                    }
                    else
                    {
                        _evaluation = (double)value;
                    }


                    //return _expression;  
                }
            }
            catch (Exception ex)
            {
                throw new Exception("evaluator", ex);
            }
        }
        public void ParseExpression(object sender, CalcInfoEventArgs e)
        {
            EvaluateExpression(e);
        }
        public string GetExpression()
        {
            return (_expression);
        }
        public double? GetCellValue()
        {
            return (_evaluation);
        }
    }
    public class SpreadsheetInJson
    {
        public event EventHandler<CalcInfoEventArgs> calcInfo;
        IList<Equation> _equations = new List<Equation>();

        public void ParseEvaluatedEquations()
        {
            try
            {
                var query = from e in _equations
                            where e.GetCellValue() != null
                            select e;

                foreach (var equation in query)
                {
                    if (equation.GetCellValue() != null)
                    {
                        var query2 = from e in _equations
                                     where e.GetName() != equation.GetName()
                                     && e.GetCellValue() == null
                                     select e;

                        foreach (var equation2 in query2)
                        {
                            //equation2._expression = equation2._expression.Replace(equation.GetName(), equation.GetCellValue().ToString());
                            CalcInfoEventArgs e = new CalcInfoEventArgs(
                                equation.GetName(),
                                equation.GetCellValue()
                            );
                            equation2.EvaluateExpression(e);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("spreadsheetinJson", ex);
            }
        }
        public void NewValue(string fieldName, double value)
        {

            calcInfo?.Invoke(this, new CalcInfoEventArgs(fieldName, value));

            ParseEvaluatedEquations();
        }
        public void RegisterEquation(Equation equation)
        {
            _equations.Add(equation);
        }
        public IList<Equation> GetEquations()
        {
            return _equations;
        }
        public double? GetValue(string cellName)
        {

            Equation equation = (from e in _equations
                                 where e.GetName() == cellName
                                 select e).FirstOrDefault<Equation>();

            return equation.GetCellValue();

        }
        public double Squared(double input)
        {
            Expression<Func<double, double>> expr = x => x * x;

            Func<double, double> h = expr.Compile();

            double retVal = h(input);
            return retVal;
        }

    }
}
