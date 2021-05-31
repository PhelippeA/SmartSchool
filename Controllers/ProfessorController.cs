using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.Dtos.ProfessorDtos;
using SmartSchool.WebApi.Models;
using System.Collections.Generic;
using SmartSchool.WebApi.Helpers.Extensions;
using SmartSchool.WebApi.Helpers;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams){
            var profs = await _repo.GetAllProfessores(pageParams,true);
            var professoresDto = _mapper.Map<IEnumerable<ProfessorDto>>(profs);
            
            Response.AddPagination(profs.CurrentPage, profs.TotalPages, profs.PageSize, profs.TotalItems);

            return Ok(professoresDto);
        }

        [HttpGet("byId")]
        public async Task<IActionResult> GetById(int id){
            var result = await _repo.GetProfessorById(id, false);           
            if(result == null)
                return BadRequest("Professor não encontrado");
            
            var professor = _mapper.Map<ProfessorDto>(result);
                
            return Ok(professor);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(ProfessorRegistrarDto model){
            var professor = _mapper.Map<Professor>(model); 
            
            await _repo.Add(professor);

            if(_repo.SaveChanges())
                return Created($"/api/professor/{professor.Id}", model);

            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProfessorRegistrarDto model){
            var result = await _repo.GetProfessorById(id, false);
            if(result == null)
                return BadRequest("Professor não encontrado");
            
            var professor = _mapper.Map<Professor>(model);
            _repo.Update(professor);

            if(_repo.SaveChanges())
                return Ok(model); 

            return BadRequest("Informação não atualizada");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, ProfessorRegistrarDto model){
            var result = await _repo.GetProfessorById(id, false);
            if(result == null)
                return BadRequest("Professor não encontrado");
            
            var professor = _mapper.Map<Professor>(model);
            _repo.Update(professor);

            if(_repo.SaveChanges())
                return Ok(model); 

            return BadRequest("Informação não atualizada");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){
            var professor = await _repo.GetProfessorById(id, false);
            if(professor == null)
                return BadRequest("Professor não encontrado");           
                
            _repo.Delete(professor);
            if(_repo.SaveChanges()) 
                return Ok("Professor excluído");

            return Ok();
        }
    }
}