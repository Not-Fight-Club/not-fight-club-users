using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using DataLayerDBContext_DBContext;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models_DBModels;

namespace BusinessLayer.Repositories
{
    public class UserInfoRepository : IRepository<ViewUser, string>
    {
        private readonly NotFightClubUserContext _dbContext = new NotFightClubUserContext();
        private readonly IMapper<UserInfo, ViewUser> _mapper;

        public UserInfoRepository(IMapper<UserInfo, ViewUser> mapper)
        {
            _mapper = mapper;
        }

        public async Task<ViewUser> Add(ViewUser ViewUser)
        {
            //check if the UserInfo already exists if so decline the entry ( implement later)
            //convert to UserInfo with Mapper class
            UserInfo UserInfo = _mapper.ViewModelToModel(ViewUser);
            UserInfo.LoginStreak = 1;
            //add to the db
            _dbContext.Database.ExecuteSqlInterpolated($"Insert into UserInfo(UserName, PWord, Email, DOB, Active, LastLogin, LoginStreak, RewardCollected) values({UserInfo.UserName},{UserInfo.Pword},{UserInfo.Email},{UserInfo.Dob},{UserInfo.Active}, {DateTime.Now}, {UserInfo.LoginStreak}, {UserInfo.RewardCollected})");
            //save changes
            _dbContext.SaveChanges();
            //read UserInfo back from the db
            UserInfo registeredUserInfo = await _dbContext.UserInfos.FromSqlInterpolated($"select * from UserInfo where UserName = {UserInfo.UserName} and PWord ={UserInfo.Pword}").FirstOrDefaultAsync();

            return _mapper.ModelToViewModel(registeredUserInfo);
        }

        public async Task<ViewUser> Read(string email)
        {
            //Set bd user model with information from database
            UserInfo user = await _dbContext.UserInfos.FromSqlInterpolated($"select * from UserInfo where email = {email}").FirstOrDefaultAsync();

            ViewUser viewUser =  _mapper.ModelToViewModel(user);

            //update login streak for logging in 
            UpdateLoginStreak(viewUser);

            //UserInfo loggedUserInfo = await _dbContext.UserInfos.FromSqlInterpolated($"select * from UserInfo where email = {email}").FirstOrDefaultAsync();


            return viewUser;

        }

        public bool Update(ViewUser user)
        {
            UserInfo dbUser = _mapper.ViewModelToModel(user);

            int rowsAffected =_dbContext.Database.ExecuteSqlInterpolated($"UPDATE UserInfo SET LastLogin = {dbUser.LastLogin}, LoginStreak = {dbUser.LoginStreak}, RewardCollected = {dbUser.RewardCollected}, ProfilePic = {dbUser.ProfilePic}, UserName = {dbUser.UserName}, PWord = {dbUser.Pword}, DOB = {dbUser.Dob}, Email = {dbUser.Email} WHERE UserId = {dbUser.UserId}");

            if (rowsAffected > 0)
                return true;
            else
                return false;
        }


        public void UpdateLoginStreak(ViewUser user)
        {
            //if last login date is before today increment streak
            if (user.LastLogin < DateTime.Now)
            {
                //if streak is at 7 roll back to 1
                if (user.LoginStreak == 7)
                {
                    user.LoginStreak = 1;
                }
                else
                {
                    //otherwise increase by 1
                    user.LoginStreak = user.LoginStreak + 1;
                }
            }
            //update user in database with current information
            //_dbContext.Database.ExecuteSqlInterpolated($"UPDATE UserInfo SET LoginStreak = {user.LoginStreak} WHERE Username = {user.UserName};");
            
            user.LastLogin = DateTime.Now;
            // _dbContext.Database.ExecuteSqlInterpolated($"UPDATE UserInfo SET LastLogin = {user.LastLogin} WHERE Username = {user.UserName};");
            Update(user);

        }

    }
}
