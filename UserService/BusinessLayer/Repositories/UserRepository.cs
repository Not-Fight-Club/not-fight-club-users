using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using Models.Models;

namespace BusinessLayer.Repositories
{
    class UserRepository /*: IRepository<ViewUser, string>*/
    {
        // private readonly P2_NotFightClubContext _dbContext = new P2_NotFightClubContext();
        //private readonly IMapper<User, ViewUser> _mapper;

        //public UserRepository(IMapper<User, ViewUser> mapper)
        //{
        //    _mapper = mapper;
        //}

    //    public async Task<ViewUser> Add(ViewUser viewUser)
    //    {
    //        //check if the user already exists if so decline the entry ( implement later)
    //        //convert to User with Mapper class
    //        User user = _mapper.ViewModelToModel(viewUser);
    //        //add to the db
    //        _dbContext.Database.ExecuteSqlInterpolated($"Insert into User(UserName, PWord, Email, DOB) values({user.UserName},{user.Pword},{user.Email},{user.Dob})");
    //        //save changes
    //        _dbContext.SaveChanges();
    //        //read user back from the db
    //        User registeredUser =  await _dbContext.Users.FromSqlInterpolated($"select * from User where UserName = {user.UserName} and PWord ={user.Pword}").FirstOrDefaultAsync();

    //       return _mapper.ModelToViewModel(registeredUser);
    //    }

    //    public async Task<ViewUser> Read(string email)
    //    {
    //      User loggedUser =  await _dbContext.Users.FromSqlInterpolated($"select * from User where email = {email}").FirstOrDefaultAsync();
            
    //            return _mapper.ModelToViewModel(loggedUser);
            
    //    }

    //    public Task<ViewUser> Read(int obj)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<List<ViewUser>> Read()
    //    {
    //        throw new NotImplementedException();
    //    }

    }
}
