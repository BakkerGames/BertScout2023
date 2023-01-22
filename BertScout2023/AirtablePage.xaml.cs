namespace BertScout2023;

public partial class AirtablePage : ContentPage
{
    public AirtablePage()
    {
        InitializeComponent();
    }

    private void AirtableSend_Clicked(object sender, EventArgs e)
    {
        AirtableResults.Text = "";
        for (int i = 0; i < 100; i++)
        {
            if (AirtableResults.Text.Length > 0)
                AirtableResults.Text += "\r\n";
            AirtableResults.Text += $"Line {i} - Message";
        }
    }

    private void VerticalStackLayout_SizeChanged(object sender, EventArgs e)
    {
        ScrollResults.HeightRequest = cp.Height - ScrollResults.Y;
    }
}