using Newtonsoft.Json;
using Pokedex.Models;
using Pokedex.Utils;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ColorConverter = System.Windows.Media.ColorConverter;

namespace Pokedex.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        PokemonTypesModel pokemonTypesModel = new();
        int displayPage = 1;

        public MainWindow()
        {
            InitializeComponent();


            GeneratePokemon();


        }



        private async void GeneratePokemon(int currentPage = 1)
        {
            PokemonTypesModel typesDePokemons = await GetPokemonTypes();
            Previous.IsEnabled = false;
            Next.IsEnabled = false;

            for (int i = currentPage * 14 - 13; i <= 14 * currentPage; i++)
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

                    Border border = new();
                    border.CornerRadius = new CornerRadius(10);
                    border.Background = GetColorByType(pokemonModel.Types[0].Type.Name);
                    border.Margin = new Thickness(10);

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
                        Text = pokemonModel.Name,
                        Margin = new Thickness(0, 0, 0, 10),
                        FontWeight = FontWeights.Bold,
                    };
                    pokemonCard.Background = new SolidColorBrush(Colors.Transparent);


                    pokemonCard.Orientation = Orientation.Vertical;
                    pokemonCard.Children.Add(pokemonImage);
                    pokemonCard.Children.Add((TextBlock)pokemonName);
                    border.Child = pokemonCard;
                    pokemonContainer.Children.Add(border);


                }
                catch (Exception e)
                {
                    Debug.WriteLine("Echec de la requête : ", e.Message);
                }

            }



            Previous.IsEnabled = true;
            Next.IsEnabled = true;

        }


        private SolidColorBrush GetColorByType(string type)
        {

            string[] types = [
            "fire", "grass", "electric", "water", "ground", "rock", "fairy", "poison", "bug", "dragon", "psychic", "flying", "fighting", "normal"
            ];

            string[] colors = [
            "#FDDFDF", "#DEFDE0", "#FCF7DE", "#DEF3FD", "#f4e7da", "#d5d5d4", "#fceaff", "#98d7a5", "#f8d5a3", "#97b3e6", "#eaeda1", "#F5F5F5", "#E6E0D4", "#F5F5F5"
            ];

            for (int i = 0; i < types.Length; i++)
            {
                if (types[i].Equals(type))
                {
                    return new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString(colors[i]));
                }
            }

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

                pokemonTypesModel = JsonConvert.DeserializeObject<PokemonTypesModel>(jsonResponse);
                //    Debug.WriteLine("Converted Types : ", pokemonTypesModel.Results[1].Name);

            }
            catch (Exception e)
            {
                Debug.WriteLine("Echec de la requête : ", e.Message);
            }


            return pokemonTypesModel;


        }
        // Drag window
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
                this.DragMove();
        }

        // Minimize window
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        // Close window
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Loaded(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button)
                UiHelper.AnimateButton(button);
        }

        private void NextPageClickHandler(object sender, RoutedEventArgs e)
        {
            displayPage++;
            if (displayPage > 1)
            {
                Previous.Visibility = Visibility.Visible;
            }
            pokemonContainer.Children.Clear();
            GeneratePokemon(displayPage);
        }

        private void PreviousPageClickHandler(object sender, RoutedEventArgs e)
        {
            displayPage--;
            if (displayPage == 1)
            {
                ((Button)sender).Visibility = Visibility.Hidden;
            }
            pokemonContainer.Children.Clear();
            GeneratePokemon(displayPage);
        }
    }
}