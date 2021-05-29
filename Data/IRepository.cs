using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Data
{
    public interface IRepository
    {
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         bool SaveChanges();

         Task<Aluno[]> GetAllAlunos(bool includeProfessor);
         Task<Aluno[]> GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor);
         Task<Aluno> GetAlunoById(int alunoId, bool includeProfessor);
         Task<Professor[]> GetAllProfessores(bool includeAlunos);
         Task<Professor[]> GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos);
         Task<Professor> GetProfessorById(int professorId, bool includeAlunos);
    }
}