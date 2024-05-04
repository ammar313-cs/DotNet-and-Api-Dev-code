using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jsonCrud.Models;
using jsonCrud.Data;
using api_crud.Models;

namespace api_crud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserDBHandler userDbHandler;

        public UserController()
        {
            userDbHandler = new UserDBHandler();
        }

     

        // GET api/user/{username}
        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            var user = await userDbHandler.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
