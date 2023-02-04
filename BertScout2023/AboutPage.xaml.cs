namespace BertScout2023;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
		LabelAboutDatabase.Text = $"Database path: {FileSystem.AppDataDirectory}";
	}
}