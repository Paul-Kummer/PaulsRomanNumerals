using System;
using System.Collections.Generic;
using System.Windows;
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
                // I bound directly to the property of the object, so this result isn't necessary.
                var result = PaulNumeral.ConvertTheNumber(NormalNumber);
                JustusNumeral.ConvertTheNumber();
            }
            catch (Exception ex)
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
