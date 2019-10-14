
using System;

namespace DownTime.CommonModel.Models
{
    public class WebSiteRequestDto
    {
        public int Id { get; set; }
        public int WebSiteId { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public DateTime Created { get; set; }
        public WebSiteDto WebSite { get; set; }
    }
}
