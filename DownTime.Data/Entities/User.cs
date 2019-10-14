using System.Collections.Generic;

namespace DownTime.Data.Entities
{
    /// <summary>
    /// Table where users are held and managed.
    /// </summary>
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public byte[] Salt { get; set; }
        public string Hash { get; set; }

        public ICollection<WebSite> WebSites { get; set; }
    }
}
