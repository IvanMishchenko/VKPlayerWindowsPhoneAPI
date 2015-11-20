using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using VK.WindowsPhone.SDK;
using VK.WindowsPhone.SDK.API;
using VK.WindowsPhone.SDK.API.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace VKPlayer
{
    
    public sealed partial class MainPage : Page
    {
        private readonly List<string> _scope = new List<string>() { VKScope.AUDIO }; 

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            VKSDK.Initialize("5138837");

            VKSDK.Authorize(_scope);
        }
        
        private void PlayMusic_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PlayTrack((sender as TextBlock).Tag.ToString());
        }

        private string GetTrueUrl(string url)
        {
            return url.Substring(0, url.IndexOf('?'));
        }

        private void TextQuest_TextChanged(object sender, TextChangedEventArgs e)
        {
            VKRequest.Dispatch<VKList<VKAudio>>(new VKRequestParameters(
                "audio.search",
                "q", TextQuest.Text),
                (result) =>
                {
                    AudioList.ItemsSource = result.Data.items;
                });
        }

        private void NextSound_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PlayTrack((AudioList.Items[++AudioList.SelectedIndex] as VKAudio).url);
        }

        private void PlayTrack(string url)
        {
            PlayerElement.Source = new Uri(GetTrueUrl(url));
            PlayerElement.Play();
        }

        private void PreviewSound_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PlayTrack((AudioList.Items[--AudioList.SelectedIndex] as VKAudio).url);

        }

        private void Play_Pausa_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (PlayerElement.CurrentState==MediaElementState.Playing)
            {
                PlayerElement.Pause();
                Image_Loaded(@"Resources/play.png");
            }
            else
            {
                PlayerElement.Play();
                Image_Loaded(@"Resources/pausa.png");
            }
     
        }

        private void Image_Loaded(string image)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.UriSource = new Uri(PlayButton.BaseUri, image);
            PlayButton.Source = bitmapImage;

        }
    }
}
