namespace MatrixTools
{
    public class Det
    {
        public Det(bool exists)
        {
            Exists = exists;
        }

        public Det(bool exists, double value) : this(exists)
        {
            Value = value;
        }

        public bool Exists { get; set; }

        public double Value { get; set; }
    }
}