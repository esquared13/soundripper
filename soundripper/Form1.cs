using System.Net;
using System;
using System.IO;
using System.Windows.Forms;
using MediaToolkit; // used nuget
using MediaToolkit.Model; // used nuget
using VideoLibrary; // used nuget

namespace soundripper
{
    public partial class frmsoundripper : Form
    {
        public frmsoundripper()
        {
            InitializeComponent();
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtVideoLink.Text)) // check that the texbox is not empty
            {
                string link = txtVideoLink.Text; // sets link equal to text in textbox

                if (isYoutube(link) == true) // call isYoutube method and checks if is true, then converts youtube video to MP3
                {
                    var downloadsfolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // grabs user's path to downloads folder
                    var youtube = YouTube.Default;
                    var video = youtube.GetVideo(link);
                    File.WriteAllBytes(downloadsfolder + video.FullName, video.GetBytes());
                    var inputFile = new MediaFile { Filename = downloadsfolder + video.FullName };
                    var outputFilePath = Path.Combine(downloadsfolder, $"{video.FullName}.mp3");
                    var outputFile = new MediaFile { Filename = outputFilePath };

                    using (var engine = new Engine())
                    {
                        engine.GetMetadata(inputFile);
                        engine.Convert(inputFile, outputFile);
                    }

                    if (!chkbxKeepVideo.Checked) // checks that checkbox is not checked
                    {
                        File.Delete(Path.Combine(downloadsfolder, video.FullName)); // deletes downloaded youtube video
                    }
                }
            }
            else
            {
                System.Media.SystemSounds.Hand.Play(); // plays error sound??? i think????
                MessageBox.Show("Please specify a YouTube link.");
            }
        }

        private bool isYoutube(string link) // checks if remote file exists
        {
            HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
            request.Method = "HEAD";  // use head rather than get because it does not download the whole content!
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            response.Close();
            return (response.StatusCode == HttpStatusCode.OK);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // not needed!!!!
        }
    }
}
