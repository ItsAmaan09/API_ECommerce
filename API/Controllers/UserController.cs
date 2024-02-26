using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Manager;
using Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public UserManager userManager;

        [HttpPost]
        [Route("Get")]

        public IActionResult Get()
        {
            this.userManager = new UserManager();
            return Ok(this.userManager.GetUsers());
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(User user)
        {
            this.userManager = new UserManager();
            return Ok(this.userManager.AddUser(user));
        }
    }
}