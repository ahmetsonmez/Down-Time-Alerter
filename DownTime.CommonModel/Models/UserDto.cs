
namespace DownTime.CommonModel.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public byte[] Salt { get; set; }
        public string Hash { get; set; }
    }
}
