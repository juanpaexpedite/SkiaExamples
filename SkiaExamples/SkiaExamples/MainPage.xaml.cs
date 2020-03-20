using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkiaExamples
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private bool Gradient = false;



        private void CanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            var typeface = SKTypeface.FromFamilyName("Arial");

            DrawShadow(canvas, typeface,x:12, shadowg1: "ff0000", shadowg2: "#00ff00");
        }

        private void DrawShadow(SKCanvas canvas, SKTypeface font, string text = "Xamarin",int x = 0, int y=0, int fontsize = 200, int strokewidth = 2, 
            string shadowcolor ="#AAffff00",string shadowg1 = "#ff0000", string shadowg2 = "#cccccc")
        {
            using (SKPaint measurepaint = new SKPaint())
            {
                measurepaint.Style = SKPaintStyle.Fill;
                measurepaint.IsAntialias = true;
                measurepaint.Typeface = font;
                measurepaint.TextSize = (float)fontsize;

                using (SKPath textPath = measurepaint.GetTextPath(text, (float)x, (float)y + (float)fontsize))
                {
                    using (SKPaint paint2 = new SKPaint())
                    {
                        paint2.IsAntialias = true;
                        paint2.StrokeMiter = 1000;
                        paint2.StrokeCap = SKStrokeCap.Round;
                        paint2.StrokeJoin = SKStrokeJoin.Round;
                        paint2.Style = SKPaintStyle.StrokeAndFill;
                        paint2.StrokeWidth = (float)strokewidth;


                        if (Gradient)
                        {
                            paint2.Color = FromString(shadowcolor);
                        }
                        else
                        {
                            paint2.Color = FromString(shadowg1);
                        }

                        paint2.Typeface = font;
                        paint2.TextSize = (float)fontsize;

                        SKRect textBounds = textPath.Bounds;
                        if (Gradient)
                        {
                            paint2.Shader = SKShader.CreateLinearGradient(
                                            new SKPoint(textBounds.Left + textBounds.Width * 0.5f, textBounds.Top),
                                            new SKPoint(textBounds.Left + textBounds.Width * 0.5f, textBounds.Bottom),
                                            new SKColor[] { FromString(shadowg1), FromString(shadowg2) },null,
                                            SKShaderTileMode.Clamp);
                        }

                        paint2.ImageFilter = SKImageFilter.CreateDropShadow(8,8,4,4, FromString(shadowcolor), SKDropShadowImageFilterShadowMode.DrawShadowOnly);

                        canvas.DrawPath(textPath, paint2);
                    }
                }

            }

            


        }

        private SKColor FromString(string color)
        {
            SKColor skcolor = SKColors.Empty;

            SKColor.TryParse(color, out skcolor);

            return skcolor;
        }

        private void ChangeButton_Clicked(object sender, EventArgs e)
        {
            Gradient = !Gradient;
            CanvasView.InvalidateSurface();
        }
    }
}
