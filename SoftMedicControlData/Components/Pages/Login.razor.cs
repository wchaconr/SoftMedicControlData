using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace SoftMedicControlData.Components.Pages
{
    public partial class Login
    {
        public class LoginModel
        {
            [Required(ErrorMessage = "El usuario es obligatorio")]
            public string Usuario { get; set; }

            [Required(ErrorMessage = "La contraseña es obligatoria")]
            public string Contrasena { get; set; }
        }

        private LoginModel loginModel = new();
        private string errorMensaje;

        private void IniciarSesion()
        {
            // prueba de inicio de sesión   
            if (loginModel.Usuario == "admin" && loginModel.Contrasena == "1234")
            {
                NavigationManager.NavigateTo("/RegistrarUsuario");
            }
            else
            {
                errorMensaje = "Usuario o contraseña incorrectos.";
            }
        }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
    }
}