using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Extensions;
using api.Models;
using api.ParamObjects.Comment;
using api.ParamObjects.Portfolio;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController(UserManager<AppUser> userManager, StockRepository stockRepository, PortfolioRepository portfolioRepository) : ControllerBase
    {
        private readonly PortfolioRepository _portfolioRepository = portfolioRepository;



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            // `User` comes from ControllerBase! It is a property that is available to all controllers
            var userId = User.GetUserId();


            var userPortfolio = await _portfolioRepository.Query(new PGetUserPortfolio { AppUserId = int.Parse(userId) });

            return Ok(userPortfolio);
        }


        [HttpDelete("{stockId:int}")]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(int stockId)
        {
            var userId = User.GetUserId();
            var userPortfolio = await _portfolioRepository.Query(new PGetUserPortfolio { AppUserId = int.Parse(userId) });
            if (userPortfolio == null && userPortfolio.Find(x => x.Id == stockId) == null)
            {
                return NotFound();
            }

            await _portfolioRepository.Execute(new PDeletePortfolio { StockId = stockId });

            return Ok();
        }
    }
}