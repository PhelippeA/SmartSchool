using Microsoft.AspNetCore.Mvc;

namespace SmartSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController: ControllerBase
    {
        public ProfessorController() {}
        [HttpGet]
        public IActionResult Get(){
            return Ok("Professor: Marta, Paula, Lucas, Rafa");
        }   
    }
}