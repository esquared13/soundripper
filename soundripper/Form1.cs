using System.Net;
using System;
using System.IO;
using System.Windows.Forms;
using MediaToolkit; // used nuget
using MediaToolkit.Model; // used nuget
using VideoLibrary;
using System.Diagnostics.Tracing;
using System.Diagnostics;
using YoutubeExplode.Channels; // used nuget

namespace soundripper
{
    public partial class frmsoundripper : Form
    {
        public frmsoundripper()
        {
            InitializeComponent();
        }

        private frmDownloadProgress downloadprogress;
        // add conversion progress here 
        string username = Environment.UserName; // gets username
        string downloadspath;  // finds path to user downloads folder


        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtVideoLink.Text)) // check that the texbox is not empty
            {
                string link = txtVideoLink.Text; // sets link equal to text in textbox

                if (await IsYoutube(link)) // call IsYoutube method and checks if is true, then converts youtube video to MP3
                {
                    downloadspath = $"C:\\Users\\{username}\\Downloads\\";
                    await DownloadVideo(link, downloadspath);
                }
                else
                {
                    System.Media.SystemSounds.Hand.Play();
                    MessageBox.Show("Please specify a valid link.", "soundripper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                System.Media.SystemSounds.Hand.Play(); // plays error sound!!!!
                MessageBox.Show("Please specify a YouTube link.", "soundripper", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> IsYoutube(string link) // checks if remote file exists
        {
            HttpResponseMessage response;
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10); // sets request timeout
                    response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, link));
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
            }
            catch (WebException ex)
            {
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message + "\nUnable to process request.  There may have been an issue with your network connectivity or the site requested.", "soundripper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (UriFormatException ex1)
            {
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show(ex1.Message + "\nText entered was not a valid URL.", "soundripper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                response = null;
                response?.Dispose(); // closes response
            }
            return false;
        }

        private static async Task DownloadVideo(string link, string downloadspath) // downloads youtube video
        {
            var youtube = YouTube.Default;
            var video = youtube.GetVideo(link);
            var videotitle = video.Title;
            var path = $"{downloadspath}{videotitle}.mp4";
            var bytes = video.GetBytes();
            await File.WriteAllBytesAsync(path, bytes); // write file to path
        }


        private static async Task ConvertVideo()
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // not needed!!!! dont delete though it will break form designer!!!!!
        }
    }
}
