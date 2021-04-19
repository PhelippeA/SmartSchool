using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;
        public AlunoController(IRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllAlunos(true);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);

            if (aluno == null)
                return BadRequest("Aluno não encontrado");

            return Ok(aluno);
        }

        [HttpPost()]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
            if(_repo.SaveChanges())
                return Created($"api/aluno/{aluno.Id}", aluno);

            return BadRequest("Aluno não encontrado"); 
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var al = _repo.GetAlunoById(id, false);
            if(al == null) 
                return BadRequest("Aluno não encontrado");
            
            _repo.Update(aluno);
            if(_repo.SaveChanges())
                return Ok(aluno);
            
            return BadRequest("Aluno não cadastrado");
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var al = _repo.GetAlunoById(id, false);
            if(al == null) 
                return BadRequest("Aluno não encontrado");
            
            _repo.Update(aluno);
            if(_repo.SaveChanges())
                return Ok(aluno);
            
            return BadRequest("Aluno não cadastrado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var al = _repo.GetAlunoById(id, false);
            if(al == null) return BadRequest("Aluno não encontrado");
            
            _repo.Delete(al);
            if(_repo.SaveChanges())
                return Ok();
            
            return BadRequest("Aluno não deletado");
        }
    }
}