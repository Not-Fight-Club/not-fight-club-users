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
      await _dbContext.SaveChangesAsync();
      //read UserInfo back from the db
      UserInfo registeredUserInfo = await _dbContext.UserInfos.FromSqlInterpolated($"select * from UserInfo where UserName = {UserInfo.UserName} and PWord ={UserInfo.Pword} and Email ={UserInfo.Email}").FirstOrDefaultAsync();
      return _mapper.ModelToViewModel(registeredUserInfo);
    }
    public async Task<ViewUser> Read(string email)
    {
      //Set bd user model with information from database
      UserInfo user = await _dbContext.UserInfos.FromSqlInterpolated($"select * from UserInfo where email = {email}").FirstOrDefaultAsync();

           // _dbContext.

      ViewUser viewUser = _mapper.ModelToViewModel(user);

      //update login streak for logging in 
      UpdateLoginStreak(viewUser);

      //UserInfo loggedUserInfo = await _dbContext.UserInfos.FromSqlInterpolated($"select * from UserInfo where email = {email}").FirstOrDefaultAsync();


      return viewUser;
    }

    //     public async Task<ViewUser> Update(ViewUser viewUser)
    // {
    //   UserInfo user = _mapper.ViewModelToModel(viewUser);
    //   var users = _dbContext.Database.ExecuteSqlInterpolated($"Update UserInfo Set UserName = {user.UserName}, Email = {user.Email}, DOB = {user.Dob}, PWord = {user.Pword}, Bucks = {user.Bucks}, Active = {user.Active}, LoginStreak = {user.LoginStreak}, LastLogin = {user.LastLogin}, ProfilePic = {user.ProfilePic}, Where UserId={user.UserId}");
    //         await _dbContext.SaveChangesAsync();
    //         UserInfo updatedUser = await _dbContext.FindAsync<UserInfo>($"{user.UserId}");
    //   //UserInfo updatedUser = await _dbContext.UserInfos.FromSqlInterpolated($"select * from UserInfo where UserId = {user.UserId}").FirstOrDefaultAsync();
    //   return _mapper.ModelToViewModel(updatedUser);
    // }
    public async Task<bool> Update(ViewUser user)
    {
      UserInfo dbUser = _mapper.ViewModelToModel(user);

            //int rowsAffected = await _dbContext.Database.ExecuteSqlInterpolatedAsync($"UPDATE UserInfo SET LastLogin = {dbUser.LastLogin}, LoginStreak = {dbUser.LoginStreak}, ProfilePic = {dbUser.ProfilePic}, UserName = {dbUser.UserName}, PWord = {dbUser.Pword}, DOB = {dbUser.Dob}, Email = {dbUser.Email}, RewardCollected = { dbUser.RewardCollected} WHERE UserId = {dbUser.UserId}");
            //_dbContext.Entry(dbUser).Reload();
            
            var returned = await _dbContext.UserInfos.SingleOrDefaultAsync(u => u.UserId == user.UserId);

            returned.UserName = user.UserName;
            returned.ProfilePic = user.ProfilePic;
            returned.Dob = user.Dob;
            returned.Bucks = user.Bucks;
            returned.Email = user.Email;
            returned.LastLogin = user.LastLogin;
            returned.LoginStreak = user.LoginStreak;
            returned.Pword = user.Pword;
            returned.RewardCollected = user.RewardCollected;
            returned.Active = user.Active;
            

            await _dbContext.SaveChangesAsync();
            //if (rowsAffected > 0)
            //    return true;
            //else
            //    return false;
            return true;
            
    }


    public async void UpdateLoginStreak(ViewUser user)
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
       await Update(user);
      
        //what does this line of code do?
        //what is the dbUser?
        //Do we need to add something back?
       // int rowsAffected =_dbContext.Database.ExecuteSqlInterpolated($"UPDATE UserInfo SET LastLogin = {dbUser.LastLogin}, LoginStreak = {dbUser.LoginStreak}, RewardCollected = {dbUser.RewardCollected}, ProfilePic = {dbUser.ProfilePic}, UserName = {dbUser.UserName}, PWord = {dbUser.Pword}, DOB = {dbUser.Dob}, Email = {dbUser.Email} WHERE UserId = {dbUser.UserId}");
            _dbContext.SaveChanges();



    }

    public async Task<ViewUser> ReadUser(Guid id)
    {
      UserInfo product = await _dbContext.UserInfos.FromSqlInterpolated($"select * from UserInfo where UserId = {id}").FirstOrDefaultAsync();

      return _mapper.ModelToViewModel(product);
    }

    public async Task<ViewUser> Delete(Guid id)
    {

      UserInfo user = await _dbContext.UserInfos.FromSqlInterpolated($"select * from UserInfo Where UserId = {id}").FirstOrDefaultAsync();
      _dbContext.Remove(user);
      await _dbContext.SaveChangesAsync();

      return _mapper.ModelToViewModel(user);

    }

  }
}
