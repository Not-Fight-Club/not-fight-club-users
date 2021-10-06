using System;
using System.Collections.Generic;

#nullable disable

namespace Models.DBModels
{
    public partial class UserInfo
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Pword { get; set; }
        public string Email { get; set; }
        public DateTime? Dob { get; set; }
        public int? Bucks { get; set; }
        public bool Active { get; set; }
    }
}
