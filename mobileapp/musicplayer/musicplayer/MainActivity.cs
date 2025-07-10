using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Media;
using Android.Graphics.Drawables;
using Android.Widget;
using Android.Graphics;
using static Android.Widget.ImageView;
using Android.Views;
using Android.Graphics.Drawables.Shapes;
using Android.Content.Res;
using System.Net;
using System.IO;
using System;
using System.Threading.Tasks;
using Android.Util;

namespace musicplayer
{
    [Activity(Label = "RFID Jukebox", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        //Declared Variables
        MediaPlayer player;
        TextView tv1, tv2, tv3, tv4;
        LinearLayout myLinearLayout;
        ImageView music_image;
        Handler handler;
        Action runnable;
        HttpWebRequest request1, button;
        HttpWebResponse response1, response_button;
        Button eject_button;
        private bool isSongPlaying = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            InitUI(); //Called the method to initialize the UI
            InitBackgroundTask(); // method initializes the background task to update the UI
        }

        //Method for Default UI
        private void InitUI()
        {
            request1 = (HttpWebRequest)WebRequest.Create("http://192.168.1.6:8080/FINAL/update_song.php?songnumber=empty");
            button = (HttpWebRequest)WebRequest.Create("http://192.168.1.6:8080/FINAL/update_stat_song.php?status_song=1");
            response1 = (HttpWebResponse)request1.GetResponse();

            Toast.MakeText(this, "Tap ID To Play!", ToastLength.Long).Show();
            myLinearLayout = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            tv1 = FindViewById<TextView>(Resource.Id.textView1);
            tv2 = FindViewById<TextView>(Resource.Id.textView2);
            tv3 = FindViewById<TextView>(Resource.Id.textView3);
            tv4 = FindViewById<TextView>(Resource.Id.textView4);
            eject_button = FindViewById<Button>(Resource.Id.eject);

            GradientDrawable gradient = new GradientDrawable(
                GradientDrawable.Orientation.TopBottom,
                new int[] { Color.ParseColor("#4B0082"), Color.ParseColor("#000221") }
            );
            myLinearLayout.SetBackgroundDrawable(gradient);

            music_image = FindViewById<ImageView>(Resource.Id.imageView1);
            Drawable myDrawable = GetDrawable(Resource.Drawable.musictrack);
            music_image.SetImageDrawable(myDrawable);
            music_image.BackgroundTintList = ColorStateList.ValueOf(Color.Black);
            music_image.SetScaleType(ImageView.ScaleType.FitXy);
            music_image.LayoutParameters = new LinearLayout.LayoutParams(960, 960);

            Typeface tf_black = Typeface.CreateFromAsset(Assets, "GothamSSm-Black.otf");
            Typeface tf_book = Typeface.CreateFromAsset(Assets, "GothamSSm-Book.otf");
            Typeface tf_light = Typeface.CreateFromAsset(Assets, "GothamSSm-Light.otf");

            tv1.Typeface = tf_light;
            tv4.Typeface = tf_book;
            tv2.Typeface = tf_black;
            tv3.Typeface = tf_light;

            eject_button.Visibility = ViewStates.Gone;

            eject_button.Click += this.EjectButton_Click;
        }

        //
        private void InitBackgroundTask()
        {
            handler = new Handler(); // object to run the task
            runnable = new Action(async () => await UpdateLayoutAsync()); //object to update the UI
            handler.Post(runnable); //Posts the task to run every 5 seconds
        }

        /*
         *  Purpose of Asyncs in some methods:
                When making requests to a server, the program can continue executing other tasks while waiting for the response.
                Send requests to the server and update the UI without blocking the UI thread.
                Perform long-running operations, such as reading from a database or file, without blocking the UI thread.
                Improve the responsiveness and performance of the app by allowing other tasks to execute while waiting for the operations to complete.
         */






        /*
            The UpdateLayoutAsync method is called periodically to update the UI:
            Sends a request to the server to get the current song information
            Updates the UI based on the response
            Repeats the task every 5 seconds
         */

        private async Task UpdateLayoutAsync()
        {

            try
            {
                string songUrl = "http://192.168.1.6:8080/FINAL/search_song.php";
                string buttonUrl = "http://192.168.1.6:8080/FINAL/stat_button.php";
                string response = await GetServerResponseAsync(songUrl);
                string buttonResponse = await GetServerResponseAsync(buttonUrl);

                if (response != null || buttonResponse != null)
                {
                    RunOnUiThread(() => UpdateUIBasedOnResponse(response, buttonResponse));
                }
            }
            catch (Exception ex)
            {
                RunOnUiThread(() => Toast.MakeText(this, "Error: " + ex.Message, ToastLength.Long).Show());
            }

            handler.PostDelayed(runnable, 5000); // Repeat every 5 seconds
        }


        // method sends a request to the server and returns the response as a string.
        private async Task<string> GetServerResponseAsync(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 10000; // 10 seconds timeout

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("WebException: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        //method updates the UI based on the response from the server
        //Checks the response and updates the UI accordingly
        //Plays the corresponding song
        //Sets the completion listener to detect when the song finishes playing
        //The corresponding song will pause or play based on the response from the server.
        private void UpdateUIBasedOnResponse(string response, string buttonResponse)
        {

            //For button pause and play
            if (buttonResponse == "1")
            {
                if (player != null && !player.IsPlaying)
                {
                    player.Start();
                    isSongPlaying = true;
                }
                return;
            }

            if (buttonResponse == "2")
            {
                if (player != null && player.IsPlaying)
                {
                    player.Pause();
                    isSongPlaying = false;
                }
                return;
            }

          if (isSongPlaying) return;

      
            if (player != null)
            {
                player.Stop();
                player.Release();
                player = null;
            }

            request1 = (HttpWebRequest)WebRequest.Create("http://192.168.1.6:8080/FINAL/update_song.php?songnumber=empty");

            if (response == "692522124")
            {
                UpdateUI(
                    new int[] { Color.ParseColor("#800000"), Color.ParseColor("#190000") },
                    Resource.Drawable.bini,
                    "Cherry On Top",
                    "BINI"
                );
                eject_button.Visibility = ViewStates.Visible;
                player = MediaPlayer.Create(this, Resource.Raw.cherryontop);
                player.Start();
                isSongPlaying = true;
                //request1 = (HttpWebRequest)WebRequest.Create("http://192.168.1.6:8080/FINAL/update_song.php?songnumber=692522124");
                // set a completion listener to detect when the song finishes playing
                
                player.Completion += (sender, e) => {
                    isSongPlaying = false;
                    if (player != null)
                    {
                        player.Stop();
                        player.Release();
                    }
                    player = null;
                  ResetUI();

                };
            }
            else if (response == "16715917491")
            {
                UpdateUI(
                    new int[] { Color.ParseColor("#989898"), Color.ParseColor("#222222") },
                    Resource.Drawable.ybneet,
                    "ILY",
                    "Young Blood Neet, Bugoy Na Koykoy"
                );
                eject_button.Visibility = ViewStates.Visible;
                player = MediaPlayer.Create(this, Resource.Raw.ily);
                player.Start();
                isSongPlaying = true;
               // request1 = (HttpWebRequest)WebRequest.Create("http://192.168.1.6:8080/FINAL/update_song.php?songnumber=16715917491");
                // set a completion listener to detect when the song finishes playing
                player.Completion += (sender, e) => {
                    isSongPlaying = false;
                    if (player != null)
                    {
                        player.Stop();
                        player.Release();
                    }
                    player = null;
                    ResetUI();
                };
            }
            else if (response == "3111161228")
            {
                UpdateUI(
                    new int[] { Color.ParseColor("#4B0082"), Color.ParseColor("#000221") },
                    Resource.Drawable.brunomars,
                    "Leave The Door Open",
                    "Bruno Mars"
                );
                eject_button.Visibility = ViewStates.Visible;
                player = MediaPlayer.Create(this, Resource.Raw.leavedooropen);
                player.Start();
                isSongPlaying = true;
               // request1 = (HttpWebRequest)WebRequest.Create("http://192.168.1.6:8080/FINAL/update_song.php?songnumber=3111161228");
                // set a completion listener to detect when the song finishes playing
                player.Completion += (sender, e) => {
                    isSongPlaying = false;
                    if (player != null)
                    {
                        player.Stop();
                        player.Release();
                    }
                    player = null;
                    ResetUI();
                };
            }
            else if (response == "72100210131")
            {
                UpdateUI(
                    new int[] { Color.ParseColor("#AB8922"), Color.ParseColor("#181405") },
                    Resource.Drawable.elvisjpg,
                    "Can't Help Falling In Love With You",
                    "Elvis Presley",
                    20,
                    10
                );
                eject_button.Visibility = ViewStates.Visible;
                player = MediaPlayer.Create(this, Resource.Raw.canthelpfallinlove);
                player.Start();
                isSongPlaying = true;
              //  request1 = (HttpWebRequest)WebRequest.Create("http://192.168.1.6:8080/FINAL/update_song.php?songnumber=72100210131");

                // set a completion listener to detect when the song finishes playing
                player.Completion += (sender, e) => {
                    isSongPlaying = false;
                    if (player != null)
                    {
                        player.Stop();
                        player.Release();
                    }
                    player = null;
                    ResetUI();
                };

            }
            else if (response == "547168114")
            {
                UpdateUI(
                    new int[] { Color.ParseColor("#4B0082"), Color.ParseColor("#000221") },
                    Resource.Drawable.johndenver,
                    "Take Me Home, Country Roads",
                    "John Denver",
                    20
                    
                );
                eject_button.Visibility = ViewStates.Visible;
                player = MediaPlayer.Create(this, Resource.Raw.takemehome);
                player.Start();
                isSongPlaying = true;
               // request1 = (HttpWebRequest)WebRequest.Create("http://192.168.1.6:8080/FINAL/update_song.php?songnumber=547168114");
                // set a completion listener to detect when the song finishes playing
                player.Completion += (sender, e) => {

                    isSongPlaying = false;
                    if (player != null)
                    {
                        player.Stop();
                        player.Release();
                    }
                    player = null;
                    ResetUI();

                };
            }
           
        }

        //method resets the UI to its initial state
        //Updates the UI with default values
        //Hides the eject button
        //Sends a request to the server to update the song information
        private void ResetUI()
        {
            UpdateUI(
                new int[] { Color.ParseColor("#4B0082"), Color.ParseColor("#000221") },
                Resource.Drawable.musictrack,
                "TAP ID TO PLAY AGAIN",
                "Artist"
            );
            eject_button.Visibility = ViewStates.Gone;
            SendUpdateRequest("empty");
        }

        //method sends a request to the server to update the song information.
        private async Task SendUpdateRequest(string songNumber)
        {
            try
            {
                string url = $"http://192.168.1.6:8080/FINAL/update_song.php?songnumber={songNumber}";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string responseText = await reader.ReadToEndAsync();
                        // Log the response for debugging
                        Log.Debug("SendUpdateRequest", $"Response: {responseText}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Log.Error("SendUpdateRequest", ex.ToString());
            }
        }

        // method is called when the eject button is clicked
        /*  Stops the current song
            Resets the UI
            Shows a toast message
         */
        private void EjectButton_Click(object sender, EventArgs e)
        {
            if (player != null)
            {
                player.Stop();
                player.Release();
                player = null;
            }
            eject_button.Visibility = ViewStates.Gone;
            isSongPlaying = false;
            ResetUI();
            Toast.MakeText(this, "Song ejected!", ToastLength.Short).Show();
        }

        /*  method updates the UI with new values:
            Updates the background color and album art
            Updates the song title and artist
            Updates the font sizes
         */

        private void UpdateUI(int[] colors, int drawableId, string songTitle, string artist, int titleSize = 24, int artistSize = 14)
        {
            GradientDrawable gradient = new GradientDrawable(
                GradientDrawable.Orientation.TopBottom,
                colors
            );
            myLinearLayout.SetBackgroundDrawable(gradient);

            Drawable myDrawable = GetDrawable(drawableId);
            music_image.SetImageDrawable(myDrawable);

            tv2.Text = songTitle;
            tv3.Text = artist;
            tv2.TextSize = titleSize;
            tv3.TextSize = artistSize;

            myLinearLayout.Invalidate();
            music_image.Invalidate();
            tv2.Invalidate();
            tv3.Invalidate();
        }

        /*
         *  method is called when the activity is destroyed:
            Removes the background task
            Releases the MediaPlayer object
         */
        protected override void OnDestroy()
        {
            base.OnDestroy();
            handler.RemoveCallbacks(runnable);
            if (player != null)
            {
                player.Release();
                player = null;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
