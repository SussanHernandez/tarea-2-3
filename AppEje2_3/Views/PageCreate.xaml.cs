using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEje2_3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCreate : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile photo = null;
        public PageCreate()
        {
            InitializeComponent();
        }

        public String Getimage64()
        {
            if (photo != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] fotobyte = memory.ToArray();

                    String Base64 = Convert.ToBase64String(fotobyte);

                    return Base64;
                }
            }

            return null;
        }

        public byte[] GetimageBytes()
        {
            if (photo != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] fotobyte = memory.ToArray();

                    return fotobyte;
                }

            }

            return null;
        }

        private async void btnfoto_Clicked(object sender, EventArgs e)
        {
            photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MYAPP",
                Name = "Foto.jpg",
                SaveToAlbum = true
            });

            if (photo != null) 
            {
                Imagen.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            
            }
        }

        private async void btnguardar_Clicked(object sender, EventArgs e)
        {
            var foto = new Models.Fotografia
            {
                Descripcion = descripcion.Text,                               
                Imagen = GetimageBytes()
            };

            if (await App.Instancia.AddEmple(foto) > 0)
            {
                await DisplayAlert("Aviso", "Fotografia ingreso con exito", "OK");
            }
            else
                await DisplayAlert("Aviso", "A ocurrido un error", "OK");
        }

        private async void btnlista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.PageList());
        }
    }
}