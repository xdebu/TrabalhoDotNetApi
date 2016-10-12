using System;

namespace AppCliente.Model
{
    public class Tarefa
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public bool Concluido { get; set; }

        public DateTime DataLimite { get; set; }

        public string Username { get; set; }
    }
}
