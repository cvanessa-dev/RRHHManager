namespace RRHHManager.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Identidad { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }

        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }

        public int PuestoId { get; set; }
        public Puesto Puesto { get; set; }

        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaSalida { get; set; }

        public EstadoEmpleado Estado { get; set; } = EstadoEmpleado.Activo;

        public ICollection<HistorialLaboral> HistorialLaborales { get; set; } = new List<HistorialLaboral>();
        public ICollection<HistorialSalarial> HistorialSalariales { get; set; } = new List<HistorialSalarial>();
        public ICollection<Vacaciones> Vacaciones { get; set; } = new List<Vacaciones>();
        public ICollection<Constancia> Constancias { get; set; } = new List<Constancia>();
        public Usuario Usuario { get; set; }
    }
}
