namespace RecursiveMediaBrowser {

  public partial class KeybindsForm : Form {

    public KeybindsForm(Form Parent) {
      InitializeComponent();
      this.Owner = Parent;
    }

    private void OnKeybindsFormFormClosing(object _, FormClosingEventArgs Event) {
      if (Event.CloseReason == CloseReason.UserClosing) {
        Event.Cancel = true;
        this.Hide();
      }
    }
  }
}
