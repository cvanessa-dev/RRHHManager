using System.ComponentModel.DataAnnotations.Schema;

namespace RRHHManager.Models
{
    public class HistorialSalarial
    {
        public int Id { get; set; }

        public int EmpleadoId { get; set; }

        public Empleado? Empleado { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalarioAnterior { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalarioNuevo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PorcentajeAumento { get; set; }

        public DateTime FechaCambio { get; set; }
        public string Motivo { get; set; } = string.Empty;

    }
}
