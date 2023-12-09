using gameCenter.Projects.CurrencyConverter.Services;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Windows;
namespace gameCenter.Projects.CurrencyConverter
{
    /// <summary>
    /// Interaction logic for CurrencyConverterView.xaml
    /// </summary>
    public partial class CurrencyConverterView : Window
    {
        private CurrencyService _currencyService;
        private Dictionary<string, double> _exchangeRates;
        public CurrencyConverterView()
        {
            InitializeComponent();
            _currencyService = new CurrencyService();
            LoadCurrencies(); //not common to run async function inside a constructor
        }

        private async void LoadCurrencies()
        {
            //we need to excute method _currencyService.GetExchangeRatesAsync();
            {
                try
                {
                    _exchangeRates = await _currencyService.GetExchangeRatesAsync();

                    // Get string[] array of the keys of the dictionary
                    string[] currencyNames = _exchangeRates.Keys.ToArray();

                    // set the itemsSource of the Combo boxes to the string array of the currencies names;
                    FromCurrencyComboBox.ItemsSource = currencyNames;
                    ToCurrencyComboBox.ItemsSource = currencyNames;
                }
                // Do error handling with try catch=> Message.Show(.....)

                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading currencies: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //we need to get - the selected currencies by user

                string? selectedFromCurrency = FromCurrencyComboBox.SelectedItem?.ToString();
                  string? selectedToCurrency = ToCurrencyComboBox.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(selectedFromCurrency) || string.IsNullOrEmpty(selectedToCurrency))
                {
                    MessageBox.Show("Please select both currencies.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //we need to get- the amount to convert

                double amountToConvert;
                if (!double.TryParse(AmountTextBox.Text, out amountToConvert) || string.IsNullOrEmpty(AmountTextBox.Text)) 
                {
                    MessageBox.Show("Invalid amount.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                // we need to value according to the selected items in the combo  boxes

                double fromRate = _exchangeRates[selectedFromCurrency];
                double toRate = _exchangeRates[selectedToCurrency];

                double convertedAmount = (amountToConvert / fromRate) * toRate;

                txtResult.Text = $"Converted Amount {amountToConvert} {selectedFromCurrency} = {convertedAmount:F2} {selectedToCurrency:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error converting currency: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnConvertReverse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string? selectedFromCurrency = ToCurrencyComboBox.SelectedItem?.ToString();
                string? selectedToCurrency = FromCurrencyComboBox.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(selectedFromCurrency) || string.IsNullOrEmpty(selectedToCurrency))
                {
                    MessageBox.Show("Please select both currencies.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                double amountToConvert;
                if (!double.TryParse(AmountTextBox.Text, out amountToConvert))
                {
                    MessageBox.Show("Invalid amount.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                double fromRate = _exchangeRates[selectedFromCurrency];
                double toRate = _exchangeRates[selectedToCurrency];

                double convertedAmount = (amountToConvert / fromRate) * toRate;

                txtResult.Text = $"Converted Amount {amountToConvert} {selectedFromCurrency} = {convertedAmount:F2} {selectedToCurrency:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error converting currency: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
