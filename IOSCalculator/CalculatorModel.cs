namespace IOSCalculator.Models
{
    public class CalculatorModel
    {
        public string Display { get; internal set; } = "0";
        public string FullExpression { get; internal set; } = string.Empty;
        private double previousValue;
        private string? selectedOperator;

        public void Clear()
        {
            Display = "0";
            FullExpression = string.Empty;
            previousValue = 0;
            selectedOperator = null;
        }

        public void AppendNumber(string number)
        {
            if (Display == "0")
                Display = number;
            else
                Display += number;
        }

        public void SetOperator(string operatorSymbol)
        {
            if (!string.IsNullOrEmpty(selectedOperator))
                Calculate();

            selectedOperator = operatorSymbol;
            previousValue = double.Parse(Display);
            FullExpression += $"{Display} {selectedOperator} ";
            Display = "0";
        }

        public void Calculate()
        {
            if (selectedOperator == null) return;

            double currentValue = double.Parse(Display);
            double result = selectedOperator switch
            {
                "+" => previousValue + currentValue,
                "-" => previousValue - currentValue,
                "*" => previousValue * currentValue,
                "/" => currentValue != 0 ? previousValue / currentValue : throw new DivideByZeroException(),
                _ => currentValue
            };

            Display = result.ToString();
            FullExpression += $"{currentValue} = {result}";
            selectedOperator = null;
            previousValue = result;
        }
    }
}
