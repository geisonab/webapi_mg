using Dapper.Contrib.Extensions;

namespace WebAPI_MG.Models
{
    [Table("tbWEB_WEBAPIMG_USER")]
    public class User
    {
        [ExplicitKey]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}