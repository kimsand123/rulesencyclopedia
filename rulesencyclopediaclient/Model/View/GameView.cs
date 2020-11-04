using JetBrains.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace rulesencyclopediaclient.Model.View
{
    class GameView : INotifyPropertyChanged
    {
        public int _Id;
        public string _Name;
        public string _Company;
        public string _Revision;

        public int Id
        {
            get => _Id;
            set
            {
                if (value == _Id) return;
                _Id = value;
                OnPropertyChanged();
            }
        }
        
        public string Name
        {
            get => _Name;
            set
            {
                if (value == _Name) return;
                _Name = value;
                OnPropertyChanged();
            }
        }
        public string Company
        {
            get => _Company;
            set
            {
                if (value == _Company) return;
                _Company = value;
                OnPropertyChanged();
            }
        }
        public string Revision
        {
            get => _Revision;
            set
            {
                if (value == _Revision) return;
                _Revision = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
