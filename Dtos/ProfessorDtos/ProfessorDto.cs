using System;
using System.Collections.Generic;

namespace SmartSchool.WebApi.Dtos.ProfessorDtos
{
    public class ProfessorDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Registro { get; set; }
        public int Idade { get; set; }
        public DateTime DataInicio { get; set; }
    }
}