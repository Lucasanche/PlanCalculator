namespace Plan_Calculator_2;

public partial class MainPage : ContentPage
{
    DateTime TotalDays { get; set; }
    DateTime ResultDays { get; set; }

    public MainPage()
    {
        InitializeComponent();
        SuscriptionPicker.Items.Add("6 Months");
        SuscriptionPicker.Items.Add("9 Months");
        SuscriptionPicker.Items.Add("1 Year");
        SuscriptionPicker.Items.Add("2 Years");
        SuscriptionPicker.SelectedIndex = 0;
        PlanPicker.Items.Add("Base");
        PlanPicker.Items.Add("Advanced");
        PlanPicker.Items.Add("Enterprise");
        PlanPicker.SelectedIndex = 0;
        TargetPlanPicker.Items.Add("Base");
        TargetPlanPicker.Items.Add("Advanced");
        TargetPlanPicker.Items.Add("Enterprise");
        TargetPlanPicker.SelectedIndex = 0;
        ActualDatePicker.Date = DateTime.Now;
        StartDatePicker.Date = DateTime.Now;
    }
    private void OnCalculateClicked(object sender, EventArgs e)
    {
        DateTime endSuscriptionDate = StartDatePicker.Date.AddMonths(CalculateSuscriptionTime(SuscriptionPicker.SelectedIndex));
        double totalDays = (endSuscriptionDate - StartDatePicker.Date).TotalDays;
        double daysUntilEnd = (endSuscriptionDate - ActualDatePicker.Date).TotalDays;
        double percentageOfDaysLeft = daysUntilEnd / totalDays;
        double priceAlreadyPaid = CalculatePrice(PlanPicker.SelectedIndex, SuscriptionPicker.SelectedIndex);
        double priceToPay = (CalculatePrice(TargetPlanPicker.SelectedIndex, SuscriptionPicker.SelectedIndex) - priceAlreadyPaid) * percentageOfDaysLeft * ActualUserNumberStepper.Value;
        double pricePerUserAdded = CalculatePrice(TargetPlanPicker.SelectedIndex, SuscriptionPicker.SelectedIndex) * percentageOfDaysLeft;
        priceToPay = priceToPay + (pricePerUserAdded * Convert.ToInt32(UserNumberLabel.Text));
        if (PlanPicker.SelectedIndex >= 0 && TargetPlanPicker.SelectedIndex >= 0)
        {
            DisplayAlert("Result:", "$" + priceToPay.ToString("0.00"), "Ok");
        }
        else
        {
            DisplayAlert("Result:", "Please complete the fields", "Ok");
        }
    }

    private void UserNumberStepper_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        UserNumberLabel.Text = UserNumberStepper.Value.ToString();
    }
    private int CalculateSuscriptionTime(int suscriptionTime)
    {
        if (suscriptionTime < 3)
        {
            suscriptionTime = 6 + (suscriptionTime * 3);
        }
        else
        {
            suscriptionTime = 24;
        }
        return suscriptionTime;
    }
    private double CalculatePrice(int plan, int period)
    {
        double price = 0;
        if (plan == 0)
        {
            price = 15 * (CalculateSuscriptionTime(period) - CalculateMonthlyDiscount(period));
        }
        else if (plan == 1)
        {
            price = 25 * (CalculateSuscriptionTime(period) - CalculateMonthlyDiscount(period));
        }
        else if (plan == 2)
        {
            price = 45 * (CalculateSuscriptionTime(period) - CalculateMonthlyDiscount(period));
        }
        return price;
    }
    private int CalculateMonthlyDiscount(int period)
    {
        if (period == 3)
        {
            period = 6;
        }
        return period;
    }

    private void ActualDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (ActualDatePicker.Date < StartDatePicker.Date)
        {
            DateValidationText.IsVisible = true;
            CalculateButton.IsEnabled = false;
        }
        else
        {
            DateValidationText.IsVisible = false;
            CalculateButton.IsEnabled = true;
        }
    }
    private void ActualUserNumberStepper_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        ActualUserNumberLabel.Text = ActualUserNumberStepper.Value.ToString();
    }
}

