using System;
using System.Collections.Generic;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PaulsRomanNumerals.JustusRomanNumClass;
using RomanNumeral;

namespace PaulsRomanNumerals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region "Variable, Constants, etc..."
        // This is used to relay the property changes to the xaml presentation layer that this code is attached to
        public event PropertyChangedEventHandler PropertyChanged;
        public Dictionary<int, string> RomanDict = new Dictionary<int, string>
        {
            { 1,     "I" },
            { 5,     "V" },
            { 10,    "X" },
            { 50,    "L" },
            { 100,   "C" },
            { 500,   "D" },
            { 1000,  "M" }
        };
        private CancellationTokenSource cts;
        #endregion



        #region "This is the main window's properties"
        private ObservableCollection<History> _conversionHistory = new ObservableCollection<History>();
        public ObservableCollection<History> ConversionHistory 
        {
            get => _conversionHistory;
            set 
            {
                _conversionHistory = value;
                OnPropertyChanged(nameof(ConversionHistory));
            } 
        } 

        private string _normalNumber = "0";
        public string NormalNumber
        {
            get => _normalNumber;
            set
            {
                _normalNumber = value;
                OnPropertyChanged(nameof(NormalNumber));
            }
        }

        private P_RomanNumeral _paulNumeral;
        public P_RomanNumeral PaulNumeral
        {
            get => _paulNumeral;
            private set
            {
                _paulNumeral = value;
                OnPropertyChanged(nameof(PaulNumeral));
            }
        }
        #endregion

        #region "<---- Expand Me Justus!: you're class object will be assigned here and linked to the xaml"
        private J_RomanNumeral _justusNumeral;
        public J_RomanNumeral JustusNumeral
        {
            get => _justusNumeral;
            set
            {
                _justusNumeral = value;
                OnPropertyChanged(nameof(JustusNumeral));
            }
        }
        #endregion



        #region "This is the constructor"
        public MainWindow()
        {
            InitializeComponent();

            InitializeRomanObjects();
        }
        #endregion



        private void InitializeRomanObjects()
        {
            PaulNumeral = new P_RomanNumeral(RomanDict);
            JustusNumeral = new J_RomanNumeral(RomanDict);
        }

        #region "This is where my functions are"
        private void ConvertNumbers()
        {
            try
            {
                int normalNumberInt = 0;
                if(int.TryParse(NormalNumber, out normalNumberInt))
                {
                    NormalNumber = EnsureWithinValidRange(normalNumberInt);

                    // I bound directly to the property of the object, so this result isn't necessary.
                    var result = PaulNumeral.ConvertTheNumber(NormalNumber);
                    JustusNumeral.ConvertTheNumber();

                    Dispatcher.Invoke(() =>
                    {
                        ConversionHistory.Insert(0,new History(NormalNumber, PaulNumeral.RomanNumeral, JustusNumeral.RomanNumeral));
                        OnPropertyChanged(nameof(ConversionHistory));
                    } );
                }
                else
                {
                    MessageBox.Show("The provided string cannot be converter to a Roman Numeral", "Invalid Text", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Some exception happened that I don't care about.{ex.ToString()}", "Somthing failed!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private String EnsureWithinValidRange(int numberToCheck)
        {
            int newNum = numberToCheck > 3999 ? 3999 : numberToCheck;
            newNum = newNum < 0 ? 0 : newNum;
            return newNum.ToString();
        }

        private async void TestAllNumbers()
        {
            cts = new CancellationTokenSource();
            for (var num = 0; num <= 3999; num++)
            {
                if(cts.IsCancellationRequested)
                {
                    return;
                }

                NormalNumber = num.ToString();
                await Task.Run(() => { ConvertNumbers(); });
                
                //await System.Threading.Tasks.Task.Delay(10);
            }
        }
        #endregion



        #region "This is my event driven functions"
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            ConvertNumbers();
        }

        private void TestAllNumbersButton_Click(object sender, RoutedEventArgs e)
        {
            TestAllNumbers();
        }
        #endregion

        private void NormalNumber_TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                ConvertNumbers();
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }
    }
}

public class History
{
    public History(string initial, string paul, string justus)
    {
        NumberToConvert = initial;
        PaulVal = paul;
        JustusVal = justus;
    }

    public string NumberToConvert { get; set; } = "NA";
    public string PaulVal { get; set; } = "NA";
    public string JustusVal { get; set; } = "NA";
}
