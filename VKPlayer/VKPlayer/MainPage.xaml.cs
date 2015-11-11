using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            VKRequest.Dispatch<VKList<VKAudio>>(new VKRequestParameters(
                "audio.search",
                "q", "guns"),
                (result) =>
                {
                    foreach (var elenent in result.Data.items)
                    {
                        AudioList.Items.Add(elenent.title);
                    }
                });
        }
    }
}
