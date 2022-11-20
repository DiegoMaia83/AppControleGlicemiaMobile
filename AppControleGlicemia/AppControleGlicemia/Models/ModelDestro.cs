using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppControleGlicemia.Models
{
    [Table("Destro")]
    public class ModelDestro
    {
        [PrimaryKey, AutoIncrement]
        public int DestroId { get; set; }

        [NotNull]
        public int ValorAferido { get; set; }

        [NotNull]
        public DateTime DataAferido { get; set; }

        public string InsulinaTipo { get; set; }

        public int InsulinaQuantidade { get; set; }

        [NotNull]
        public string Observacoes { get; set; }

        [Ignore]
        public string Stats { get; set; }
        [Ignore]
        public bool MostraInsulina { get; set; }
    }
}
