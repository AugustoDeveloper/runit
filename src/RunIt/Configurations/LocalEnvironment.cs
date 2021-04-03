namespace RunIt.Configurations
{
    public class LocalEnvironment
    {
        public string Conf {get; set; }
        
        public Domain[] Domains { get; set; }
        public Application[] Applications { get; set; }
    }
}