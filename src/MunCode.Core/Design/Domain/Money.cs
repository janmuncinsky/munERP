namespace MunCode.Core.Design.Domain
{
    using System;
    using System.Runtime.Serialization;

    using MunCode.Core.Guards;

    [Serializable]
    public class Money : ValueObject<Money>, IComparable<Money>, ISerializable
    {
        private decimal amount;
        private string currency;

        public Money(SerializationInfo info, StreamingContext context)
        {
            Guard.NotNull(info, nameof(info));
            Guard.NotNull(context, nameof(context));

            this.amount = info.GetDecimal(nameof(this.amount));
            this.currency = info.GetString(nameof(this.currency));
        }

        public Money(decimal amount, string currency)
        {
            Guard.NotNull(currency, nameof(currency));

            this.amount = amount;
            this.currency = currency;
        }

        protected Money()
        {
        }

        public static Money Default => new Money(0, "USD");

        public static Money operator +(Money left, Money right)
        {
            return new Money(left.amount + right.amount, left.currency);
        }

        public static Money operator -(Money left, Money right)
        {
            return new Money(left.amount - right.amount, left.currency);
        }

        public static bool operator <(Money left, Money right)
        {
            return left.amount < right.amount;
        }

        public static bool operator <=(Money left, Money right)
        {
            return left.amount <= right.amount;
        }

        public static bool operator >(Money left, Money right)
        {
            return left.amount > right.amount;
        }

        public static bool operator >=(Money left, Money right)
        {
            return left.amount >= right.amount;
        }

        public static Money operator *(Money left, int right)
        {
            return new Money(left.amount * right, left.currency);
        }

        public int CompareTo(Money other)
        {
            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            if (this.Equals(other))
            {
                return 0;
            }

            return this.amount.CompareTo(other.amount);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.amount), this.amount);
            info.AddValue(nameof(this.currency), this.currency);
        }
    }
}