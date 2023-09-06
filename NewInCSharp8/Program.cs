namespace NewInCSharp8
{
    struct XX
    {
        public int X { get; set; }
        public int Y { get; set; }
        private int z;
        public int Z
        {
            readonly get
            {
                Change();
                return z;
            }
            set { z = value; }
        }
        public readonly int GetSum()
        {
            Change();
            return X + Y;
        }
        public void Change()
        {
            X = 0; 
            Y = 0;
        }
    }
    internal class Program
    {

        static void Main(string[] args)
        {
            XX xx = new XX();
            xx.X = 45; xx.Y = 15;
            Console.WriteLine("Hello, World!" + xx.GetSum());
        }
    }
}
