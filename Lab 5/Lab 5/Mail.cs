using System;

namespace Lab5BEn
{
    public class Mail : Item
    {
        private bool Registered;

        public Mail(string sender, string recepient, DateTime date, bool registered = false) : base(sender, recepient, date)
        {
            Registered = registered;
        }

        public override DateTime CalculateArrivalTime()
        {
            if (Registered)
                return postageDate.AddDays(2);
            else
                return postageDate.AddDays(5);
        }

        public override string ToString()
        {
            if (Registered)
                return base.ToString() + "Registered mail\n";
            else
                return base.ToString() + "Standart mail\n";
        }

        protected bool IsRegistered()
        {
            return Registered;
        }
    }
}