using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.DTO
{
    public class PrecioDto
    {
        public Guid Precioid { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PrecioActual { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Promocion { get; set; }
        public Guid Cursoid { get; set; }
    }
}
