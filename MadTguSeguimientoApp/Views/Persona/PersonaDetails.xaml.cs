using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadTguSeguimientoApp.Views.Persona
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonaDetails : ContentPage
    {
        public PersonaDetails(PersonaModel personaModel)
        {
            InitializeComponent();
            TxtId.Text = personaModel.Id;
            LbNombres.Text = personaModel.Nombres;
            LbApellidos.Text = personaModel.Apellidos;
            LbDireccion.Text = personaModel.Direccion;
            LbTelefono.Text = personaModel.Telefono;
            LbCumpleaños.Text = personaModel.Cumpleaños;
            LbSexo.Text = personaModel.Sexo;
            LbEstado.Text = personaModel.EstadoCivil;
        }

        private async void BtnCancelar_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}