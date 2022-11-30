using gyak10_b.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gyak10_b.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class BoatController : ControllerBase
    {
        [HttpGet]
        [Route("kerdesek/all")]
        public IActionResult hajo()
        {
            Models.HajosContext context = new Models.HajosContext();
            var kerdes = from x in context.Questions
                         select x;
            return Ok(kerdes);
        }


        [HttpGet]
        [Route("kerdesek/{id}")]
        public IActionResult mas(int id)
        {
            Models.HajosContext context = new HajosContext();
            var kerdes = from x in context.Questions
                         where x.QuestionId == id
                         select x;

            if (kerdes == null)
            {
                return BadRequest("Nincs ilyen sorszámú kérdés...");
            }

           
            return new JsonResult(kerdes.FirstOrDefault());          //jsonként jelenít meg, nem lesz [] benne
        }
        


    }
}
