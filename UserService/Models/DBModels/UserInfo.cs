using System;
using System.Collections.Generic;

#nullable disable

namespace Models_DBModels
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
        public DateTime LastLogin { get; set; }
        public int? LoginStreak { get; set; }
        public string? ProfilePic { get; set; }
        public bool RewardCollected { get; set; }

    }
}
