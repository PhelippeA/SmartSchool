using System;

namespace SmartSchool.WebApi.Dtos.ProfessorDtos
{
    public class ProfessorRegistrarDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Registro { get; set; }
        public DateTime DataNasc { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}