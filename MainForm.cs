using LibVLCSharp.Shared;

namespace RecursiveMediaBrowser {

  public partial class MainForm : Form {

    private static readonly string[] SupportedFileExtensions = [".3g2", ".3ga", ".3gp", ".3gp2", ".3gpp", ".669", ".a52", ".aac", ".ac3", ".adts", ".aif", ".aifc", ".aiff", ".amr", ".amv", ".aob", ".ape", ".apng", ".asf", ".asx", ".au", ".avi", ".avif", ".b4s", ".bik", ".caf", ".cda", ".cue", ".dav", ".divx", ".drc", ".dts", ".dv", ".dvr-ms", ".evo", ".f4v", ".flac", ".flv", ".gvi", ".gxf", ".ifo", ".it", ".jfif", ".jpg", ".jpeg", ".m1v", ".m2t", ".m2ts", ".m2v", ".m3u", ".m3u8", ".m4a", ".m4p", ".m4v", ".mid", ".mka", ".mkv", ".mlp", ".mod", ".mov", ".mp1", ".mp2", ".mp2v", ".mp3", ".mp4", ".mp4v", ".mpa", ".mpc", ".mpe", ".mpeg", ".mpeg1", ".mpeg2", ".mpeg4", ".mpg", ".mpga", ".mpgv2", ".mts", ".mtv", ".mxf", ".nsv", ".nuv", ".oga", ".ogg", ".ogm", ".ogv", ".ogx", ".oma", ".opus", ".pls", ".pjp", ".pjpeg", ".png", ".qcp", ".ra", ".ram", ".rec", ".rm", ".rmi", ".rmvb", ".rpi", ".s3m", ".sdp", ".snd", ".spx", ".thp", ".tod", ".tp", ".ts", ".tta", ".tts", ".vlc", ".vlt", ".vob", ".voc", ".vqf", ".vro", ".w64", ".wav", ".webm", ".webp", ".wma", ".wmv", ".wpl", ".wsz", ".wtv", ".wv", ".wvx", ".xa", ".xesc", ".xm", ".xspf", ".zpl"];

    private readonly LibVLC Vlc;
    private readonly MediaPlayer Player;

    private readonly List<string> MediaFiles = [];
    private int CurrentIndex = 0;

    private bool Fullscreen = false;
    private Point LastLocation;

    private readonly KeybindsForm KeybindsWindow;

    public MainForm() {
      InitializeComponent();
      this.KeybindsWindow = new(this);

      this.Vlc = new LibVLC();
      this.Player = new MediaPlayer(this.Vlc);
      this.VlcVideoView.MediaPlayer = this.Player;
    }

    private void OnMainFormLoad(object _, EventArgs Event) {
      if (this.OpenFolderDialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(this.OpenFolderDialog.SelectedPath)) {
        this.Close();
      }

      this.Activate(); // Bring the window to the front. BringToFront() and Focus() didnt work for me

      #region Check and update when the Program was last opened and show Keybinds if it was more than a day ago
      string LastOpenedFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RecursiveMediaBrowserLastOpen.txt");
      if (!File.Exists(LastOpenedFilePath)) {
        this.KeybindsWindow.Show();

      } else {
        long LastOpenedTimestamp = long.Parse(File.ReadAllText(LastOpenedFilePath));

        if (DateTimeOffset.FromUnixTimeSeconds(LastOpenedTimestamp).AddDays(1) < DateTimeOffset.UtcNow) {
          this.KeybindsWindow.Show();
        }
      }

      File.WriteAllText(LastOpenedFilePath, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());
      #endregion

      foreach (string FilePath in Directory.EnumerateFiles(this.OpenFolderDialog.SelectedPath, "*", SearchOption.AllDirectories)) {
        if (SupportedFileExtensions.Contains(Path.GetExtension(FilePath))) {
          this.MediaFiles.Add(FilePath);
        }
      }

      this.MediaFiles.Sort(new NaturalStringComparer());
      this.Play();
    }

    private void Play() {
      using Media MediaFile = new(this.Vlc, MediaFiles[CurrentIndex]);
      string? MediaTitle = MediaFile.Meta(MetadataType.Title) ?? MediaFile.Meta(MetadataType.Description) ?? MediaFiles[CurrentIndex];

      this.Text = $"Recursive Media Browser - {MediaTitle}";
      this.Player.Play(MediaFile);
    }

    private void OnMainFormKeyUp(object _, KeyEventArgs Event) {
      // Next/Previous use modulo to wrap around at the ends of the list

      switch (Event.KeyCode) {

        case Keys.Right:
          this.CurrentIndex = (CurrentIndex + 1) % MediaFiles.Count;
          this.Play();
          break;

        case Keys.Left:
          this.CurrentIndex = (CurrentIndex - 1 + MediaFiles.Count) % MediaFiles.Count;
          this.Play();
          break;

        case Keys.Up:
          this.Player.Volume = Math.Min(this.Player.Volume + 1, 100);
          break;

        case Keys.Down:
          this.Player.Volume = Math.Max(this.Player.Volume - 1, 0);
          break;

        case Keys.Space:
          this.Player.Pause();
          break;

        case Keys.R:
          this.CurrentIndex = Random.Shared.Next(MediaFiles.Count);
          this.Play();
          break;

        case Keys.F:
          if (!this.Fullscreen) {
            this.LastLocation = this.Location;
          }

          this.Fullscreen = !this.Fullscreen;
          this.Size = this.Fullscreen ? Screen.FromControl(this).Bounds.Size : new Size(800, 450);
          this.Location = this.Fullscreen ? Screen.FromControl(this).Bounds.Location : this.LastLocation;
          this.FormBorderStyle = this.Fullscreen ? FormBorderStyle.None : FormBorderStyle.Sizable;
          break;

        case Keys.K:
          KeybindsWindow.Show();
          break;

        case Keys.Q:
          this.Player.PreviousChapter();
          break;

        case Keys.E:
          this.Player.NextChapter();
          break;

        case Keys.W:
          this.Player.NextFrame();
          break;

        case Keys.A:
          if (this.Player.IsSeekable) {
            this.Player.SeekTo(TimeSpan.FromMilliseconds(this.Player.Time - 10000));
          }
          break;

        case Keys.D:
          if (this.Player.IsSeekable) {
            this.Player.SeekTo(TimeSpan.FromMilliseconds(this.Player.Time + 10000));
          }
          break;

        case Keys.S:
          this.Player.SetPause(true);
          this.Player.Position = 0;
          break;

        case Keys.Escape:
          this.Close();
          break;
      }
    }
  }
}
