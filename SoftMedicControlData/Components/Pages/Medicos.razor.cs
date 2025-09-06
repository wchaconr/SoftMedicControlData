using SoftMedicControlData.Models;

namespace SoftMedicControlData.Components.Pages
{
    public partial class Medicos
    {
        private Medico medico = new();
        private List<Medico> listaMedicos = [];

        // Variables de estado
        private bool ventanaVisible = false;
        private bool ventanaMinimizada = false;
        private Medico? medicoEditando = null;

        private bool modoEdicion = false;

        private string filtroNombre = string.Empty;
        private string filtroDocumento = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await CargarMedicos();
        }

        private async Task CargarMedicos()
        {
            listaMedicos = await medicoService.Get_TodosMedicos();
        }

        private async Task GuardarMedico()
        {
            if (modoEdicion)
            {
                await medicoService.Update_RegistroMedico(medico);
            }
            else
            {
                await medicoService.Add_RegistroMedico(medico);
            }

            await CargarMedicos();
            LimpiarFormulario();
        }

        private async Task EliminarMedico(Medico medico)
        {
            await medicoService.Delete_RegistroMedico(medico.IdMedico);
            await CargarMedicos();
        }

        private void Cancelar()
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            medico = new Medico();
            modoEdicion = false;
        }

        private void AbrirVentanaFlotante(Medico medico)
        {
            // Clonar medico para edición
            medicoEditando = new Medico
            {
                IdMedico = medico.IdMedico,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                Telefono = medico.Telefono,
                Especialidad = medico.Especialidad,
                CorreoElectronico = medico.CorreoElectronico,
                JornadaAtencion = medico.JornadaAtencion
            };

            ventanaVisible = true;
            ventanaMinimizada = false;
        }

        private void Minimizar()
        {
            ventanaMinimizada = !ventanaMinimizada;
        }

        private void CerrarVentana()
        {
            ventanaVisible = false;
            medicoEditando = null;
        }

        private async Task GuardarCambios()
        {
            if (medicoEditando == null) return;

            try
            {
                // Buscar medico original
                var original = listaMedicos.FirstOrDefault(p =>
                    p.IdMedico == medicoEditando.IdMedico);

                if (original != null)
                {
                    // Actualizar propiedades
                    original.Nombre = medicoEditando.Nombre;
                    original.Apellido = medicoEditando.Apellido;
                    original.Telefono = medicoEditando.Telefono;
                    original.Especialidad = medicoEditando.Especialidad;
                    original.CorreoElectronico = medicoEditando.CorreoElectronico;
                    original.JornadaAtencion = medicoEditando.JornadaAtencion;

                    // Guardar en BD
                    await medicoService.Update_RegistroMedico(original);

                    // Actualizar UI
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                // Manejar error (opcional)
                Console.WriteLine($"Error al guardar: {ex.Message}");
            }
            finally
            {
                CerrarVentana();
            }
        }
        public class VentanaFlotante
        {
            public Medico medicoAEditar { get; set; } = new();
            public string EstiloPosicion { get; set; } = "left:100px; top:100px; z-index:100;";
            public bool Minimizada { get; set; }
            public int ZIndex { get; set; } = 100;
        }
    }
}