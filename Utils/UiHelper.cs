using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace Pokedex.Utils
{
    public class UiHelper
    {
        public static void AnimateButton(Button btn, string? btnType = "normal")
        {
            // Accède aux ressources de couleur du thème actuel
            var normalBtnBrushKey = btnType == "play" ? "playBtnActive" : "normalBtnActive";
            var normalBtnBrushActiveKey = btnType == "play" ? "playBtn" : "normalBtn";


            if (Application.Current.Resources[normalBtnBrushKey] is SolidColorBrush normalBtnBrush && Application.Current.Resources[normalBtnBrushActiveKey] is SolidColorBrush normalBtnBrushActive)
            {
                // Crée une nouvelle animation avec les couleurs du thème
                ColorAnimation colorAnimationEnter = new()
                {
                    To = normalBtnBrush.Color,
                    Duration = TimeSpan.FromSeconds(0.15)
                };

                ColorAnimation colorAnimationLeave = new()
                {
                    To = normalBtnBrushActive.Color,
                    Duration = TimeSpan.FromSeconds(0.15)
                };

                // Crée les storyboards
                Storyboard storyboardEnter = new();
                storyboardEnter.Children.Add(colorAnimationEnter);
                Storyboard.SetTarget(colorAnimationEnter, btn);
                Storyboard.SetTargetProperty(colorAnimationEnter, new PropertyPath("Background.Color"));

                Storyboard storyboardLeave = new();
                storyboardLeave.Children.Add(colorAnimationLeave);
                Storyboard.SetTarget(colorAnimationLeave, btn);
                Storyboard.SetTargetProperty(colorAnimationLeave, new PropertyPath("Background.Color"));

                // Attache les storyboards aux événements de la souris
                try
                {
                    btn.MouseEnter += (s, args) => storyboardEnter.Begin();
                    btn.MouseLeave += (s, args) => storyboardLeave.Begin();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"UIHelper => {ex}");
                }
            }

        }

    }
}
