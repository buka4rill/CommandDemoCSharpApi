namespace Commander.Dtos
{
    public class CommandReadDto
    {
        // Properties
        public int Id { get; set; }

        public string HowTo { get; set; }

        public string Line { get; set; }

        // We dont want to expose the Platform
        // property, just for illustration
    }
}