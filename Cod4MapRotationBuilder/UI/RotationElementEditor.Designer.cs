namespace Cod4MapRotationBuilder.UI
{
    partial class RotationElementEditor
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
            this.components = new System.ComponentModel.Container();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.mapsListView = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.goToMapFolderButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.mapTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gameModesComboBox = new System.Windows.Forms.ComboBox();
            this.optionsGroupBox.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Controls.Add(this.mapsListView);
            this.optionsGroupBox.Controls.Add(this.goToMapFolderButton);
            this.optionsGroupBox.Controls.Add(this.panel3);
            this.optionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(250, 467);
            this.optionsGroupBox.TabIndex = 3;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            // 
            // mapsListView
            // 
            this.mapsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapsListView.HideSelection = false;
            this.mapsListView.LargeImageList = this.imageList;
            this.mapsListView.Location = new System.Drawing.Point(3, 127);
            this.mapsListView.Name = "mapsListView";
            this.mapsListView.Size = new System.Drawing.Size(244, 314);
            this.mapsListView.TabIndex = 3;
            this.mapsListView.UseCompatibleStateImageBehavior = false;
            this.mapsListView.SelectedIndexChanged += new System.EventHandler(this.mapsListView_SelectedIndexChanged);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(64, 64);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // goToMapFolderButton
            // 
            this.goToMapFolderButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.goToMapFolderButton.Location = new System.Drawing.Point(3, 441);
            this.goToMapFolderButton.Name = "goToMapFolderButton";
            this.goToMapFolderButton.Size = new System.Drawing.Size(244, 23);
            this.goToMapFolderButton.TabIndex = 0;
            this.goToMapFolderButton.Text = "Go to map folder";
            this.goToMapFolderButton.UseVisualStyleBackColor = true;
            this.goToMapFolderButton.Click += new System.EventHandler(this.goToMapFolderButton_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.mapTextBox);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.gameModesComboBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(244, 111);
            this.panel3.TabIndex = 3;
            // 
            // mapTextBox
            // 
            this.mapTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapTextBox.Location = new System.Drawing.Point(6, 72);
            this.mapTextBox.Name = "mapTextBox";
            this.mapTextBox.Size = new System.Drawing.Size(229, 20);
            this.mapTextBox.TabIndex = 4;
            this.mapTextBox.TextChanged += new System.EventHandler(this.mapTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Game mode:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Map:";
            // 
            // gameModesComboBox
            // 
            this.gameModesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameModesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gameModesComboBox.FormattingEnabled = true;
            this.gameModesComboBox.Location = new System.Drawing.Point(3, 27);
            this.gameModesComboBox.Name = "gameModesComboBox";
            this.gameModesComboBox.Size = new System.Drawing.Size(232, 21);
            this.gameModesComboBox.TabIndex = 0;
            this.gameModesComboBox.SelectedIndexChanged += new System.EventHandler(this.gameModesComboBox_SelectedIndexChanged);
            // 
            // RotationElementEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.optionsGroupBox);
            this.Name = "RotationElementEditor";
            this.Size = new System.Drawing.Size(250, 467);
            this.optionsGroupBox.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox optionsGroupBox;
        private System.Windows.Forms.ListView mapsListView;
        private System.Windows.Forms.Button goToMapFolderButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox mapTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox gameModesComboBox;
        private System.Windows.Forms.ImageList imageList;
    }
}
