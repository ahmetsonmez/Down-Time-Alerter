
namespace DownTime.Data.Entities
{
    /// <summary>
    /// Request results are kept in this table.
    /// </summary>   
    public class WebSiteRequest : BaseEntity
    {
        public int WebSiteId { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public WebSite WebSite { get; set; }
    }
}
