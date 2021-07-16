using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicsStore.API.Services;
using ElectronicsStore.API.Model;
using AutoMapper;
using ElectronicsStore.API.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace ElectronicsStore.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IElectronicsStoreRepository _electronicsStoreRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public UsersController(IElectronicsStoreRepository electronicsStoreRepository,
            IMapper mapper, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration
            )
        {
            _electronicsStoreRepository = electronicsStoreRepository ??
                throw new ArgumentNullException(nameof(electronicsStoreRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


        [Authorize]
        [HttpGet]
        public ActionResult GetUsers()
        {
            var usersFromRepo = _electronicsStoreRepository.GetUsers();
           
            return Ok(_mapper.Map<IEnumerable<UserDto>>(usersFromRepo));
        
        }
        

        [HttpGet("{userId}", Name = "GetUser")]
        public IActionResult GetUser(string userId)
        {
            var userFromRepo = _electronicsStoreRepository.GetUser(userId);
            if(userFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDto>(userFromRepo));
        }

        


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.EmailAddress);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var userExists = await userManager.FindByEmailAsync(userForRegistrationDto.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

             User user = new User()
             {

                 Email = userForRegistrationDto.Email,
                 SecurityStamp = Guid.NewGuid().ToString(),
                 FirstName = userForRegistrationDto.FirstName,
                 LastName = userForRegistrationDto.LastName,
                 UserName = userForRegistrationDto.UserName,
                 Address = userForRegistrationDto.Address
             };
             

             
            var result = await userManager.CreateAsync(user, userForRegistrationDto.Password);
            
            
            if (!result.Succeeded)
                 return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
             
            
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        } 

        

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserForRegistrationDto userForRegistrationDto)  
        {  
            var userExists = await userManager.FindByEmailAsync(userForRegistrationDto.Email);  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });  
  
            User user = new User()  
            {
                
                Email = userForRegistrationDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = userForRegistrationDto.FirstName,
                LastName = userForRegistrationDto.LastName,
                UserName = userForRegistrationDto.UserName,                
                Address = userForRegistrationDto.Address
            };  
            var result = await userManager.CreateAsync(user, userForRegistrationDto.Password);  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });  
  
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))  
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));  
            if (!await roleManager.RoleExistsAsync(UserRoles.User))  
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));  
  
            if (await roleManager.RoleExistsAsync(UserRoles.Admin))  
            {  
                await userManager.AddToRoleAsync(user, UserRoles.Admin);  
            }  
  
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });  
        }  

        [HttpPut("{userId}")]
        public ActionResult UpdateUser(string userId,
            UserForUpdateDto user)
        {
            if (!_electronicsStoreRepository.UserExists(userId))
            {
                return NotFound();
            }

            var userFromRepo = _electronicsStoreRepository.GetUser(userId);
            if (userFromRepo == null)
            {
                return NotFound();
            }

            //Map the entity to a productForUpdateDto
            //Apply the updated fields to that Dto
            //Map the productForUpdateDto back to that entity
            _mapper.Map(user, userFromRepo);

            _electronicsStoreRepository.UpdateUser(userFromRepo);
            _electronicsStoreRepository.Save();
            return NoContent();
        }


    }
}
