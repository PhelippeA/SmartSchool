using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Data
{
    public class Repository : IRepository
    {
        private readonly SmartSchoolContext _context;
        public Repository(SmartSchoolContext context){
            _context = context;
        }
        void IRepository.Add<T>(T entity) where T: class 
        {
            _context.Add(entity);
        }

        void IRepository.Update<T>(T entity) where T: class
        {
            _context.Update(entity);
        }
        void IRepository.Delete<T>(T entity) where T: class
        {
            _context.Remove(entity);
        }

        bool IRepository.SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<Aluno[]> GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            
            if(includeProfessor){
                query = query.Include(alu => alu.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(a => a.Id);
            
            return await query.ToArrayAsync();
        }

        public async Task<Aluno[]> GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            
            if(includeProfessor){
                query = query.Include(alu => alu.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(a => a.AlunosDisciplinas
                         .Any(ad => ad.DisciplinaId == disciplinaId));

            return await query.ToArrayAsync();
        }

        public async Task<Aluno> GetAlunoById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            
            if(includeProfessor){
                query = query.Include(alu => alu.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().Where(a => a.Id == alunoId);
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Professor[]> GetAllProfessores(bool includeAlunos)
        {
            IQueryable<Professor> query = _context.Professores;
            
            if(includeAlunos){
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }
            query = query.AsNoTracking().OrderBy(p => p.Id);
            
            return await query.ToArrayAsync();
        }

        public async Task<Professor[]> GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos)
        {
            IQueryable<Professor> query = _context.Professores;
            
            if(includeAlunos){
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(p => p.Id)
                         .Where(p => p.Disciplinas.Any(
                             d => d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)
                         ));
            
            return await query.ToArrayAsync();
        }

        public async Task<Professor> GetProfessorById(int professorId, bool includeAlunos)
        {
            IQueryable<Professor> query = _context.Professores;
            
            if(includeAlunos){
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                         .Where(p => p.Id == professorId);
            
            return await query.FirstOrDefaultAsync();
        }
    }
}