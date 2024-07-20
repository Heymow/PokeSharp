using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class PokemonTypesModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _count;
        private List<Type> _results;


        [JsonProperty("count")]
        public int Count {
            get => _count;
            set
            {
                _count = (int)value;
                OnPropertyChanged(nameof(Count));
            }
        }


        [JsonProperty("results")]
        public List<Type> Results {
            get => _results;
            set 
            {
                _results = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
    }


}
