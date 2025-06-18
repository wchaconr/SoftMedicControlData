using Microsoft.AspNetCore.Components.Forms;

namespace SoftMedicControlData.Components.Pages
{
    public partial class HistoriaClinica
    {
        public class Historia_Clinica
        {
            public string Paciente { get; set; }
            public int CitaID { get; set; }
            public DateTime Fecha { get; set; } = DateTime.Now;
            public string Medico { get; set; }
            public string Notas { get; set; }
            public List<IBrowserFile> Archivos { get; set; } = new();
        }

        public class Cita
        {
            public int Id { get; set; }
            public string Medico { get; set; }
            public DateTime Fecha { get; set; }
        }

        private List<string> pacientes = new() { "Juan Pérez", "Ana Gómez", "Carlos Torres" };
        private List<Cita> citas = new()
    {
        new Cita { Id = 1, Medico = "Dra. Ramírez", Fecha = DateTime.Today },
        new Cita { Id = 2, Medico = "Dr. Díaz", Fecha = DateTime.Today.AddDays(1) }
    };

        private Historia_Clinica historia = new();
        private List<Historia_Clinica> historias = new();
        private List<IBrowserFile> archivosAdjuntos = new();
        private string pacienteFiltro;

        private void HandleFileUpload(InputFileChangeEventArgs e)
        {
            archivosAdjuntos = e.GetMultipleFiles().ToList();
            historia.Archivos = archivosAdjuntos;
        }

        private void GuardarHistoria()
        {
            var cita = citas.FirstOrDefault(c => c.Id == historia.CitaID);
            if (cita != null)
            {
                historia.Medico = cita.Medico;
            }

            historias.Add(new Historia_Clinica
            {
                Paciente = historia.Paciente,
                CitaID = historia.CitaID,
                Fecha = DateTime.Now,
                Medico = historia.Medico,
                Notas = historia.Notas,
                Archivos = new List<IBrowserFile>(historia.Archivos)
            });

            historia = new();
            archivosAdjuntos.Clear();
        }
    }
}