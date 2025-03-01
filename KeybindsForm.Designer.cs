namespace RecursiveMediaBrowser {
  partial class KeybindsForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeybindsForm));
      this.KeybindsLabel = new Label();
      this.SuspendLayout();
      // 
      // KeybindsLabel
      // 
      this.KeybindsLabel.BackColor = Color.Transparent;
      this.KeybindsLabel.Dock = DockStyle.Fill;
      this.KeybindsLabel.Font = new Font("Lucida Console", 11.25F, FontStyle.Regular, GraphicsUnit.Point,  0);
      this.KeybindsLabel.ForeColor = SystemColors.Window;
      this.KeybindsLabel.Location = new Point(0, 0);
      this.KeybindsLabel.Margin = new Padding(0);
      this.KeybindsLabel.Name = "KeybindsLabel";
      this.KeybindsLabel.Padding = new Padding(8);
      this.KeybindsLabel.Size = new Size(334, 261);
      this.KeybindsLabel.TabIndex = 0;
      this.KeybindsLabel.Text = resources.GetString("KeybindsLabel.Text");
      // 
      // KeybindsForm
      // 
      this.AutoScaleDimensions = new SizeF(7F, 15F);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(  24,   24,   24);
      this.ClientSize = new Size(334, 261);
      this.Controls.Add(this.KeybindsLabel);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "KeybindsForm";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Recursive Media Browser - Keybinds";
      this.FormClosing += this.OnKeybindsFormFormClosing;
      this.ResumeLayout(false);
    }

    #endregion

    private Label KeybindsLabel;
  }
}