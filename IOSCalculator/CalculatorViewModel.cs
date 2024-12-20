using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace IOSCalculator
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private string display = "0";
        private string fullExpression = string.Empty;
        private double previousValue;
        private string selectedOperator;

        public string Display
        {
            get => display;
            set
            {
                display = value;
                OnPropertyChanged();
            }
        }

        public string FullExpression
        {
            get => fullExpression;
            set
            {
                fullExpression = value;
                OnPropertyChanged();
            }
        }

        public ICommand NumberCommand => new RelayCommand(param =>
        {
            if (Display == "0") Display = param.ToString();
            else Display += param.ToString();
            FullExpression += param.ToString();
        });

        public ICommand ClearCommand => new RelayCommand(_ =>
        {
            Display = "0";
            FullExpression = string.Empty;
            previousValue = 0;
            selectedOperator = null;
        });

        public ICommand OperatorCommand => new RelayCommand(param =>
        {
            if (double.TryParse(Display, out double value))
            {
                previousValue = value;
                selectedOperator = param.ToString();
                FullExpression += $" {selectedOperator} ";
                Display = "0";
            }
        });

        public ICommand EqualsCommand => new RelayCommand(_ =>
        {
            if (double.TryParse(Display, out double value))
            {
                FullExpression += $" = ";
                switch (selectedOperator)
                {
                    case "+":
                        Display = (previousValue + value).ToString();
                        break;
                    case "-":
                        Display = (previousValue - value).ToString();
                        break;
                    case "*":
                        Display = (previousValue * value).ToString();
                        break;
                    case "/":
                        Display = value != 0 ? (previousValue / value).ToString() : "Error";
                        break;
                }
                FullExpression += Display;
            }
        });

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
