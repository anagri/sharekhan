namespace Sharekhan.domain
{
    public class Rate
    {
        public virtual double Value { get; set; }

        private Rate()
        {
        }

        public Rate(double rate)
        {
            Value = rate;
        }

        public bool Equals(Rate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Value == Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Rate)) return false;
            return Equals((Rate) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}