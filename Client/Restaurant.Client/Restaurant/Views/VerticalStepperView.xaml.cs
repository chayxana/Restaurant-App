using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VerticalStepperView : ContentView
	{
		public static readonly BindableProperty MaxValueProperty =
			BindableProperty.Create(nameof(MaxValue), typeof(int), typeof(StepperView), int.MaxValue);

		public int MaxValue
		{
			get { return (int)GetValue(MaxValueProperty); }
			set { SetValue(MaxValueProperty, value); }
		}

		public static readonly BindableProperty MinValueProperty =
			BindableProperty.Create(nameof(MinValue), typeof(int), typeof(StepperView), int.MinValue);

		public int MinValue
		{
			get { return (int)GetValue(MinValueProperty); }
			set { SetValue(MinValueProperty, value); }
		}

		public static readonly BindableProperty ValueProperty =
			BindableProperty.Create(nameof(Value), typeof(double), typeof(StepperView), 0D, BindingMode.TwoWay);

		public double Value
		{
			get { return (double)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		public static readonly BindableProperty StepProperty =
			BindableProperty.Create(nameof(Step), typeof(double), typeof(StepperView), 1D);

		public double Step
		{
			get { return (double)GetValue(StepProperty); }
			set { SetValue(StepProperty, value); }
		}

		public VerticalStepperView()
		{
			InitializeComponent();
		}

		private void ButtonDown_OnClicked(object sender, EventArgs e)
		{
			if (MinValue < Value)
				Value -= Step;
		}

		private void ButtonUp_OnClicked(object sender, EventArgs e)
		{
			if (MaxValue > Value)
				Value += Step;
		}
	}
}