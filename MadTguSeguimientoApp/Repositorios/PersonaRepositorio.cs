using Firebase.Database;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadTguSeguimientoApp.Repositorios
{
    public class PersonaRepositorio
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://madtguseguimientofirebase-default-rtdb.firebaseio.com/");

        public async Task<bool> Guardar(PersonaModel personaModel)
        {
            var data = await firebaseClient.Child(nameof(PersonaModel)).PostAsync
                (JsonConvert.SerializeObject(personaModel));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }
        public async Task<List<PersonaModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(PersonaModel)).OnceAsync<PersonaModel>()).Select(item => new PersonaModel
            {
                Nombres = item.Object.Nombres,
                Apellidos = item.Object.Apellidos,
                Direccion = item.Object.Direccion,
                Telefono = item.Object.Telefono,
                Cumpleaños = item.Object.Cumpleaños,
                Sexo = item.Object.Sexo,
                Id = item.Key
            }).ToList();
        }

        public async Task<List<PersonaModel>> GetAllByName(string name)
        {
            return (await firebaseClient.Child(nameof(PersonaModel)).OnceAsync<PersonaModel>()).Select(item => new PersonaModel
            {
                Nombres = item.Object.Nombres,
                Apellidos = item.Object.Apellidos,
                Direccion = item.Object.Direccion,
                Telefono = item.Object.Telefono,
                Cumpleaños = item.Object.Cumpleaños,
                Sexo = item.Object.Sexo,
                Id = item.Key
            }).Where(c => c.Nombres.ToLower().Contains(name.ToLower())).ToList();
        }
        public async Task<List<PersonaModel>> GetAllByLastName(string apell)
        {
            return (await firebaseClient.Child(nameof(PersonaModel)).OnceAsync<PersonaModel>()).Select(item => new PersonaModel
            {
                Nombres = item.Object.Nombres,
                Apellidos = item.Object.Apellidos,
                Direccion = item.Object.Direccion,
                Telefono = item.Object.Telefono,
                Cumpleaños = item.Object.Cumpleaños,
                Sexo = item.Object.Sexo,
                Id = item.Key
            }).Where(c => c.Apellidos.ToLower().Contains(apell.ToLower())).ToList();
        }

        public async Task<PersonaModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(PersonaModel) + "/" + id).OnceSingleAsync<PersonaModel>());
        }

        public async Task<bool> Update(PersonaModel student)
        {
            await firebaseClient.Child(nameof(PersonaModel) + "/" + student.Id).PutAsync(JsonConvert.SerializeObject(student));
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(PersonaModel) + "/" + id).DeleteAsync();
            return true;
        }

        //public async Task<string> Upload(Stream img, string fileName)
        //{
        //    //var image = await firebaseStorage.Child("Images").Child(fileName).PutAsync(img);
        //    //return image;
        //    return "";
        //}


    }
}
