using SoftMedicControlData.Models;

namespace SoftMedicControlData.Components.Pages
{
    public partial class HistoriaClinica
    {
        private HistorialMedico historiaClinica = new();
        private HistorialMedico? historiaClinicaEncontrada = null;
        private HistorialMedico? historiaClinicaEditada = null;
        private Paciente pacienteEncontrado = new();
        private string nombreCompletoPaciente = "";

        // Variables de estado
        private bool ventanaNuevaHistoriaVisible = false;
        private bool ventanaConsultaHistoriaVisible = false;
        private bool modoEdicion = false;
        private bool mostrarPopup = false;
        private bool historiaRegistradaOK = false;
        private bool historiaEditadaOK = false;

        // Filtros
        private int? _filtroDocumentoPaciente;
        public int? filtroDocumentoPaciente
        {
            get => _filtroDocumentoPaciente;
            set
            {
                if (_filtroDocumentoPaciente != value)
                {
                    _filtroDocumentoPaciente = value;
                    _ = Get_HistorialMedico();
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            //await CargarMedicos();
        }

        private async Task Get_HistorialMedico()
        {
            if (filtroDocumentoPaciente == null)
                return;
            else
            {
                historiaClinicaEncontrada = await historiaClinicaService.Get_HistorialMedicoPorCedulaPaciente(Convert.ToInt32(filtroDocumentoPaciente));
                if (historiaClinicaEncontrada != null)
                {
                    pacienteEncontrado = await pacienteService.Get_PacientePorCedula(historiaClinicaEncontrada.CedulaPaciente);
                    nombreCompletoPaciente = $"{pacienteEncontrado.Nombre} {pacienteEncontrado.Apellido}";
                    StateHasChanged();
                }
            } 
        }

        private async Task GuardarMedico()
        {
            await historiaClinicaService.Add_RegistroHistorialMedico(historiaClinica);

            //await CargarCitas();
            mostrarPopup = true;
            historiaEditadaOK = false;
            historiaRegistradaOK = true;

            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            historiaClinica = new HistorialMedico();
            // Actualizar UI
            StateHasChanged();
        }

        private void CerrarEdit()
        {
            modoEdicion = false;
        }

        private void editarHistoria()
        {
            modoEdicion = true;
        }

        private async Task GuardarCambios()
        {
            try
            {
                if (historiaClinicaEncontrada != null)
                {
                    // Guardar en BD
                    await historiaClinicaService.Update_RegistroHistorialMedico(historiaClinicaEncontrada);

                    //await CargarCitas();
                    mostrarPopup = true;
                    historiaEditadaOK = true;
                    historiaRegistradaOK = false;

                    LimpiarFormulario();
                }
                else return;

            }
            catch (Exception ex)
            {
                // Manejar error (opcional)
                Console.WriteLine($"Error al guardar: {ex.Message}");
            }
        }

        private void AbrirVentanaNuevaHistoria()
        {
            ventanaNuevaHistoriaVisible = true;
            ventanaConsultaHistoriaVisible = false;
            modoEdicion = false;
        }

        private void AbrirVentanaConsultarHistoria()
        {
            ventanaConsultaHistoriaVisible = true;
            ventanaNuevaHistoriaVisible = false;
            modoEdicion = false;
        }
    }
}