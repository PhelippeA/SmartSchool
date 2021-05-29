using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.Dtos.ProfessorDtos;
using SmartSchool.WebApi.Models;
using System.Collections.Generic;

namespace SmartSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController: ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper) {
            this._repo = repo;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(){
            var result = _repo.GetAllProfessores(true);
            // var professores = _mapper.Map<IEnumerable<ProfessorDto>>(result);
            
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(result));
        }

        [HttpGet("byId")]
        public IActionResult GetById(int id){
            var result = _repo.GetProfessorById(id, false);           
            if(result == null)
                return BadRequest("Professor não encontrado");
            
            var professor = _mapper.Map<ProfessorDto>(result);
                
            return Ok(professor);
        }
        
        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model){
            var professor = _mapper.Map<Professor>(model); 
            
            _repo.Add(professor);

            if(_repo.SaveChanges())
                return Created($"/api/professor/{professor.Id}", model);

            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model){
            var result = _repo.GetProfessorById(id, false);
            if(result == null)
                return BadRequest("Professor não encontrado");
            
            var professor = _mapper.Map<Professor>(model);
            _repo.Update(professor);

            if(_repo.SaveChanges())
                return Ok(model); 

            return BadRequest("Informação não atualizada");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model){
            var result = _repo.GetProfessorById(id, false);
            if(result == null)
                return BadRequest("Professor não encontrado");
            
            var professor = _mapper.Map<Professor>(model);
            _repo.Update(professor);

            if(_repo.SaveChanges())
                return Ok(model); 

            return BadRequest("Informação não atualizada");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            var professor = _repo.GetProfessorById(id, false);
            if(professor == null)
                return BadRequest("Professor não encontrado");           
                
            _repo.Delete(professor);
            if(_repo.SaveChanges()) 
                return Ok("Professor excluído");

            return Ok();
        }
    }
}