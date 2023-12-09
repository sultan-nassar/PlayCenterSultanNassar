using gameCenter.Projects.Calculator.Models;
using System.Windows;
using System.Windows.Controls;


namespace gameCenter.Projects.Calculator
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        private CalculatorLogic calculatorLogic;

        public Calculator()
        {
            InitializeComponent();
            this.DataContext = this;
            DevideBtn.Content = "\u00F7";//חילוק
            SquareBtn.Content = "X" + "\u00B2"; //ריבוע
            SqrtBtn.Content = "\u221A"; //שורש מרובע

            calculatorLogic = new CalculatorLogic();
        }

        private void NumBtn_Click(object sender, RoutedEventArgs e)
        {
            string digit = ((Button)sender).Content.ToString()!;
            calculatorLogic.HandleNumBtnClick(digit);
            OutputTextBlock.Text = calculatorLogic.Output;
        }

        private void DecimalBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!calculatorLogic.Output.Contains("."))
            {
                calculatorLogic.HandleDecimalBtnClick();
                OutputTextBlock.Text = calculatorLogic.Output;
            }
        }

        private void OperatorBtn_Click(object sender, RoutedEventArgs e)
        {
            string operatorName = ((Button)sender).Content.ToString()!;
            calculatorLogic.HandleOperatorBtnClick(operatorName!);
            OutputTextBlock.Text = calculatorLogic.Output;
        }

        private void ToggleplusMinusBtn_Click(object sender, RoutedEventArgs e)
        {
            calculatorLogic.HandleToggleplusMinusBtnClick();
            OutputTextBlock.Text = calculatorLogic.Output;
        }

        private void EqualBtn_Click(object sender, RoutedEventArgs e)
        {
            calculatorLogic.HandleEqualBtnClick();
            OutputTextBlock.Text = calculatorLogic.Output;
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            calculatorLogic.HandleClearBtnClick();
            OutputTextBlock.Text = calculatorLogic.Output;
        }
    }
}


