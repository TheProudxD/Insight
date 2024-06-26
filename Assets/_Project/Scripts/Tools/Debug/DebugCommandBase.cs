namespace Utilites
{
    public abstract class DebugCommandBase
    {
        public string ID { get; private set; }
        public string Description { get; private set; }
        public string Format { get; private set; }

        protected DebugCommandBase(string id, string description, string format)
        {
            ID = id;
            Description = description;
            Format = format;
        }
    }
}