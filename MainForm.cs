using LibVLCSharp.Shared;

namespace RecursiveMediaBrowser {

  public partial class MainForm : Form {

    private static readonly string[] ImageExtensions = [".png", ".jpg", ".jpeg", ".jfif"];
    private static readonly string[] OtherExtensions = [".mp4", ".mkv", ".avi", ".mp3", ".flac", ".wav", ".ogg"];

    private readonly LibVLC Vlc;

    private string RootFolder = null!;
    private readonly List<string> MediaFiles = [];
    private int CurrentIndex = 0;

    private bool Fullscreen = false;
    private Point LastLocation;

    public MainForm() {
      InitializeComponent();

      this.Vlc = new LibVLC();
      this.VlcVideoView.MediaPlayer = new MediaPlayer(this.Vlc);
    }

    private void OnMainFormLoad(object _, EventArgs Event) {
      if (this.OpenFolderDialog.ShowDialog() != DialogResult.OK) {
        this.Close();
      }

      this.Activate(); // Bring the window to the front. BringToFront() and Focus() didnt work for me

      this.RootFolder = this.OpenFolderDialog.SelectedPath;
      foreach (string FilePath in Directory.EnumerateFiles(this.RootFolder, "*", SearchOption.AllDirectories)) {
        if (ImageExtensions.Contains(Path.GetExtension(FilePath)) || OtherExtensions.Contains(Path.GetExtension(FilePath))) {
          this.MediaFiles.Add(FilePath);
        }
      }

      this.MediaFiles.Sort(new NaturalStringComparer());
      this.Play();
    }

    private void Play() {
      Media MediaFile = new(this.Vlc, MediaFiles[CurrentIndex]);
      string? MediaTitle = MediaFile.Meta(MetadataType.Title) ?? MediaFile.Meta(MetadataType.Description) ?? MediaFiles[CurrentIndex];

      this.Text = $"Recursive Media Browser - {MediaTitle}";
      this.VlcVideoView.MediaPlayer!.Play(MediaFile);
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
          this.VlcVideoView.MediaPlayer!.Volume = Math.Min(this.VlcVideoView.MediaPlayer!.Volume + 1, 100);
          break;

        case Keys.Down:
          this.VlcVideoView.MediaPlayer!.Volume = Math.Max(this.VlcVideoView.MediaPlayer!.Volume - 1, 0);
          break;

        case Keys.Space:
          this.VlcVideoView.MediaPlayer!.Pause();
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

        case Keys.Escape:
          this.Close();
          break;
      }
    }
  }
}
