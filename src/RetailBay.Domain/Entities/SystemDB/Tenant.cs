namespace RetailBay.Domain.Entities.SystemDB
{
    public class Tenant : EntityBase
    {
        public string Name { get; set; }
        public string HostName { get; set; }
        public string ConnectionString { get; set; }
    }
}
