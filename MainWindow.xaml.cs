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
            {1, "I" },
            { 5, "V" },
            {10, "X" },
            {50, "L" },
            {100, "C" },
            {500, "D" }
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
            NormalNumber.ToString()
            int currentNumber = 0;
            string convertedNumeral = "";
            try
            {
                if (int.TryParse(NormalNumber, out currentNumber))
                {
                    RecursiveDerivitation(currentNumber);
                }
            }
            catch
            {
                MessageBox.Show("Some exception happened that I don't care about.", "Somthing failed!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int RecursiveDerivitation(int currentNumber)
        {
            int output;

            int minNum = 0;
            int maxNum = 0;

            foreach(int normyNum in RomanDict.Keys.OrderByDescending((key) => key))
            {
                minNum = normyNum <= currentNumber ? normyNum : minNum;
                if(normyNum >= currentNumber)
                {
                    maxNum = normyNum;
                    continue;
                }
            }

            // We add "I"s to Min
            if((minNum + 3) >= currentNumber)
            {

            }
            else // We subtract
            {

            }




            return output;
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
