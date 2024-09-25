using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PaulsRomanNumerals.JustusRomanNumClass
{
    public class J_RomanNumeral : INotifyPropertyChanged
    {
        #region "DON"T CHANGE THESE"
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private string _romanNumeral;
        public string RomanNumeral
        {
            get => _romanNumeral;
            set
            {
                _romanNumeral = value;
                OnPropertyChanged(nameof(RomanNumeral));
            }
        }
        #endregion

        /// <summary>
        /// Dictionary to store any roman conversion mapping you would like to do. I'd recommend passing the RomanDict in as a parameter to the constructor
        /// </summary>
        public Dictionary<int, string> J_RomanDict;


        // Constructors for you new class. The second one is overloaded so it can take a dictionary as a parameter
        public J_RomanNumeral()
        {

        }
        public J_RomanNumeral(Dictionary<int, string> newDict)
        {
            J_RomanDict = newDict;
        }

        // Justus, do you're conversion work here or in other functions as you see fit
        // and store the result to the RomanNumeral property
        // IE: RomanNumeral = "XVII"
        public void ConvertTheNumber()
        {


        }
    }
}
