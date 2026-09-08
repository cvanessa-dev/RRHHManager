namespace RRHHManager.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}
