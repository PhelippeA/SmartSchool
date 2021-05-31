using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.Dtos.AlunoDtos;
using SmartSchool.WebApi.Helpers;
using SmartSchool.WebApi.Models;
using SmartSchool.WebApi.Helpers.Extensions;

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
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunos(pageParams, true);
            var alunosDto = _mapper.Map<IEnumerable<AlunoDto>>(alunos); 
            
            Response.AddPagination(alunos.CurrentPage, alunos.TotalPages, alunos.PageSize, alunos.TotalItems);

            return Ok(alunosDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var aluno = await _repo.GetAlunoById(id, false);

            if (aluno == null) 
                return BadRequest("Aluno não encontrado");
            
            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
        }

        [HttpPost()]
        public async Task<IActionResult> Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            await _repo.Add(aluno);

            if(_repo.SaveChanges())
                return Created($"api/aluno/{aluno.Id}", model);

            return BadRequest("Aluno não encontrado"); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            aluno = await _repo.GetAlunoById(id, false);

            if(aluno == null) 
                return BadRequest("Aluno não encontrado");
            
            _repo.Update(aluno);

            if(_repo.SaveChanges())
                return Ok(aluno);
            
            return BadRequest("Aluno não cadastrado");
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            aluno = await _repo.GetAlunoById(id, false);
            if(aluno == null) 
                return BadRequest("Aluno não encontrado");
            
            _repo.Update(aluno);
            if(_repo.SaveChanges())
                return Ok(model);
            
            return BadRequest("Aluno não cadastrado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var aluno = await _repo.GetAlunoById(id, false);
            if(aluno == null) return BadRequest("Aluno não encontrado");
            
            _repo.Delete(aluno);
            if(_repo.SaveChanges())
                return Ok();
            
            return BadRequest("Aluno não deletado");
        }
    }
}