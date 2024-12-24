using IOSCalculator.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace IOSCalculator
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorModel calculatorModel = new();

        public string Display => calculatorModel.Display;
        public string FullExpression => calculatorModel.FullExpression;

        public ICommand NumberCommand => new RelayCommand(param =>
        {
            calculatorModel.AppendNumber(param?.ToString() ?? string.Empty);
            UpdateProperties();
        });

        public ICommand ClearCommand => new RelayCommand(_ =>
        {
            calculatorModel.Clear();
            UpdateProperties();
        });

        public ICommand OperatorCommand => new RelayCommand(param =>
        {
            if (param is string operatorSymbol)
            {
                calculatorModel.SetOperator(operatorSymbol);
                UpdateProperties();
            }
        });

        public ICommand EqualsCommand => new RelayCommand(_ =>
        {
            calculatorModel.Calculate();
            UpdateProperties();
        });

        private void UpdateProperties()
        {
            OnPropertyChanged(nameof(Display));
            OnPropertyChanged(nameof(FullExpression));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
