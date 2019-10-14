
namespace DownTime.CommonModel.Models
{
    public class WebSiteListDto
    {
        public int Id { get; set; }   
        public string Name { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }     
        public int RequestInterval { get; set; }
        public bool IsActive { get; set; }
        public UserDto User { get; set; }
    }
}
