namespace MatrixTools
{
    public struct ZeroLocation
    {
        public ZeroLocation(Line location, int index, int ammount)
        {
            Location = location;
            Index = index;
            Ammount = ammount;
        }

        public Line Location { get; set; }

        public int Index { get; set; }

        public int Ammount { get; set; }
    }
}