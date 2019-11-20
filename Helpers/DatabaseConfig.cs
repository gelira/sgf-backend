namespace SGFBackend.Helpers
{
    public class DatabaseConfig
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string ConnectionString 
        {
            get => string.Format("server={0};port={1};database={2};userid={3};pwd={4};",
                Host, Port, Database, Username, Password);
        }
    }
}
