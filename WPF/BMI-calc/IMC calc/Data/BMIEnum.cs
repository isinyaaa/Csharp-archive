using System;

namespace IMC_calc
{
    /// <summary>
    /// Represents a label in the BMI classification table
    /// </summary>
    public enum BMIE
    {
        [Description("very severely underweight")]
        VSUnder,

        [Description("severely underweight")]
        SevUnder,

        [Description("underweight")]
        Under,

        [Description("healthy weight")]
        HWeight,

        [Description("overweight")]
        OWeight,

        [Description("moderately obese")]
        ModObese,

        [Description("severely obese")]
        SevObese,

        [Description("very severely or morbidly obese")]
        VSObese,
    }

    /// <summary>
    /// Property for the BMI enums that allows them to have a description
    /// </summary>
    internal class Description : Attribute
    {
        /// <value>The text that holds the description</value>
        internal string Text { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="text"></param>
        public Description(string text) => Text = text;
    }
}
