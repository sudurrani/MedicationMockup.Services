using MedicationMockup.Application.Shared.Common.Dtos;
using MedicationMockup.Application.Shared.Dtos.User;
using MedicationMockup.Application.Shared.Interfaces;
using MedicationMockup.Application.Shared.Models;
using MedicationMockup.Application.Shared.Models.User;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedicationMockup.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        #region Services to be inject
        IUserAppService _userAppService;
        #endregion
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public UserController(IUserAppService userAppService
            , IMapper mapper
            , IConfiguration configuration
            , IWebHostEnvironment environment)
        {
            _userAppService = userAppService;
            _mapper = mapper;
            _configuration = configuration;
            _environment = environment;
        }

        /// <summary>
        /// User will provide Username  & password to singin to MedicationMockup.io markte place
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Signin")]
        [Produces(typeof(ResponseOutputDto))]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel)
        {
            var userInputDto = _mapper.Map<UserInputDto>(userLoginModel);

            var responseOutputDto = await _userAppService.Login(userInputDto);
            if (responseOutputDto.IsSuccess)
            {
                // Else we generate JSON Web Token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                  {
                    new Claim("Id", responseOutputDto.resultJSON.Id.ToString()),
                    new Claim(ClaimTypes.Name, responseOutputDto.resultJSON.Username)
                  }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token1 = tokenHandler.CreateToken(tokenDescriptor);
                responseOutputDto.resultJSON.Token = tokenHandler.WriteToken(token1);
                return Ok(responseOutputDto);
            }
            else
            {
                return Ok(responseOutputDto);
            }
        }

        /// <summary>
        /// This api is used to create user account to use marketplace and mobile app
        /// </summary>
        /// <param name="userRegisterModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Signup")]
        [Produces(typeof(ResponseOutputDto))]
        public async Task<IActionResult> Register( UserRegisterModel userRegisterModel)
        {
            var userInputDto = _mapper.Map<UserInputDto>(userRegisterModel);
            
            var response = await _userAppService.Create(userInputDto);
            return Ok(response);

        }

        /// <summary>
        /// This API will return all available/created users
        /// </summary>
        /// <returns>
        /// Will return JSON object
        /// </returns>
        ///        
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll")]
        [Produces(typeof(ResponseOutputDto))]
        public async Task<IActionResult> GetAll()
        {
            var responseOutputDto = await _userAppService.GetAll();
            return Ok(responseOutputDto);
        }

        /// <summary>
        /// This api will get user by id and will return single record if id is valid 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]
        [Produces(typeof(ResponseOutputDto))]
        public async Task<IActionResult> GetById(long id)
        {
            var responseOutputDto = await _userAppService.GetById(id);
            return Ok(responseOutputDto);
        }






        [HttpPost]
        [Route("Update")]
        [Produces(typeof(ResponseOutputDto))]
        public async Task<IActionResult> Update([FromForm] UserUpdateModel userUpdateModel)
        {
            var userInputDto = _mapper.Map<UserInputDto>(userUpdateModel);
           
            var response = await _userAppService.Update(userInputDto);
            return Ok(response);
        }
        [HttpPost]
        [Route("Delete")]
        [Produces(typeof(ResponseOutputDto))]
        public async Task<IActionResult> Delete(UserDeleteModel userDeleteModel)
        {
            var userInputDto = _mapper.Map<UserInputDto>(userDeleteModel);
            var response = await _userAppService.Delete(userInputDto);
            return Ok(response);
        }
    }
}
