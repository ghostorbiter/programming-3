using System;

namespace Lab5BEn
{
    public abstract class Item
    {
        public static int NumOfCreatedItems;
        protected readonly int ID;
        
        protected readonly string Sender;
        protected readonly string Recipient;
        protected readonly DateTime postageDate;

        protected Item(string sender, string recepient, DateTime date)
        {
            Sender = sender;
            Recipient = recepient;
            postageDate = date;
            ID = NumOfCreatedItems++;
        }

        public abstract DateTime CalculateArrivalTime();

        public virtual int GetId()
        {
            return ID;
        }

        public virtual string GetSender()
        {
            return Sender;
        }

        public virtual string GetRecipient()
        {
            return Recipient;
        }

        public virtual DateTime GetPostageDate()
        {
            return postageDate;
        }

        public override string ToString()
        {
            return $"Item with id: {ID}\nFrom: {Sender}\nTo: {Recipient}\nPostage date: {postageDate}\n";
        }
    }
}