namespace Core.Planning
{
    public class Parameter
    {
        public string Name { get; set; }
        public bool IsRequiredForGoal { get; set; }
        public bool IsRequiredExectCount { get; set; }
        public int Count { get; set; }

        public Parameter ShallowCopy()
        {
            return (Parameter)MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("N{0}C{1}", Name, Count);
        }
    }
}