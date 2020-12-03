using JetBrains.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace rulesencyclopediaclient.Model.View
{
    public class TocListView : INotifyPropertyChanged
    {
        private int _Id;
        private string _ParagraphNumber;
        private string _Headline;
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
        public string ParagraphNumber
        {
            get => _ParagraphNumber;
            set
            {
                if (value == _ParagraphNumber) return;
                _ParagraphNumber = value;
                OnPropertyChanged();
            }
        }
        public string Headline
        {
            get => _Headline;
            set
            {
                if (value == _Headline) return;
                _Headline = value;
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
