using Microsoft.AspNetCore.Mvc;


namespace Practice_WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository, IMapper mapper, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authRepository.Register(
                               new User { Username = request.Username }, request.Password
                                          );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await _authRepository.Login(
                               request.Username, request.Password
                                          );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
