using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace rulesencyclopediaclient.Model.View
{
    class GamesListView : INotifyPropertyChanged
    {
        private List<GameView> _gamesList;
        public List<GameView> gamesList 
        {
            get
            {
                return _gamesList;
            }
            set
            {
                if (value == _gamesList) return;
                {
                    _gamesList = value;
                    OnPropertyChanged();
                }
            }      
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
