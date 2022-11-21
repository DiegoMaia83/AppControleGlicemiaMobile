using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppControleGlicemia.Models
{
    [Table("Insulina")]
    public class ModeInsulina
    {
        [PrimaryKey, AutoIncrement]
        public int InsulinaId { get; set; }

        [NotNull]
        public string Tipo { get; set; }
    }
}
