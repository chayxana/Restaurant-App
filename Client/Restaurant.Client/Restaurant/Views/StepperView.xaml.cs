using System;
using Xamarin.Forms;

namespace Restaurant.Views
{
    public partial class StepperView : ContentView
    {
        public StepperView()
        {
            InitializeComponent();
            StepLabel.Text = Value.ToString();
        }

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
            BindableProperty.Create(nameof(Value), typeof(double), typeof(StepperView), 0D, BindingMode.TwoWay, null, ValuePropertyChanged);

        private static void ValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var stepperView = bindable as StepperView;
            if (stepperView != null)
            {
                stepperView.StepLabel.Text = newValue.ToString();
            }
        }

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


        private async void PlusButton_OnClicked(object sender, EventArgs e)
        {
            if (MaxValue > Value)
            {
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
        }

        private async void MinusButton_OnClicked(object sender, EventArgs e)
        {
            if (MinValue < Value)
            {
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
}