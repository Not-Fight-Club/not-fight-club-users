using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Repositories;


namespace UserServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardController : Controller
    {
        private readonly RewardRepository _rewardRepo = new RewardRepository();

        [HttpPost("/[action]")]
        public int GetReward([FromBody] ViewUser user)
        {

            return  _rewardRepo.GetReward(user);
        }

    }
}
