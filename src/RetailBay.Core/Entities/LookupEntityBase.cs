namespace RetailBay.Core.Entities
{
    /// <summary>
    /// LookupEntityBase class.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Entities.EntityBase" />
    public abstract class LookupEntityBase : EntityBase
    {
        public string Name { get; set; }
        public string Abrv { get; set; }
        public string Slug { get; set; }
        public bool IsDeleted { get; set; }
    }
}
