using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SarpunWebAPI.Data;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace SarpunWebAPI.Controllers
{
    public class TokenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
 

        public TokenController(ApplicationDbContext context )
        {
            _context = context; 
        }

        [Route("/token")]
        [HttpPost]
        public async Task<IActionResult> Create(string username, string password)
        { 
            if (await IsValidUsernameAndPassword(username,password) == false)
            {
                return BadRequest();
            }
            else
            {
                return new ObjectResult(await GenerateToken(username));
            }
        } 

        private async Task<bool> IsValidUsernameAndPassword(string username, string password)
        {
            
            //TODO implement usermanager and  checkpassword
            // var user = await _usermanager.FindByEmailAsync(username);
            // return await _userManager.CheckPasswordAsync(user,password);

            return await Task.Run(() =>
            {
                if (username == "aliihsan@aliihsan.com" && password == "*Ali157423")
                {
                    return true;
                }
                return false;
            });

              

 
        }

        private async Task<dynamic> GenerateToken(string username)
        {
            //var user = await _usermanager.FindByEmailAsync(username);

            //var roles = from ur in _context.UserRoles
            //            join r in _context.Roles on ur.RoleId equals r.Id
            //            where ur.UserId == user.Id 
            //            select new {ur.UserId, ur.RoleId, r.Name };

            return await Task.Run(() =>
            {
                // user.Id
                string user_id = "userId1";
                //roles
                List<Tuple<string, string, string>> roles = new List<Tuple<string, string, string>>()
            {
            new Tuple<string, string, string> ("userId1","role1","rolename1"),
            new Tuple<string, string, string> ("userId1","role2","rolename2")
            };


                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier,user_id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(5)).ToUnixTimeSeconds().ToString())
            };

                foreach (var role in roles)
                {
                    //claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    claims.Add(new Claim(ClaimTypes.Role, role.Item3));
                }

                var token = new JwtSecurityToken(
                    new JwtHeader(
                        new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BUM BE YARAG GARIDES DACMIN GOS BUM")),
                            SecurityAlgorithms.HmacSha256)),
                        new JwtPayload(claims));

                var output = new
                {
                    Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserName = username
                };

                return output;
            });
            
                 
        }


    }
}