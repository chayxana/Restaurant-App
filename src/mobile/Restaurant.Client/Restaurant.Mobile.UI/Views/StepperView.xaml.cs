using System;
using Xamarin.Forms;

namespace Restaurant.Mobile.UI.Views
{
    public partial class StepperView : ContentView
    {
        public static readonly BindableProperty MaxValueProperty =
            BindableProperty.Create(nameof(MaxValue), typeof(int), typeof(StepperView), int.MaxValue);

        public static readonly BindableProperty MinValueProperty =
            BindableProperty.Create(nameof(MinValue), typeof(int), typeof(StepperView), int.MinValue);

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(double), typeof(StepperView), 0D, BindingMode.TwoWay, null,
                ValuePropertyChanged);

        public static readonly BindableProperty StepProperty =
            BindableProperty.Create(nameof(Step), typeof(double), typeof(StepperView), 1D);

        public StepperView()
        {
            InitializeComponent();
            StepLabel.Text = Value.ToString();
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

        private static void ValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is StepperView stepperView)
                stepperView.StepLabel.Text = newValue.ToString();
        }


        private async void PlusButton_OnClicked(object sender, EventArgs e)
        {
            if (MaxValue > Value)
                try
                {
                    await StepLabel.ScaleTo(1.3, 100);
                    Value += Step;
                    await StepLabel.ScaleTo(1, 100);
                }
                catch (Exception)
                {
// ignored
                }
        }

        private async void MinusButton_OnClicked(object sender, EventArgs e)
        {
            if (MinValue < Value)
                try
                {
                    await StepLabel.ScaleTo(1.3, 100);
                    Value -= Step;
                    await StepLabel.ScaleTo(1, 100);
                }
                catch (Exception)
                {
// ignored
                }
        }
    }
}