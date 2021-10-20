using BusinessLayer.Interfaces;
using BusinessLayer.Repositories;
using DataLayerDBContext_DBContext;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models_DBModels;
using System;
using System.Collections;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace TestingLayer.RepoTests
{

    public class UserRepoTests
    {

        private static DbContextOptions<NotFightClubUserContext> _options;
        private static readonly IMapper<UserInfo, ViewUser> _mapper;
        private static readonly UserInfoRepository _userInfoRepository = new UserInfoRepository(_mapper);


        private ViewUser user = new ViewUser()
        {

            LastLogin = DateTime.Today,
            LoginStreak = 5
        };
        [Fact]
        public void ShouldNotIncrementLogin()
        {
            //arrange
            
            //act
            _userInfoRepository.UpdateLoginStreak(user);
            //assert
            Assert.Equal(5, user.LoginStreak);
        }

    }
}
