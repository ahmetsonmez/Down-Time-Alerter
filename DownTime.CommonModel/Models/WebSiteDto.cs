using System.ComponentModel.DataAnnotations;

namespace DownTime.CommonModel.Models
{
    public class WebSiteDto
    {
        public int Id { get; set; }

        [Display(Name = "WebSite Name")]
        [Required(ErrorMessage = "* WebSite Name is required")]
        public string Name { get; set; }

        [Display(Name = "WebSite Url")]
        [Required(ErrorMessage = "* WebSite Url is required")]
        [Url(ErrorMessage = "Invalid URL!")]
        public string Url { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Request Interval Url")]
        [Required(ErrorMessage = "* Request Interval Url is required")]        
        public int RequestInterval { get; set; }

        public bool IsActive { get; set; }                                 
    }
}
