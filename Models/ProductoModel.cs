using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace POOII_CL2_TorresMirandaJuanCarlos.Models
{
    public class ProductoModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre  es obligatorio")]
        public string?  nombre  { get; set; }

        [Required(ErrorMessage = "El idtipo  es obligatorio")]
        public int  idtipo { get; set; }

        [Required(ErrorMessage = "El precio  es obligatorio")]
        public decimal precio { get; set; }

        [Required(ErrorMessage = "La fehca  es obligatorio")]
        public DateTime fecha { get; set;}
        public string? foto { get; set; }
    }
}
