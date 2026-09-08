namespace RRHHManager.Models
{
    public class HistorialLaboral
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public Empleado? Empleado { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public string? Observaciones { get; set; }

    }
}
