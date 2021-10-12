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
      _dbContext.Database.ExecuteSqlInterpolated($"Insert into UserInfo(UserName, PWord, Email, DOB, Active, LastLogin, LoginStreak) values({UserInfo.UserName},{UserInfo.Pword},{UserInfo.Email},{UserInfo.Dob},{UserInfo.Active}, {DateTime.Now}, {UserInfo.LoginStreak})");
      //save changes
      _dbContext.SaveChanges();
      //read UserInfo back from the db
      UserInfo registeredUserInfo = await _dbContext.UserInfos.FromSqlInterpolated($"select * from UserInfo where UserName = {UserInfo.UserName} and PWord ={UserInfo.Pword}").FirstOrDefaultAsync();

      return _mapper.ModelToViewModel(registeredUserInfo);
    }

    public async Task<ViewUser> Read(string email)
    {
      UserInfo user = await _dbContext.UserInfos.FromSqlInterpolated($"select * from UserInfo where email = {email}").FirstOrDefaultAsync();

      UpdateLoginStreak(user);

      UserInfo loggedUserInfo = await _dbContext.UserInfos.FromSqlInterpolated($"select * from UserInfo where email = {email}").FirstOrDefaultAsync();


      return _mapper.ModelToViewModel(loggedUserInfo);

    }

    public async Task<ViewUser> Update(ViewUser viewUser)
    {
      UserInfo user = _mapper.ViewModelToModel(viewUser);
      var users = _dbContext.Database.ExecuteSqlInterpolated($"Update UserInfo Set UserName = {user.UserName}, Email = {user.Email}, DOB = {user.Dob}, PWord = {user.Pword}, Bucks = {user.Bucks}, Active = {user.Active}, LoginStreak = {user.LoginStreak}, LastLogin = {user.LastLogin}, ProfilePic = {user.ProfilePic}, Where UserId={user.UserId}");

            await _dbContext.SaveChangesAsync();
            UserInfo updatedUser = await _dbContext.FindAsync<UserInfo>($"{user.UserId}");
      //UserInfo updatedUser = await _dbContext.UserInfos.FromSqlInterpolated($"select * from UserInfo where UserId = {user.UserId}").FirstOrDefaultAsync();
      return _mapper.ModelToViewModel(updatedUser);
    }


    public void UpdateLoginStreak(UserInfo user)
    {
      if (user.LoginStreak == 7)
      {
        user.LoginStreak = 1;
      }
      else
      {
        user.LoginStreak = user.LoginStreak + 1;
      }

      _dbContext.Database.ExecuteSqlInterpolated($"UPDATE UserInfo SET LoginStreak = {user.LoginStreak} WHERE Username = {user.UserName};");

      user.LastLogin = DateTime.Now;
      _dbContext.Database.ExecuteSqlInterpolated($"UPDATE UserInfo SET LastLogin = {user.LastLogin} WHERE Username = {user.UserName};");


    }

  }
}
