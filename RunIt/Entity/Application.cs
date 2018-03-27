namespace RunIt.Entity
{
    public class Application : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string FileName { get; set; }

        public override string ToString()
        {
            return $"{{ Id: {Id}, Name: {Name}, Alias: {Alias}, Filename: {FileName} }}";
        }
    }
}
