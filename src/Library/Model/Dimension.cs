namespace Library.Model
{
    public class Dimension
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Name, Value);
        }
    }
}