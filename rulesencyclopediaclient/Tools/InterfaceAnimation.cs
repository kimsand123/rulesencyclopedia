using rulesencyclopediaclient.Interfaces;
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
    class InterfaceAnimation : IInterfaceAnimation
    {
        public void animateTextBox(Control textBox, string upDown)


        {
            //Setting the duration of the animation in milliseconds
            TimeSpan duration = TimeSpan.FromMilliseconds(500);
            //Animates over 2 values, from and to over a timeframe
            DoubleAnimation animateOpacity;
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

            //Animates the chosen effect with the animation properties set above
            dropShadowEffect.BeginAnimation(DropShadowEffect.OpacityProperty,
                                            animateOpacity);
            //Execute the effect.
            textBox.Effect = dropShadowEffect;

        }

        public void animateButton(Button button, string upDown)
        {
            //Setting the duration of the animation in milliseconds
            TimeSpan duration = TimeSpan.FromMilliseconds(500);
            //Animates over 2 values, from and to over a timeframe
            DoubleAnimation animateOpacity;
            if (upDown == "UP")
            {
                animateOpacity = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = duration,
                };
            }
            else
            {
                animateOpacity = new DoubleAnimation()
                {
                    From = 1,
                    To = 0,
                    Duration = duration,
                };
            }


            DropShadowEffect dropShadowEffect = new DropShadowEffect();
            //Animates the chosen effect with the animation properties set above
            dropShadowEffect.BeginAnimation(DropShadowEffect.OpacityProperty,
                                animateOpacity);
            //Execute the effect.
            button.Effect = dropShadowEffect;
        }
    }
}
