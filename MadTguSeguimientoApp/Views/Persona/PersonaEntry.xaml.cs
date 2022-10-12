using MadTguSeguimientoApp.Repositorios;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadTguSeguimientoApp.Views.Persona
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonaEntry : ContentPage
    {
        PersonaRepositorio respositorio = new PersonaRepositorio();
        private string fecha;
        private string estado;
        private string sexo;
        public PersonaEntry()
        {
            InitializeComponent();
        }
        private void startDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            fecha = FechaNacimiento.Date.ToString("yyyy-MM-dd");
        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            
            if (CasadoEntry.IsChecked && SolteroEntry.IsChecked)
            {
                await DisplayAlert("Advertencia", "Solo puede elegir un estado civil", "Cancelar");
            }
            if (MasculinoEntry.IsChecked && FemeninoEntry.IsChecked)
            {
                await DisplayAlert("Advertencia", "Solo puede elegir un tipo de sexo", "Cancelar");
            }
            
            if (CasadoEntry.IsChecked)
            {
                estado = "CASADO";
            }
            if (SolteroEntry.IsChecked)
            {
                estado = "SOLTERO";
            }
            if (MasculinoEntry.IsChecked)
            {
                sexo = "MASCULINO";
            }
            if (FemeninoEntry.IsChecked)
            {
                sexo = "FEMENINO";
            }
            
            string nombres = TxtNombre.Text;
            string apellidos = TxtApellidos.Text;
            string direccion = TxtDireccion.Text;
            string telefono = TxtTelefono.Text;
            string cumpleaños = fecha;
            string _sexo = sexo;
            string _estado = estado;
            if (string.IsNullOrEmpty(nombres))
            {
                await DisplayAlert("Advertencia", "Ingrese los nombres", "Cancelar");
            }
            if (string.IsNullOrEmpty(apellidos))
            {
                await DisplayAlert("Advertencia", "Ingrese los apellidos", "Cancelar");
            }
            if (string.IsNullOrEmpty(direccion))
            {
                await DisplayAlert("Advertencia", "Ingrese la dirección", "Cancelar");
            }
            if (string.IsNullOrEmpty(telefono))
            {
                await DisplayAlert("Advertencia", "Ingrese el teléfono", "Cancelar");
            }
            if (string.IsNullOrEmpty(cumpleaños))
            {
                await DisplayAlert("Advertencia", "Seleccione fecha de cumpleaños", "Cancelar");
            }
            if (string.IsNullOrEmpty(_sexo))
            {
                await DisplayAlert("Advertencia", "Seleccione el sexo", "Cancelar");
            }
            if (string.IsNullOrEmpty(_estado))
            {
                await DisplayAlert("Advertencia", "Seleccione el estado civil", "Cancelar");
            }

            PersonaModel persona = new PersonaModel();
            persona.Nombres = nombres.ToUpper();
            persona.Apellidos = apellidos.ToUpper();
            persona.Direccion = direccion.ToUpper();
            persona.Telefono = telefono;
            persona.Cumpleaños = cumpleaños.ToUpper();
            persona.Sexo = _sexo.ToUpper();
            persona.EstadoCivil = _estado.ToUpper();

            var isSaved = await respositorio.Guardar(persona);
            if (isSaved)
            {
                await DisplayAlert("Información", "Persona guardada con éxito", "Ok");
                Clear();
                await Navigation.PopModalAsync();

            }
            else
            {
                await DisplayAlert("Error", "Error al enviar datos, intente de nuevo", "Ok");
            }

        }
        public void Clear()
        {
            TxtNombre.Text = string.Empty;
            TxtApellidos.Text = string.Empty;
            TxtDireccion.Text = string.Empty;
            TxtTelefono.Text = string.Empty;
            FechaNacimiento.Date = DateTime.Now;
            MasculinoEntry.IsChecked = false;
            FemeninoEntry.IsChecked = false;
            CasadoEntry.IsChecked = false;
            SolteroEntry.IsChecked = false;

        }

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }

}