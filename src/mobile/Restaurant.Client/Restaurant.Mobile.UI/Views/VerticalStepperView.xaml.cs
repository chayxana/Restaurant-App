using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Mobile.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerticalStepperView : ContentView
    {
        public static readonly BindableProperty MaxValueProperty =
            BindableProperty.Create(nameof(MaxValue), typeof(int), typeof(StepperView), int.MaxValue);

        public static readonly BindableProperty MinValueProperty =
            BindableProperty.Create(nameof(MinValue), typeof(int), typeof(StepperView), int.MinValue);

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(double), typeof(StepperView), 0D, BindingMode.TwoWay);

        public static readonly BindableProperty StepProperty =
            BindableProperty.Create(nameof(Step), typeof(double), typeof(StepperView), 1D);

        public VerticalStepperView()
        {
            InitializeComponent();
        }

        public int MaxValue
        {
            get => (int) GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public int MinValue
        {
            get => (int) GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        public double Value
        {
            get => (double) GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public double Step
        {
            get => (double) GetValue(StepProperty);
            set => SetValue(StepProperty, value);
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