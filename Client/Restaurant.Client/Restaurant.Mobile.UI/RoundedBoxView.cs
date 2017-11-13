using Xamarin.Forms;

// ReSharper disable once CheckNamespace
namespace RoundedBoxView.Forms.Plugin.Abstractions
{
    public class RoundedBoxView : BoxView
    {
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create("CornerRadius", typeof (double), typeof (RoundedBoxView), default(double));

        public double CornerRadius
        {
            get { return (double) GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

		/// <summary>
		/// Thickness property of border
		/// </summary>
		public static readonly BindableProperty BorderThicknessProperty =
			BindableProperty.Create<RoundedBoxView, int>(
				p => p.BorderThickness, 0);

		/// <summary>
		/// Border thickness of circle image
		/// </summary>
		public int BorderThickness
		{
			get { return (int)GetValue(BorderThicknessProperty); }
			set { SetValue(BorderThicknessProperty, value); }
		}

		/// <summary>
		/// Color property of border
		/// </summary>
		public static readonly BindableProperty BorderColorProperty =
			BindableProperty.Create<RoundedBoxView, Color>(
				p => p.BorderColor, Color.White);

		/// <summary>
		/// Border Color of circle image
		/// </summary>
		public Color BorderColor
		{
			get { return (Color)GetValue(BorderColorProperty); }
			set { SetValue(BorderColorProperty, value); }
		}
    }
}
