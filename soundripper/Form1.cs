using YoutubeExplode; // used dotnet add package YoutubeExplode --version 6.3.13

namespace soundripper
{
    public partial class frmsoundripper : Form
    {
        string link;

        public frmsoundripper()
        {
            InitializeComponent();
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (txtVideoLink != null) // check that a value is present in textbox
            {
                // add check for if youtube link is valid

                link = txtVideoLink.Text; // sets link equal to text in textbox
                var youtube = new YoutubeClient();
                var video = await youtube.Videos.GetAsync(link); // gets youtube video
                
                // convert video, maybe as a method
                // delete video and save mp3
            }
        }
    }
}
