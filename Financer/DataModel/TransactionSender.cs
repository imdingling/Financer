using System;
using SQLite;

namespace Financer
{
    public class TransactionSender
    {
        public TransactionSender ()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int TransactionId { get; set; }

        public double Share { get; set; }
    }
}

