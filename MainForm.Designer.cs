namespace RecursiveMediaBrowser
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.VlcVideoView = new LibVLCSharp.WinForms.VideoView();
      this.OpenFolderDialog = new FolderBrowserDialog();
      ((System.ComponentModel.ISupportInitialize) this.VlcVideoView).BeginInit();
      this.SuspendLayout();
      // 
      // VlcVideoView
      // 
      this.VlcVideoView.BackColor = Color.Black;
      this.VlcVideoView.Dock = DockStyle.Fill;
      this.VlcVideoView.Location = new Point(0, 0);
      this.VlcVideoView.MediaPlayer = null;
      this.VlcVideoView.Name = "VlcVideoView";
      this.VlcVideoView.Size = new Size(784, 411);
      this.VlcVideoView.TabIndex = 0;
      this.VlcVideoView.Text = "videoView1";
      // 
      // OpenFolderDialog
      // 
      this.OpenFolderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
      this.OpenFolderDialog.ShowNewFolderButton = false;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new SizeF(7F, 15F);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(784, 411);
      this.Controls.Add(this.VlcVideoView);
      this.KeyPreview = true;
      this.Name = "MainForm";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Recursive Media Browser";
      this.Load += this.OnMainFormLoad;
      this.KeyUp += this.OnMainFormKeyUp;
      ((System.ComponentModel.ISupportInitialize) this.VlcVideoView).EndInit();
      this.ResumeLayout(false);
    }

    #endregion

    private LibVLCSharp.WinForms.VideoView VlcVideoView;
    private FolderBrowserDialog OpenFolderDialog;
  }
}
