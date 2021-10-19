using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Microsoft.Extensions.Logging;
using BusinessLayer.Interfaces;
using DataLayerDBContext_DBContext;
using Models_DBModels;

namespace UserServiceApi.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class UserController : Controller
  {
    private readonly NotFightClubUserContext _context;
    private readonly ILogger<UserController> _logger;
    private readonly IRepository<ViewUser, string> _ur;

    public UserController(IRepository<ViewUser, string> ur, NotFightClubUserContext context, ILogger<UserController> logger)
    {
      _context = context;
      _logger = logger;
      _ur = ur;
    }



    [HttpGet("/[action]/{email}")]
    //what if multiple 
    public async Task<ActionResult<ViewUser>> Login(string email)
    {

      if (!ModelState.IsValid) return BadRequest("Invalid data.");

      var loggedUser = await _ur.Read(email);


      _logger.LogInformation($"{loggedUser.UserName} logged in");
      _logger.LogCritical("Critical Test Log");

      return Ok(loggedUser);
    }

    [HttpPost("/[action]")]
    public async Task<ActionResult<ViewUser>> Register([FromBody] ViewUser viewUser)
    {
      if (!ModelState.IsValid) return BadRequest("Invalid data.");
      //call to repository to add user
      //return the result
      //Console.WriteLine(viewUser);
      var registeredUser = await _ur.Add(viewUser);


      _logger.LogInformation($"Registered New User with Username: {viewUser.UserName}");
      return Ok(registeredUser);
    }

    //// GET: User
    //public async Task<IActionResult> Index()
    //{
    //    return View(await _context.UserInfos.ToListAsync());
    //}

    // GET: User/Details/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ViewUser>> GetUserById(Guid id)
    {
      ViewUser user = await _ur.ReadUser(id);

      _logger.LogInformation($"{user.UserName} was selected.");
      return Ok(user);
    }

    [HttpPut("/edit-profile/{id}")]
    public async Task<ActionResult<ViewUser>> PutUsers(Guid id, [FromBody] ViewUser user)
    {
      _logger.LogInformation("Hit me");
      if (id != user.UserId)
      {
        return BadRequest();
      }
      _logger.LogInformation($"Updating {user.UserName}'s profile");
      _logger.LogInformation($"{user}");
      await _ur.Update(user);
      _logger.LogInformation($"Updated details: Username: {user.UserName} || Email: {user.Email} || DOB: {user.Dob}");

      return Ok(user);
    }
    // }

    //// GET: User/Create
    //public IActionResult Create()
    //{
    //    return View();
    //}

    //// POST: User/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("UserId,UserName,Pword,Email,Dob,Bucks,Active")] UserInfo userInfo)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        userInfo.UserId = Guid.NewGuid();
    //        _context.Add(userInfo);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(userInfo);
    //}

    //// GET: User/Edit/5
    //public async Task<IActionResult> Edit(Guid? id)
    //{
    //    if (id == null)
    //    {
    //        return NotFound();
    //    }

    //    var userInfo = await _context.UserInfos.FindAsync(id);
    //    if (userInfo == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(userInfo);
    //}

    //// POST: User/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(Guid id, [Bind("UserId,UserName,Pword,Email,Dob,Bucks,Active")] UserInfo userInfo)
    //{
    //    if (id != userInfo.UserId)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(userInfo);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!UserInfoExists(userInfo.UserId))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(userInfo);
    //}

    //// GET: User/Delete/5
    //public async Task<IActionResult> Delete(Guid? id)
    //{
    //    if (id == null)
    //    {
    //        return NotFound();
    //    }

    //    var userInfo = await _context.UserInfos
    //        .FirstOrDefaultAsync(m => m.UserId == id);
    //    if (userInfo == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(userInfo);
    //}

    //// POST: User/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(Guid id)
    //{
    //    var userInfo = await _context.UserInfos.FindAsync(id);
    //    _context.UserInfos.Remove(userInfo);
    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    /// <summary>
    /// Update a user's total bucks by adding the provided value to the provided user
    /// </summary>
    /// <param name="data">an object containing the userId (guid) and the change in bucks (+/- int)</param>
    /// <returns></returns>
    private async Task<ViewUserBucks> UpdateUserTotal(ViewUserBucks data)
    {
     //get view user to run logic on 
      var user = await _ur.ReadUser(data.UserId);
      //send view user into method to adjust bucks
      if (user == null)
      {
        throw new ArgumentException($"User with id {data.UserId} doesn't exist");
      }
      if (user.Bucks == null)
      {
        user.Bucks = 0;
      }
      // 
      int newTotal = (int)user.Bucks + data.TotalCurrency;
      if (newTotal < 0)
      {
        throw new ArgumentException("User bucks total cannot go negative");
      }
      user.Bucks = newTotal;
      await _ur.Update(user);
      data.TotalCurrency = newTotal;
      return data;
    }

    [HttpPut("/[action]")]
    //
    public async Task<ActionResult<ViewUserBucks>> UpdateTotal(ViewUserBucks viewUserBucks)
    {
      //check model
      if (!ModelState.IsValid) return BadRequest("Invalid data.");

      var result = await UpdateUserTotal(viewUserBucks);

      return Ok(result);
    }

    [HttpPut("/[action]")]
    public async Task<ActionResult<ViewUserBucks>> UpdateTotalList(List<ViewUserBucks> totalsToUpdate)
    {
      if (!ModelState.IsValid) return BadRequest("Invalid data.");

      var results = new List<ViewUserBucks>();
      foreach (var toUpdate in totalsToUpdate)
      {
        var result = await UpdateUserTotal(toUpdate);
        results.Add(result);
      }
      return Ok(results);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ViewUser>> DeleteUser(Guid id)
    {
      await _ur.Delete(id);
      return Ok();
    }

    private bool UserExists(Guid id)
    {
      return _context.UserInfos.Any(e => e.UserId == id);
    }
  }
}
