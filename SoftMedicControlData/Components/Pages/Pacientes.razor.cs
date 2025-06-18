namespace SoftMedicControlData.Components.Pages
{
    public partial class Pacientes
    {
        public class Paciente
        {
            public string Nombre { get; set; }
            public string Documento { get; set; }
            public string Telefono { get; set; }
            public string EPS { get; set; }
        }

        private Paciente paciente = new Paciente();
        private List<Paciente> listaPacientes = new List<Paciente>();

        private string filtroNombre = string.Empty;
        private string filtroDocumento = string.Empty;

        private void GuardarPaciente()
        {
            listaPacientes.Add(new Paciente
            {
                Nombre = paciente.Nombre,
                Documento = paciente.Documento,
                Telefono = paciente.Telefono,
                EPS = paciente.EPS
            });

            paciente = new Paciente();
        }
    }
}