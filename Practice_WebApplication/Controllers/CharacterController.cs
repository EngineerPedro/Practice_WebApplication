using Microsoft.AspNetCore.Mvc;


namespace Practice_WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private static Character knight = new Character();

        [HttpGet(Name = "GetCharacter")]
        public IActionResult Get()
        {
            return Ok(knight);
        }
    }
}
