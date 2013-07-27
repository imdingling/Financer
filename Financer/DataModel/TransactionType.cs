using System;

namespace Financer
{
    public class TransactionType
    {
        public string Description { get; private set; }

        public TransactionType(string description)
        {
            this.Description = description;
        }
    }
}

