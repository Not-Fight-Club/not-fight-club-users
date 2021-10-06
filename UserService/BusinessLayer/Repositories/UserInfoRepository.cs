using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using DataLayerDBContext.DBModels;
using Microsoft.EntityFrameworkCore;
using Models.DBModels;
using Models.Models;

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
            //add to the db
            _dbContext.Database.ExecuteSqlInterpolated($"Insert into UserInfo(UserName, PWord, Email, DOB, Active) values({UserInfo.UserName},{UserInfo.Pword},{UserInfo.Email},{UserInfo.Dob},{UserInfo.Active})");
            //save changes
            _dbContext.SaveChanges();
            //read UserInfo back from the db
            UserInfo registeredUserInfo = await _dbContext.UserInfos.FromSqlInterpolated($"select * from UserInfo where UserName = {UserInfo.UserName} and PWord ={UserInfo.Pword}").FirstOrDefaultAsync();

            return _mapper.ModelToViewModel(registeredUserInfo);
        }

        public async Task<ViewUser> Read(string email)
        {
            UserInfo loggedUserInfo = await _dbContext.UserInfos.FromSqlInterpolated($"select * from UserInfo where email = {email}").FirstOrDefaultAsync();

            return _mapper.ModelToViewModel(loggedUserInfo);

        }

        public Task<ViewUser> Read(int obj)
        {
            throw new NotImplementedException();
        }

        public Task<List<ViewUser>> Read()
        {
            throw new NotImplementedException();
        }

    }
}
