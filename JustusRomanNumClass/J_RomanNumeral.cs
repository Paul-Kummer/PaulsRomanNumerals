using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RomanNumeral;

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

        // Every entry in this dictionary represents a numeral that can be turned on to build one decimal position of a complete Roman Numeral
        public Dictionary<string, NumSwitch> switchDict = new Dictionary<string, NumSwitch>
        {
            {"one", new NumSwitch()},
            {"two", new NumSwitch() },
            {"three", new NumSwitch() },
            {"four", new NumSwitch() },
            {"five", new NumSwitch() },
            {"six", new NumSwitch() },
            {"seven", new NumSwitch() },
            {"eight", new NumSwitch() },
            {"nine", new NumSwitch() },
            {"ten", new NumSwitch() },
        };


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
        public void ConvertTheNumber(string theNumber = "39")
        {
            // Your final result will be written to resultString. tempString will be used to build resultString.
            string resultString = "", tempString = "";

            // These will hold the values of the numeral while for loop is working.
            // The default values are lowernumeral = I, midNumeral = V, upperNumeral = X
            string lowerNumearl = J_RomanDict[1], midNumeral = J_RomanDict[5], upperNumeral = J_RomanDict[10];

            //iterate over the string number and determine what each of the switches should be at each decmal place
            for (int index = 0; index < theNumber.Length; index++)
            {
                // What the number is at the current decimal positon. We are not concerned with any numbers at the other positions right now.
                // This will be needed to determine if a switch is on later on.
                int curNumAtDecimal = int.Parse(theNumber[index].ToString());

                // Make sure that the dictionary of the switches is reset so no leftover values are used
                ResetSwitches();

                // Getting the switches ready for use and assinged to a variable for easier access
                NumSwitch one = switchDict["one"], two = switchDict["two"], three = switchDict["three"], four = switchDict["four"], five = switchDict["five"], 
                    six = switchDict["six"], seven = switchDict["seven"], eight = switchDict["eight"], nine = switchDict["nine"], ten = switchDict["ten"];

                //if you pass the number into my static function, you will get your upper, mied, and lower numerals you need to build the complete Roman Numeral
                var workingNumerals = P_RomanNumeral.GetUMLNumeralsForPosition(theNumber, index);
                string theHighNum = workingNumerals.upperNumeral;
                string theMidNum = workingNumerals.midNumeral;
                string theLowNum = workingNumerals.lowerNumeral;

                // Assign the SwitchNum a numeral that it should be if it is activated
                one.NumeralValue = theLowNum;
                two.NumeralValue = theLowNum;
                three.NumeralValue = theLowNum;
                four.NumeralValue = theLowNum;
                five.NumeralValue = theMidNum;
                six.NumeralValue = theLowNum;
                seven.NumeralValue = theLowNum;
                eight.NumeralValue = theLowNum;
                nine.NumeralValue = theLowNum;
                ten.NumeralValue = theHighNum;

                // Perform the some conditional calculations to determine what characters to show

                /*
                   the number 9 would look like this in terms of switches, the numeral after shows what the switch will be if it is true.

                    one.SwitchActive = false;       // I
                    two.SwitchActive = false;       // I
                    three.SwitchActive = false;     // I
                    four.SwitchActive = false;      // I
                    five.SwitchActive = false;      // V
                    six.SwitchActive = false;       // I
                    seven.SwitchActive = false;     // I
                    eight.SwitchActive = false;     // I
                    nine.SwitchActive = true;       // I
                    ten.SwitchActive = true;        // X

                    switch nine and ten are turned on to generate the string IX
                 */


                #region "JUSTUS FIX THIS PLEASE"

                // !!! IMPORTANT !!! FIX THIS TO PRODUCE THE CORRECT RESULT
                one.SwitchActive = true;
                two.SwitchActive = true;
                three.SwitchActive = true;
                four.SwitchActive = true;
                five.SwitchActive = true;
                six.SwitchActive = true;
                seven.SwitchActive = true;
                eight.SwitchActive = true;
                nine.SwitchActive = true;
                ten.SwitchActive = true;

                #endregion


                /* If a switch is flipped to true, the character will be concatenated to the resulting string */

                // The dollar sign in front of the quotations means that string formatting is being done and values in curly braces will be replaced with their .tostring() method
                // In the NumSwitch class, the NumeralValue will always be "" if the switch is not active, so its safe to always use the NumeralValue 
                tempString =    $"{one.NumeralValue}" +
                                $"{two.NumeralValue}" +
                                $"{three.NumeralValue}" +
                                $"{four.NumeralValue}" +
                                $"{five.NumeralValue}" +
                                $"{six.NumeralValue}" +
                                $"{seven.NumeralValue}" +
                                $"{eight.NumeralValue}" +
                                $"{nine.NumeralValue}" +
                                $"{ten.NumeralValue}";

                // Concatenate the temp result to the result string. This builds out the entire number on each iteration of the for loop
                resultString += tempString;
            }

            // The work is complete, so assign the result to RomanNumeral
            RomanNumeral = resultString;
        }

        private void ResetSwitches()
        {
            foreach(var possibleNumeral in switchDict.Keys)
            {
                switchDict[possibleNumeral].Reset();
            }
        }

        public class NumSwitch
        {
            public bool SwitchActive = false;
            private string _numeralValue = "";
            public string NumeralValue
            {
                get => SwitchActive ? _numeralValue : "";
                set => _numeralValue = value;
            }

            public NumSwitch()
            {

            }

            public void Reset()
            {
                SwitchActive = false;
                NumeralValue = "";
            }
        }
    }
}
