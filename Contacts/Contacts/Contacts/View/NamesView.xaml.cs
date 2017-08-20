using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Contacts.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NamesView : ContentView
    {
        const float TextOffset = 1.25f;

        public NamesView()
        {
            InitializeComponent();
        }

        void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;

            var canvas = surface.Canvas;

            canvas.Clear();

            var circleFill = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = GetColorForText()
            };

            canvas.DrawCircle((float)Height, (float)Height, (float)Height, circleFill);

            var textPaint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColors.White,
                TextSize = (float)Height / TextOffset,
                TextAlign = SKTextAlign.Center,
                Typeface = SKTypeface.FromFamilyName(FontFamily)
            };

            canvas.DrawText(Text, (float)Height, (float)Height * TextOffset, textPaint);
        }

        public SKColor GetColorForText()
        {
            return SKColors.Orange;
        }

        #region Bindable properties

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(NamesView), string.Empty,
                                    BindingMode.OneWay, null, (bindable, oldValue, newValue) =>
                                    {
                                        var view = (bindable as NamesView);
                                        view.ForceLayout();
                                    });

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create("FontFamily", typeof(string), typeof(NamesView), string.Empty,
                                    BindingMode.OneWay, null, (bindable, oldValue, newValue) =>
                                    {
                                        var view = (bindable as NamesView);
                                        view.ForceLayout();
                                    });

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        #endregion
    }
}