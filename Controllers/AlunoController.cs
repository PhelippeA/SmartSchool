using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public AlunoController() { }
        List<Aluno> alunos = new List<Aluno>{
            new Aluno(1,"Rand","Al'Thor","121212"),
            new Aluno(2,"Matrim","Cauthon","131313"),
            new Aluno(3,"Perrin","Aybara","141414")
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(alunos);
        }

        [HttpGet("byId")]
        public IActionResult GetById(int id)
        {
            var aluno = alunos.FirstOrDefault(al => al.Id == id); 
            
            if(aluno == null)
                return BadRequest("Aluno não encontrado");
                 
            return Ok(aluno);
        }

        [HttpPost()]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, Aluno aluno)
        {
            return Ok(aluno);
        }
    }
}