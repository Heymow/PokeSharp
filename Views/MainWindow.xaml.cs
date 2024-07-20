using Newtonsoft.Json;
using Pokedex.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pokedex.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();



            GeneratePokemon();

        }



        private async void GeneratePokemon()
        {
            await GetPokemonTypes();


            for (int i = 1; i <= 10; i++)
            {


                HttpClient client = new()
                {
                    BaseAddress = new Uri(@$"https://pokeapi.co/api/v2/pokemon/{i}")
                };
                try
                {
                    string jsonResponse = await client.GetStringAsync("");
                    PokemonModel pokemonModel = JsonConvert.DeserializeObject<PokemonModel>(jsonResponse);
                    //  Debug.WriteLine("Converted Pokemon : ", pokemonModel.Name);


                    StackPanel pokemonCard = new();
                    Image pokemonImage = new();
                    BitmapImage image = new();
                    image.BeginInit();
                    image.UriSource = pokemonModel.Sprites.ImageUri;
                    image.EndInit();
                    pokemonImage.Source = image;
                    pokemonImage.Height = 80;
                    TextBlock pokemonName = new()
                    {
                        TextAlignment = TextAlignment.Center,
                        Text = pokemonModel.Name
                    };
                    pokemonCard.Background = GetColorByType(pokemonModel.Types[0].Type.Name);

                    pokemonCard.Orientation = Orientation.Vertical;
                    pokemonCard.Children.Add(pokemonImage);
                    pokemonCard.Children.Add((TextBlock)pokemonName);
                    pokemonContainer.Children.Add(pokemonCard);


                }
                catch (Exception e)
                {
                    Debug.WriteLine("Echec de la requête : ", e.Message);
                }

            }





        }


        private SolidColorBrush GetColorByType(string type)
        {
            return new SolidColorBrush(Colors.Blue);
        }




        private async Task<PokemonTypesModel> GetPokemonTypes()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri(@"https://pokeapi.co/api/v2/type")
            };
            try
            {
                string jsonResponse = await client.GetStringAsync("");

                PokemonTypesModel pokemonTypesModel = JsonConvert.DeserializeObject<PokemonTypesModel>(jsonResponse);
                //    Debug.WriteLine("Converted Types : ", pokemonTypesModel.Results[1].Name);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Echec de la requête : ", e.Message);
            }


            return new PokemonTypesModel();

        }

    }
}