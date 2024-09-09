using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using api.Dtos.Account;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountRegisterDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser
                {
                    UserName = dto.Username,
                    Email = dto.Email
                };


                // Create User first
                // Unit of work!


                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var userCreationResult = await _userManager.CreateAsync(appUser, dto.Password);
                    if (userCreationResult.Succeeded)
                    {
                        // and if operation above is successful, add user to role.
                        // ! SATEFY: what if user creating operation succeeds but role adding operation fails?
                        // ! It just fails with user being not assigned with any role.
                        var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                        if (roleResult.Succeeded)
                        {
                            transaction.Complete();
                            return Ok("User created successfully");
                        }
                        else
                        {
                            Console.WriteLine("Error !");
                            return StatusCode(500, roleResult.Errors);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error3 !");
                        return StatusCode(500, userCreationResult.Errors);
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}