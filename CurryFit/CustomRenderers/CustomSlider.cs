using Xamarin.Forms;

namespace CurryFit.CustomRenderers
{
    public class CustomSlider : Slider
    {
        public static readonly BindableProperty GradientColor1Property = BindableProperty.Create(nameof(GradientColor1), typeof(Color), typeof(CustomSlider), Color.Default);

        public Color GradientColor1
        {
            get { return (Color)GetValue(GradientColor1Property); }
            set { SetValue(GradientColor1Property, value); }
        }

        public static readonly BindableProperty GradientColor2Property = BindableProperty.Create(nameof(GradientColor2), typeof(Color), typeof(CustomSlider), Color.Default);

        public Color GradientColor2
        {
            get { return (Color)GetValue(GradientColor2Property); }
            set { SetValue(GradientColor2Property, value); }
        }

        public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(double), typeof(CustomSlider), 10.0);

        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        public static readonly BindableProperty ThumbSizeProperty = BindableProperty.Create(nameof(ThumbSize), typeof(double), typeof(CustomSlider), 30.0);

        public double ThumbSize
        {
            get { return (double)GetValue(ThumbSizeProperty); }
            set { SetValue(ThumbSizeProperty, value); }
        }

        public static readonly BindableProperty ThumbColorProperty = BindableProperty.Create(nameof(ThumbColor), typeof(Color), typeof(CustomSlider), Color.Default);

        public Color ThumbColor
        {
            get { return (Color)GetValue(ThumbColorProperty); }
            set { SetValue(ThumbColorProperty, value); }
        }
    }
}