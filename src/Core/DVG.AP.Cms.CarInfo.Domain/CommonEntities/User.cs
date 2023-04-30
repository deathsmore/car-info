using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Domain.CommonEntities
{
    [Table("Users")]
    public class User
    {
        public User(int id, string? userName, string? displayName, string? email, string? mobile, string? phone)
            : this()
        {
            Id = id;
            UserName = userName;
            DisplayName = displayName;
            Email = email;
            Mobile = mobile;
            Phone = phone;
        }

        public User()
        {
        }

        [Key] 
        [Column("UserId")] public int Id { get; set; }
        public string? UserName { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
