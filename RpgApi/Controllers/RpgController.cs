using RpgApi.Models;
using RpgApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RpgApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RpgController : ControllerBase
    {
        private readonly RPGService _rpgService;

        public RpgController(RPGService rpgService)
        {
            _rpgService = rpgService;
        }


        [HttpGet]
        public ActionResult<List<User>> Get() =>
            _rpgService.Get();


        [HttpGet("{id:length(24)}")]
        public ActionResult<User> Get(string id)
        {
            var user = _rpgService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            _rpgService.Create(user);

            return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
        }


        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, User userIn)
        {
            var user = _rpgService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _rpgService.Update(id, userIn);

            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _rpgService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _rpgService.Remove(user.Id);

            return NoContent();
        }
    }
}