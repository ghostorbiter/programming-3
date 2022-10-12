using System;

namespace Lab5BEn
{
    public class Package : Item
    {
        private float Weight;
        public Package(string sender, string recepient, DateTime date, float weight) : base(sender, recepient, date)
        {
            Weight = weight;
        }

        public override DateTime CalculateArrivalTime()
        {
            if (Weight > 20)
                return postageDate.AddDays(15);
            else
                return postageDate.AddDays(10);
        }

        public override string ToString()
        {
            return base.ToString() + $"Type: package\nWeight: {Weight}\n";
        }

        protected float GetWeight()
        {
            return Weight;
        }
    }
}