using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // Base de datos en memoria
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "Alice Smith", Email = "alice@example.com" },
            new User { Id = 2, Name = "Bob Jones", Email = "bob@example.com" }
        };

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_users);
        }

        // GET: api/users/1
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User newUser)
        {
            // La validación se maneja automáticamente gracias a las DataAnnotations en el modelo
            if (!ModelState.IsValid) return BadRequest(ModelState);

            newUser.Id = _users.Max(u => u.Id) + 1;
            _users.Add(newUser);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        // PUT: api/users/1
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            return NoContent();
        }

        // DELETE: api/users/1
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            _users.Remove(user);
            return NoContent();
        }
    }
}