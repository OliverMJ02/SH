using Android.App;
using Android.Runtime;
using Android.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.Graphics.Drawable;
using Android.Widget;
using CurryFit.CustomRenderers;
using CurryFit.Droid.CustomRenderers;
using Android.Graphics;
using Android.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;


[assembly: ExportRenderer(typeof(CustomSlider), typeof(SliderRenderer))]

namespace CurryFit.CustomRenderers
{
    public class CustomSliderRenderer : SliderRenderer
    {
        public CustomSliderRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomSlider.ThicknessProperty.PropertyName ||
               e.PropertyName == CustomSlider.GradientColor1Property.PropertyName ||
               e.PropertyName == CustomSlider.GradientColor2Property.PropertyName ||
               e.PropertyName == CustomSlider.ThumbSizeProperty.PropertyName)
            {
                UpdateSlider();
            }
        }


        private void UpdateSlider()
        {
            if (Control != null && Element != null)
            {
                var seekBar = Control;
                var customSlider = Element as CustomSlider;

                // Set the thickness of the slider bar
                LayerDrawable layerDrawable = (LayerDrawable)seekBar.ProgressDrawable;
                GradientDrawable gradientDrawable = (GradientDrawable)layerDrawable.FindDrawableByLayerId(Android.Resource.Id.Background);
                gradientDrawable.SetStroke((int)customSlider.Thickness, customSlider.GradientColor1.ToAndroid());

                // Set the gradient color of the slider bar
                gradientDrawable.SetColors(new int[] { customSlider.GradientColor1.ToAndroid(), customSlider.GradientColor2.ToAndroid() });

                // Set the size of the thumb grabber
                var thumb = Context.GetDrawable("slider_thumb.png") as GradientDrawable;
                thumb.SetSize((int)customSlider.ThumbSize, (int)customSlider.ThumbSize);

                // Set the color of the thumb grabber
                thumb.SetColor(customSlider.ThumbColor.ToAndroid());

                // Set the thumb grabber to the slider
                seekBar.SetThumb(thumb);
            }
        }
    }
}