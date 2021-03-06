using System;

namespace SmartSchool.WebApi.Models
{
    public class AlunoDisciplina
    {
        public AlunoDisciplina(){}
        public AlunoDisciplina(int alunoId, int disciplinaId)
        {
            this.AlunoId = alunoId;
            this.DisciplinaId = disciplinaId;

        }
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public DateTime DataInicio { get; set; } = DateTime.Now;
        public DateTime DataFim { get; set; }
        public double? Nota { get; set; } = null;
        public Aluno Aluno { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}