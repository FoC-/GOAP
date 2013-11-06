using System;

namespace Core.Planning
{
    [Serializable]
    public class Parameter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsRequiredForGoal { get; set; }
        //Todo: Make it enum less more equal
        public bool IsRequiredExectCount { get; set; }
        public int Count { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Parameter)) return false;
            var other = (Parameter) obj;
            if (this.Id == other.Id
                && this.Count == other.Count)
                return true;
            return  false;

        }

        public override int GetHashCode()
        {
            var iHash = 127;
            iHash ^= Id == null ? 0 : Id.GetHashCode();
            iHash ^= Name == null ? 0 : Name.GetHashCode();
            iHash ^= IsRequiredForGoal.GetHashCode();
            iHash ^= IsRequiredExectCount.GetHashCode();
            iHash ^= Count.GetHashCode();
            return iHash;
        }

        public override string ToString()
        {
            return string.Format("Id: {0}, Count: {1}", Id, Count);
        }
    }
}