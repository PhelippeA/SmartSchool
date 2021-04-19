using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Data
{
    public interface IRepository
    {
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         bool SaveChanges();

         Aluno [] GetAllAlunos(bool includeProfessor);
         Aluno [] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor);
         Aluno GetAlunoById(int alunoId, bool includeProfessor);
         Professor [] GetAllProfessores(bool includeAlunos);
         Professor [] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos);
         Professor GetProfessorById(int professorId, bool includeAlunos);
    }
}