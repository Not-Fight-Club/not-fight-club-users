using BusinessLayer.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mappers
{
    public class UserMapper /*: IMapper<User, ViewUser>*/
    {
       /* public ViewUser ModelToViewModel(User user)
        {
            ViewUser viewUser = new ViewUser();
            viewUser.UserId = user.UserId;
            viewUser.UserName = user.UserName;
            viewUser.Email = user.Email;

            //convert from datetime to string

            viewUser.Dob = user.Dob;
            viewUser.Pword = user.Pword;
            viewUser.Bucks = user.Bucks;

            return viewUser;



        }

        public List<ViewUser> ModelToViewModel(List<User> obj)
        {
            throw new NotImplementedException();
        }

        public User ViewModelToModel(ViewUser viewUser)
        {
            User user = new User();
            Guid g = Guid.NewGuid();

            user.UserName = viewUser.UserName;
            user.Email = viewUser.Email;

            //convert string from js to datetime c#
            //DateTime d;
            //DateTime.TryParseExact(viewUser.Dob, @"yyyy-MM-dd\Z", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal, out DateTime d);
            user.Dob = viewUser.Dob;
            user.Pword = viewUser.Pword;
            user.Bucks = viewUser.Bucks;

            user.UserId = g;

            return user;
        }

        public List<User> ViewModelToModel(List<ViewUser> obj)
        {
            throw new NotImplementedException();
        }*/
    }
}
