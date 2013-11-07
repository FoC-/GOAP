using System;

namespace Core.Planning
{
    [Serializable]
    public class Parameter
    {
        public string Name { get; set; }
        public bool IsRequiredForGoal { get; set; }
        //Todo: Make it enum less more equal
        public bool IsRequiredExectCount { get; set; }
        public int Count { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Parameter)) return false;
            var other = (Parameter)obj;
            return this.Name == other.Name && this.Count == other.Count;
        }

        public override int GetHashCode()
        {
            var iHash = 127;
            iHash ^= Name == null ? 0 : Name.GetHashCode();
            iHash ^= IsRequiredForGoal.GetHashCode();
            iHash ^= IsRequiredExectCount.GetHashCode();
            iHash ^= Count.GetHashCode();
            return iHash;
        }

        public Parameter ShallowCopy()
        {
            return (Parameter)MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("Name: {0}, Count: {1}", Name, Count);
        }
    }
}