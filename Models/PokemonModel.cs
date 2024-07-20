
using System.ComponentModel;
using Newtonsoft.Json;

namespace Pokedex.Models
{
    public class PokemonModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _id;
        private string _name;
        private Sprite _sprites;
        private Uri _imageUri;
        private List<TypeObject> _types;

        [JsonProperty("id")]
        public int Id 
        {
            get => _id; 
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        [JsonProperty("name")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        [JsonProperty("sprites")]
        public Sprite Sprites
        {
            get => _sprites;
            set
            {
                _sprites = value;
                OnPropertyChanged(nameof(Sprites));
            }
        }

        [JsonProperty("types")]
        public List<TypeObject> Types
        {
            get => _types;
            set
            {
                _types = value;
                OnPropertyChanged(nameof(Types));
            }
        }

        public class Sprite : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;
            private Uri _imageUri;

            [JsonProperty("front_default")]
            public Uri ImageUri
            {
                get => _imageUri;
                set
                {
                    _imageUri = value;
                    OnPropertyChanged(nameof(ImageUri));
                }
            }

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class TypeObject : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;
            private Type _type;

            [JsonProperty("type")]
            public Type Type
            {
                get => _type;
                set
                {
                    _type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class Type : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;
            private string _name;

            [JsonProperty("name")]
            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
