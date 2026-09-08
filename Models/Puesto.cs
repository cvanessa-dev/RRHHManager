namespace RRHHManager.Models
{
    public class Puesto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int DepartamentoId { get; set; }
        public Departamento? Departamento { get; set; }
        public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}
