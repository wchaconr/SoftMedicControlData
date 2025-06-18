using System.ComponentModel.DataAnnotations;

namespace SoftMedicControlData.Components.Pages
{
    public partial class Citas
    {
        public class Cita
        {
            [Required] public string Medico { get; set; }
            [Required] public string Paciente { get; set; }
            [Required] public DateTime Fecha { get; set; } = DateTime.Today;
            [Required] public TimeSpan Hora { get; set; }
            public string Motivo { get; set; }
        }

        private string vistaCalendario = "Mensual";
        private Cita cita = new();
        private List<Cita> citas = new();
        private string filtro;
        private bool mostrarPopup = false;

        private void GuardarCita()
        {
            citas.Add(new Cita
            {
                Medico = cita.Medico,
                Paciente = cita.Paciente,
                Fecha = cita.Fecha,
                Hora = cita.Hora,
                Motivo = cita.Motivo
            });

            cita = new(); // Limpiar
            mostrarPopup = true;
        }
    }
}