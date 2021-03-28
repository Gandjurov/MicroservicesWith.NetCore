using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Database;
using UserService.Database.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    public class UserController : ApiController
    {
        private UsersDbContext context;
        public UserController(UsersDbContext context)
        {
            this.context = context;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            return context.Users.ToList();
        }

        // GET api/<UserController>/5
        [HttpGet(Id, Name = "Get")]
        public UserModel Get(int id)
        {
            return context.Users.Find(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status500InternalServerError);

            context.Users.Add(user);
            context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created, user);
        }

        // PUT api/<UserController>/5
        [HttpPut(Id)]
        public IActionResult Put(int id, [FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status500InternalServerError);

            UserModel currentUser = context.Users.Find(user.UserId);
            currentUser.Name = user.Name;

            context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created, user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete(Id)]
        public IActionResult Delete(int id)
        {
            UserModel user = context.Users.Find(id);

            if (user == null)
                return NotFound();

            context.Users.Remove(user);
            context.SaveChanges();

            return NoContent();
        }
    }
}
