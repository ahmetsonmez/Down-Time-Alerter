using System;
using System.Collections.Generic;

namespace DownTime.Data.Entities
{
    /// <summary>
    /// The site to be requested is also defined in this table
    /// </summary>
    public class WebSite : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int RequestInterval { get; set; }
        public bool IsActive { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public bool IsSetJob { get; set; }
        public User User { get; set; }
        public virtual ICollection<WebSiteRequest> WebSiteRequests { get; set; }
    }
}
