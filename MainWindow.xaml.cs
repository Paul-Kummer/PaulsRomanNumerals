using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
        #endregion



        #region "This is the main window's properties"
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

        private string _romanNumeral = "0";
        public string RomanNumeral
        {
            get
            {
                return _romanNumeral;
            }
            private set
            {
                _romanNumeral = value;
                OnPropertyChanged(nameof(RomanNumeral));
            }
        }

        //Verbos getter and setter
        public string getRomanNumeral()
        {
            return _romanNumeral;
        }
        private void SetRomanNumeral(string value)
        {
            _romanNumeral = value;
            OnPropertyChanged("SetRomanNumeral");
        }

        //Auto Implemented property
        public string PaulNumeral { get; set; } = "Paul";
        #endregion



        #region "This is the constructor"
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion




        #region "This is where my functions are"
        private void ConvertNumbers()
        {
            int currentNumber = 0;
            int numOfZeros = 0;

            int lowerInt = 0;
            int midInt = 0;
            int upperInt = 0;

            string lowerNumeral = "";
            string midNumeral = "";          
            string upperNumeral = "";

            try
            {
                // Clear out the Roman Numeral so the string can be built
                RomanNumeral = "";

                for (int index = 0; index < NormalNumber.Length; index++)
                {
                    numOfZeros = NormalNumber.Length - (1 + index);
                    int number = int.Parse(NormalNumber[index].ToString());

                    int.TryParse($"1{new string('0', numOfZeros)}", out lowerInt);
                    int.TryParse($"5{new string('0', numOfZeros)}", out midInt);
                    int.TryParse($"1{new string('0', numOfZeros + 1)}", out upperInt);

                   
                     lowerNumeral = RomanDict.ContainsKey(lowerInt) ? RomanDict[lowerInt] : "";
                     midNumeral = RomanDict.ContainsKey(midInt) ? RomanDict[midInt] : "";
                     upperNumeral = RomanDict.ContainsKey(upperInt) ? RomanDict[upperInt] : "";

                    // Exact match of lower numeral
                    if(number == 0)
                    {
                        continue;
                    }
                    else if (number == 1)
                    {
                        RomanNumeral += lowerNumeral;
                    }
                    else if (number +3 > 5)
                    {
                        RomanNumeral += new string(lowerNumeral[0], 5 - number);
                    }
                    // Exact match of mid numeral
                    else if(number == 5)
                    {
                        RomanNumeral += midNumeral;            
                    }
                    // Move up from the lower numeral
                    else if (number+3 <= 8)
                    {
                        RomanNumeral += $"{midNumeral}{new string(lowerNumeral[0], 5 - number)}";
                    }
                    // Move down from the upper numeral
                    else if (number + 3 > 8)
                    {
                        RomanNumeral += $"{new string(upperNumeral[0], 10 - number)}{midNumeral}";
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Some exception happened that I don't care about.{ex.ToString()}", "Somthing failed!", MessageBoxButton.OK, MessageBoxImage.Error);
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
        #endregion
    }
}
