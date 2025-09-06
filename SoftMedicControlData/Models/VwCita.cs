namespace SoftMedicControlData.Models
{
    public partial class VwCita
    {
        public int IdCita { get; set; }

        public DateOnly Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        public string Motivo { get; set; } = null!;

        public string? Estado { get; set; }

        public int CedulaPaciente { get; set; }

        public string NombrePaciente { get; set; } = null!;

        public int IdMedico { get; set; }

        public string NombreMedico { get; set; } = null!;

        public int IdServicio { get; set; }

        public string NombreServicio { get; set; } = null!;

        public bool Activo { get; set; }
    }
}
