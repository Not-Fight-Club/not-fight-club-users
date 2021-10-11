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
        public async Task<ActionResult<ViewUser>> Login(string email)
        {

            if (!ModelState.IsValid) return BadRequest("Invalid data.");

            var loggedUser = await _ur.Read(email);

            _logger.LogInformation($"{loggedUser.UserName} logged in");

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

        //// GET: User/Details/5
        //public async Task<IActionResult> Details(Guid? id)
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

        //private bool UserInfoExists(Guid id)
        //{
        //    return _context.UserInfos.Any(e => e.UserId == id);
        //}
    }
}
