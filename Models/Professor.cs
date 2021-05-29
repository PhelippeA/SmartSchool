using System;
using System.Collections.Generic;

namespace SmartSchool.WebApi.Models
{
    public class Professor
    {
        public Professor() {}
        
        public Professor(int id, int registro, string nome, string sobrenome)
        {
            this.Id = id;
            this.Registro = registro;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Registro { get; set; }
        public DateTime DataNasc { get; set; }
        public DateTime DataInicio { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;
        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}