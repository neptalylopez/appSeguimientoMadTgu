using MadTguSeguimientoApp.Repositorios;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadTguSeguimientoApp.Views.Persona
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonaListPage : ContentPage
    {
        PersonaRepositorio respositorio = new PersonaRepositorio();
        public PersonaListPage()
        {
            InitializeComponent();            
            PersonaListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }
        protected override async void OnAppearing()
        {
            var students = await respositorio.GetAll();
            PersonaListView.ItemsSource = null;
            PersonaListView.ItemsSource = students;
            PersonaListView.IsRefreshing = false;

        }

        private void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PersonaEntry());
        }

        private async void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchValue = TxtBuscar.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var students = await respositorio.GetAllByName(searchValue);
                PersonaListView.ItemsSource = null;
                PersonaListView.ItemsSource = students;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void TxtBuscar_SearchButtonPressed(object sender, EventArgs e)
        {
            string searchValue = TxtBuscar.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var students = await respositorio.GetAllByName(searchValue);
                PersonaListView.ItemsSource = null;
                PersonaListView.ItemsSource = students;
            }
            else
            {
                OnAppearing();
            }
        }

        private void PersonaListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            //var studnet = e.Item as PersonaModel;
            //Navigation.PushModalAsync(new PersonaDetails(studnet));
            //((ListView)sender).SelectedItem = null;
        }

        private async void EditarSwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var student = await respositorio.GetById(id);
            if (student == null)
            {
                await DisplayAlert("Advertencia", "Persona no encontrada.", "Ok");
            }
            student.Id = id;
            await Navigation.PushModalAsync(new PersonaEdit(student));
        }


        private async void EliminarMenuItem_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Eliminar", "¿Deseas eliminar la persona?", "Yes", "No");
            if (response)
            {
                string id = ((MenuItem)sender).CommandParameter.ToString();
                bool isDelete = await respositorio.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Información", "Persona eliminada", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Error al eliminar persona", "Ok");
                }
            }
        }


        private async void DeleteTapp_Tapped(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Eliminar", "¿Deseas elimnar esta persona?", "Si", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await respositorio.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Información", "Persona eliminada", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Error al eliminar persona", "Ok");
                }
            }
        }

        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var student = await respositorio.GetById(id);
            if (student == null)
            {
                await DisplayAlert("Advertencia", "Error al buscar persona", "Ok");
            }
            student.Id = id;
            await Navigation.PushModalAsync(new PersonaEdit(student));
        }

        private async void VerTapp_Tapped(object sender, EventArgs e)
        {
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var student = await respositorio.GetById(id);
            if (student == null)
            {
                await DisplayAlert("Advertencia", "Persona no encontrada.", "Ok");
            }
            student.Id = id;
            await Navigation.PushModalAsync(new PersonaDetails(student));
        }
                
    }
}