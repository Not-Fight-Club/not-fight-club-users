using BusinessLayer.Interfaces;
using Models;
using Models.Models;
using Models_DBModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mappers
{
    public class UserMapper : IMapper<UserInfo, ViewUser>
    {


        public ViewUser ModelToViewModel(UserInfo user)
        {
             ViewUser viewUser = new ViewUser();
             viewUser.UserId = user.UserId;
             viewUser.UserName = user.UserName;
             viewUser.Email = user.Email;
            viewUser.LoginStreak = user.LoginStreak;
            viewUser.LastLogin = user.LastLogin;

             //convert from datetime to string

             viewUser.Dob = user.Dob;
             viewUser.Pword = user.Pword;
             viewUser.Bucks = user.Bucks;

             return viewUser;
        }

        public List<ViewUser> ModelToViewModel(List<UserInfo> obj)
        {
            throw new NotImplementedException();
        }

        public UserInfo ViewModelToModel(ViewUser viewUser)
         {
             UserInfo user = new UserInfo();
             Guid g = Guid.NewGuid();

             user.UserName = viewUser.UserName;
             user.Email = viewUser.Email;

             //convert string from js to datetime c#
             //DateTime d;
             //DateTime.TryParseExact(viewUser.Dob, @"yyyy-MM-dd\Z", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal, out DateTime d);
             user.Dob = viewUser.Dob;
             user.Pword = viewUser.Pword;
             user.Bucks = viewUser.Bucks;
            user.Active = viewUser.Active;
            user.LoginStreak = viewUser.LoginStreak;
            user.LastLogin = viewUser.LastLogin;

             user.UserId = g;

             return user;
         }

         public List<UserInfo> ViewModelToModel(List<ViewUser> obj)
         {
             throw new NotImplementedException();
         }
    }
}
