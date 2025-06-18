using System.ComponentModel.DataAnnotations;

namespace SoftMedicControlData.Components.Pages
{
    public partial class Medicos
    {
        public class Medico
        {
            [Required] public string Nombre { get; set; }
            [Required] public string Especialidad { get; set; }
            [Required] public string Email { get; set; }
            public string Telefono { get; set; }
        }

        public class Cita
        {
            public string Medico { get; set; }
            public string Paciente { get; set; }
            public DateTime Fecha { get; set; }
            public TimeSpan Hora { get; set; }
            public string Motivo { get; set; }
        }

        public class Horario
        {
            public string Dia { get; set; }
            public TimeSpan Inicio { get; set; }
            public TimeSpan Fin { get; set; }
        }

        private Medico medico = new();
        private List<Medico> medicos = new();
        private List<Cita> citas = new()
    {
        new Cita { Medico = "Dra. Ana Pérez", Paciente = "Carlos Gómez", Fecha = DateTime.Today, Hora = new TimeSpan(10, 30, 0), Motivo = "Control general" }
    };

        private string medicoSeleccionado;
        private Horario nuevoHorario = new();
        private List<Horario> horariosDisponibles = new();

        private void GuardarMedico()
        {
            if (!medicos.Any(m => m.Nombre == medico.Nombre))
            {
                medicos.Add(medico);
            }
            medico = new(); // Reset
        }

        private void AgregarHorario()
        {
            horariosDisponibles.Add(new Horario
            {
                Dia = nuevoHorario.Dia,
                Inicio = nuevoHorario.Inicio,
                Fin = nuevoHorario.Fin
            });
            nuevoHorario = new();
        }
    }
}