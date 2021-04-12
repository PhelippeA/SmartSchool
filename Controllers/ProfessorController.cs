using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController: ControllerBase
    {
        private readonly SmartSchoolContext _context;
        public ProfessorController(SmartSchoolContext context) {
            this._context = context;
        }

        [HttpGet]
        public IActionResult Get(){
            return Ok(_context.Professores);
        }

        [HttpGet("byId")]
        public IActionResult GetById(int id){
            var professor = _context.Professores.FirstOrDefault(prof => prof.Id == id);
            
            if(professor == null)
                return BadRequest("Professor não encontrado");
                
            return Ok(professor);
        }

        [HttpGet("byId")]
        public IActionResult GetByName(string nome){
            var professor = _context.Professores.FirstOrDefault(
                prof => prof.Nome.Contains(nome)
            );
            
            if(professor == null)
                return BadRequest("Professor não encontrado");

            return Ok(professor);
        }
        
        [HttpPost]
        public IActionResult Post(Professor professor){
            _context.Add(professor);
            _context.SaveChanges();

            return Created($"/api/professor/{professor.Id}", professor);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor){
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);

            if(prof == null)
                return BadRequest("Professor não encontrado");
            
            _context.Update(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor){
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);

            if(prof == null)
                return BadRequest("Professor não encontrado");
            
            _context.Update(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            var professor = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            
            _context.Remove(professor);
            _context.SaveChanges();

            if(professor == null)
                return BadRequest("Professor não encontrado");           

            return Ok();
        }

    }
}