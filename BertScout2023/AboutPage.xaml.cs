using BertScout2023.Databases;

namespace BertScout2023;

public partial class AboutPage : ContentPage
{
    private readonly LocalDatabase db = new();
    
	public AboutPage()
	{
		InitializeComponent();
		LabelAboutDatabase.Text = $"Database path: {db.DatabaseDirPath}";
	}
}