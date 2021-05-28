using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.Dtos;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);

            if (aluno == null) 
                return BadRequest("Aluno não encontrado");
            
            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(aluno);
        }

        [HttpPost()]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            _repo.Add(aluno);

            if(_repo.SaveChanges())
                return Created($"api/aluno/{aluno.Id}", model);

            return BadRequest("Aluno não encontrado"); 
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            aluno = _repo.GetAlunoById(id, false);

            if(aluno == null) 
                return BadRequest("Aluno não encontrado");
            
            _repo.Update(aluno);

            if(_repo.SaveChanges())
                return Ok(aluno);
            
            return BadRequest("Aluno não cadastrado");
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            aluno = _repo.GetAlunoById(id, false);
            if(aluno == null) 
                return BadRequest("Aluno não encontrado");
            
            _repo.Update(aluno);
            if(_repo.SaveChanges())
                return Ok(model);
            
            return BadRequest("Aluno não cadastrado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if(aluno == null) return BadRequest("Aluno não encontrado");
            
            _repo.Delete(aluno);
            if(_repo.SaveChanges())
                return Ok();
            
            return BadRequest("Aluno não deletado");
        }
    }
}