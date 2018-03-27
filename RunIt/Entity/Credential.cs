namespace RunIt.Entity
{
    public class Credential : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }

        public override string ToString()
        {
            return $"{{ Id: {Id}, Name: {Name}, Username: {Username}, Password: {Password}, Domain: {Domain}}}";
        }
    }
}
