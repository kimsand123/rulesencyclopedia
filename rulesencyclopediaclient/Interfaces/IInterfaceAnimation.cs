using System.Windows.Controls;

namespace rulesencyclopediaclient.Interfaces
{
    interface IInterfaceAnimation
    {
        void animateTextBox(Control textBox, string upDown);
        void animateButton(Button button, string upDown);

    }
}
