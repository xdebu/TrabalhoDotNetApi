using System.Data.Entity;

namespace WebApi.Models
{
    public class Contexto : DbContext
    {
        public Contexto() : base ("Contexto")
        {

        }

        public DbSet<Tarefas> Tarefas { get; set; }
    }
}