using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace rulesencyclopediaclient.Tools
{
    class InterfaceAnimation
    {
        public void animateTextBox(Control textBox, string upDown)


        {
            if (textBox is TextBox)
            {
                TextBox typedTextBox = textBox as TextBox;
            }

            if (textBox is PasswordBox)
            {
                PasswordBox typedTextBox = textBox as PasswordBox;
            }

            TimeSpan duration = TimeSpan.FromMilliseconds(1000);
            DoubleAnimation animateOpacity = null;
            if (upDown == "UP")
            {
                animateOpacity = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = duration,
                };
            } else
            {
                animateOpacity = new DoubleAnimation()
                {
                    From = 1,
                    To = 0,
                    Duration = duration,
                };
            }

                //Pulses
                /*            DoubleAnimation animateOpacity = new DoubleAnimation()
                            {
                                From = 0,
                                To = 1,
                                Duration = duration,
                                AutoReverse = true
                            };
                */
                DropShadowEffect dropShadowEffect = new DropShadowEffect();

            dropShadowEffect.BeginAnimation(DropShadowEffect.OpacityProperty,
                                            animateOpacity);

            textBox.Effect = dropShadowEffect;

        }

        public void animateButton(Button button)
        {

        }
    }
}
