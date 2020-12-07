using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace rulesencyclopediaclient.Model.View
{
    public class TOCView : INotifyPropertyChanged
    {
        private int _Id;
        private string _Text;
        private string _Revision;

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
        public string Text
        {
            get => _Text;
            set
            {
                if (value == _Text) return;
                _Text = value;
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

