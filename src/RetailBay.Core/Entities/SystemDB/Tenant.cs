using System;

namespace RetailBay.Core.Entities.SystemDb
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HostName { get; set; }
        public string ConnectionString { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
