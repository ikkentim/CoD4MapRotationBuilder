namespace Cod4MapRotationBuilder.UI
{
    partial class RotationElementPreview
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mapPreviewGroupBox = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.loadscreenPictureBox = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.compassPictureBox = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mapNameLabel = new System.Windows.Forms.Label();
            this.gameModeNameLabel = new System.Windows.Forms.Label();
            this.mapPreviewGroupBox.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadscreenPictureBox)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compassPictureBox)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapPreviewGroupBox
            // 
            this.mapPreviewGroupBox.Controls.Add(this.tabControl1);
            this.mapPreviewGroupBox.Controls.Add(this.panel2);
            this.mapPreviewGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapPreviewGroupBox.Location = new System.Drawing.Point(0, 0);
            this.mapPreviewGroupBox.Name = "mapPreviewGroupBox";
            this.mapPreviewGroupBox.Size = new System.Drawing.Size(378, 397);
            this.mapPreviewGroupBox.TabIndex = 4;
            this.mapPreviewGroupBox.TabStop = false;
            this.mapPreviewGroupBox.Text = "Map Preview";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(372, 276);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.loadscreenPictureBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(364, 250);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Loadscreen";
            // 
            // loadscreenPictureBox
            // 
            this.loadscreenPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadscreenPictureBox.Location = new System.Drawing.Point(3, 3);
            this.loadscreenPictureBox.Name = "loadscreenPictureBox";
            this.loadscreenPictureBox.Size = new System.Drawing.Size(358, 244);
            this.loadscreenPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loadscreenPictureBox.TabIndex = 0;
            this.loadscreenPictureBox.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.compassPictureBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(465, 433);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Compass";
            // 
            // compassPictureBox
            // 
            this.compassPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compassPictureBox.Location = new System.Drawing.Point(3, 3);
            this.compassPictureBox.Name = "compassPictureBox";
            this.compassPictureBox.Size = new System.Drawing.Size(459, 427);
            this.compassPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.compassPictureBox.TabIndex = 1;
            this.compassPictureBox.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.mapNameLabel);
            this.panel2.Controls.Add(this.gameModeNameLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 292);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(372, 102);
            this.panel2.TabIndex = 3;
            // 
            // mapNameLabel
            // 
            this.mapNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.mapNameLabel.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.mapNameLabel.Location = new System.Drawing.Point(0, 42);
            this.mapNameLabel.Name = "mapNameLabel";
            this.mapNameLabel.Size = new System.Drawing.Size(372, 42);
            this.mapNameLabel.TabIndex = 2;
            this.mapNameLabel.Text = "-";
            this.mapNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gameModeNameLabel
            // 
            this.gameModeNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.gameModeNameLabel.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.gameModeNameLabel.Location = new System.Drawing.Point(0, 0);
            this.gameModeNameLabel.Name = "gameModeNameLabel";
            this.gameModeNameLabel.Size = new System.Drawing.Size(372, 42);
            this.gameModeNameLabel.TabIndex = 1;
            this.gameModeNameLabel.Text = "-";
            this.gameModeNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RotationElementPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mapPreviewGroupBox);
            this.Name = "RotationElementPreview";
            this.Size = new System.Drawing.Size(378, 397);
            this.mapPreviewGroupBox.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadscreenPictureBox)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.compassPictureBox)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox mapPreviewGroupBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox loadscreenPictureBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox compassPictureBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label mapNameLabel;
        private System.Windows.Forms.Label gameModeNameLabel;
    }
}
