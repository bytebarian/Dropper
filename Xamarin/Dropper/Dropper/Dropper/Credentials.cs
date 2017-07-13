namespace Dropper
{
    public class Credentials
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public ushort Port { get; set; }
        public string DatabaseName { get; set; }
        public string SyncGatewayUrl { get; set; }
    }
}
