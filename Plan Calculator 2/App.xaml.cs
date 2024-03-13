namespace Plan_Calculator_2;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new AppShell();
	}
    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);
        //TODO: Uncoment this to set de Window size
        const int minWidth = 350;
        const int maxWidth = 500;

        window.Width = minWidth;
        window.MaximumWidth = maxWidth;
        window.MinimumWidth = minWidth;
        return window;
    }
}
