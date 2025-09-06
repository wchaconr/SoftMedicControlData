using SoftMedicControlData.Models;

namespace SoftMedicControlData.Components.Pages
{
    public partial class Pacientes
    {
        private Paciente paciente = new ();
        private List<Paciente> Listapacientes = [];

        // Variables de estado
        private bool ventanaVisible = false;
        private bool ventanaMinimizada = false;
        private Paciente? pacienteEditando = null;

        private bool modoEdicion = false;

        private string filtroNombre = string.Empty;
        private string filtroDocumento = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await CargarPacientes();
        }

        private async Task CargarPacientes()
        {
            Listapacientes = await pacienteService.Get_TodosPacientes();
        }

        private async Task GuardarPaciente()
        {
            if (modoEdicion)
            {
                await pacienteService.Update_RegistroPaciente(paciente);
            }
            else
            {
                await pacienteService.Add_RegistroPaciente(paciente);
            }

            await CargarPacientes();
            LimpiarFormulario();
        }

        private async Task EliminarPaciente(Paciente paciente)
        {
            await pacienteService.Delete_RegistroPaciente(paciente.CedulaPaciente);
            await CargarPacientes();
        }

        private void Cancelar()
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            paciente = new Paciente();
            modoEdicion = false;
        }

        private async Task GuardarPacienteAsync()
        {
            paciente = new Paciente
            {
                CedulaPaciente = paciente.CedulaPaciente,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                Telefono = paciente.Telefono,
                FechaNacimiento = paciente.FechaNacimiento,
                Genero = paciente.Genero,
                Direccion = paciente.Direccion,
                CorreoElectronico = paciente.CorreoElectronico,
                Activo = paciente.Activo
            };

            if (paciente.CedulaPaciente != 0)
            {
                // Lógica para agregar un nuevo paciente
                await pacienteService.Add_RegistroPaciente(paciente);
            }
            else
            {
                // Lógica para actualizar un paciente existente
                await pacienteService.Update_RegistroPaciente(paciente);
            }
        }

        private void AbrirVentanaFlotante(Paciente paciente)
        {
            // Clonar paciente para edición
            pacienteEditando = new Paciente
            {
                CedulaPaciente = paciente.CedulaPaciente,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                Telefono = paciente.Telefono,
                FechaNacimiento = paciente.FechaNacimiento,
                Genero = paciente.Genero,
                Direccion = paciente.Direccion,
                CorreoElectronico = paciente.CorreoElectronico,
                Activo = paciente.Activo
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
            pacienteEditando = null;
        }

        private async Task GuardarCambios()
        {
            if (pacienteEditando == null) return;

            try
            {
                // Buscar paciente original
                var original = Listapacientes.FirstOrDefault(p =>
                    p.CedulaPaciente == pacienteEditando.CedulaPaciente);

                if (original != null)
                {
                    // Actualizar propiedades
                    original.Nombre = pacienteEditando.Nombre;
                    original.Apellido = pacienteEditando.Apellido;
                    original.Telefono = pacienteEditando.Telefono;
                    original.FechaNacimiento = pacienteEditando.FechaNacimiento;
                    original.Genero = pacienteEditando.Genero;
                    original.Direccion = pacienteEditando.Direccion;
                    original.CorreoElectronico = pacienteEditando.CorreoElectronico;
                    original.Activo = pacienteEditando.Activo;

                    // Guardar en BD
                    await pacienteService.Update_RegistroPaciente(original);

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
            public Paciente PacienteAEditar { get; set; } = new();
            public string EstiloPosicion { get; set; } = "left:100px; top:100px; z-index:100;";
            public bool Minimizada { get; set; }
            public int ZIndex { get; set; } = 100;
        }
    }
}