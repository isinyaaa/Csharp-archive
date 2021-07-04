namespace IMC_calc
{
    public static class Logic
    {
        /// <summary>
        /// Corrects the value inputs
        /// </summary>
        /// <param name="input">Unverified input</param>
        /// <returns>Corrected input</returns>
        public static string CorrectInput(string input)
        {
            // If the input is empty then return
            if (input == string.Empty)
                return input;

            // The input must use dots instead of commas
            input = input.Replace(',', '.');
            
            // Checks if the input converts to a float and its length
            if (!input.CheckInput() || input.Length > 6)
                // If input doesn't pass test then remove last char
                input = input.Remove(input.Length - 1);
            
            return input;
        }

        /// <summary>
        /// Extension method that checks whether the input converts to a float
        /// </summary>
        /// <param name="input">Unverified input text</param>
        /// <returns></returns>
        public static bool CheckInput(this string input)
        {
            // Unused test variable (necessary)
            var test = new float();

            return float.TryParse(input, out test);
        }

        /// <summary>
        /// Extension method for getting the description of a BMIE enum
        /// </summary>
        /// <param name="bmi"></param>
        /// <returns>The description</returns>
        public static string GetDescription(this BMIE bmi)
        {
            // Gets the member info for the BMIE element
            var memInfo = bmi.GetType().GetMember(bmi.ToString());

            // If it's not null and has a length then...
            if (memInfo?.Length > 0)
            {
                // Gets the member info array first element's custom attributes
                var attributes = memInfo[0].GetCustomAttributes(typeof(Description), false);

                // And if those are not null and have a length then we return the text of the description attribute
                if (attributes?.Length > 0)
                    return ((Description)attributes[0]).Text;
            }

            // Else we can just convert the BMIE enum to a string
            return bmi.ToString();
        }

        /// <summary>
        /// Converts a number to an BMIE enum through successive if checks
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BMIE BMIConvert(float value)
        {
            if (value < 15)
                return BMIE.VSUnder;

            else if (15 <= value && value < 16)
                return BMIE.SevUnder;

            else if (16 <= value && value < 18.5)
                return BMIE.Under;

            else if (18.5 <= value && value < 25)
                return BMIE.HWeight;

            else if (25 <= value && value < 30)
                return BMIE.OWeight;

            else if (30 <= value && value < 35)
                return BMIE.ModObese;

            else if (35 <= value && value < 40)
                return BMIE.SevObese;

            return BMIE.VSObese;
        }
    }
}
