namespace SoftMedicControlData.Models
{
    public partial class Especialidades
    {
        public int IdEspecialidad { get; set; }

        public string NombreEspecialidad { get; set; } = null!;

        public virtual ICollection<Medico> Medico { get; set; } = new List<Medico>();
    }
}
