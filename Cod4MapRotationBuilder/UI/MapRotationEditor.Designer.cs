namespace Cod4MapRotationBuilder.UI
{
    partial class MapRotationEditor
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
            this.rotationGroupBox = new System.Windows.Forms.GroupBox();
            this.rotationListView = new System.Windows.Forms.ListView();
            this.gameModeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mapColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.addButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.rotationGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rotationGroupBox
            // 
            this.rotationGroupBox.Controls.Add(this.rotationListView);
            this.rotationGroupBox.Controls.Add(this.panel1);
            this.rotationGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rotationGroupBox.Location = new System.Drawing.Point(0, 0);
            this.rotationGroupBox.Name = "rotationGroupBox";
            this.rotationGroupBox.Size = new System.Drawing.Size(519, 482);
            this.rotationGroupBox.TabIndex = 5;
            this.rotationGroupBox.TabStop = false;
            this.rotationGroupBox.Text = "Rotation";
            // 
            // rotationListView
            // 
            this.rotationListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rotationListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.gameModeColumnHeader,
            this.mapColumnHeader});
            this.rotationListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rotationListView.FullRowSelect = true;
            this.rotationListView.HideSelection = false;
            this.rotationListView.Location = new System.Drawing.Point(3, 16);
            this.rotationListView.Name = "rotationListView";
            this.rotationListView.Size = new System.Drawing.Size(472, 463);
            this.rotationListView.TabIndex = 0;
            this.rotationListView.UseCompatibleStateImageBehavior = false;
            this.rotationListView.View = System.Windows.Forms.View.Details;
            this.rotationListView.SelectedIndexChanged += new System.EventHandler(this.rotationListView_SelectedIndexChanged);
            // 
            // gameModeColumnHeader
            // 
            this.gameModeColumnHeader.Text = "Game mode";
            this.gameModeColumnHeader.Width = 150;
            // 
            // mapColumnHeader
            // 
            this.mapColumnHeader.Text = "Map";
            this.mapColumnHeader.Width = 150;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.addButton);
            this.panel1.Controls.Add(this.deleteButton);
            this.panel1.Controls.Add(this.downButton);
            this.panel1.Controls.Add(this.upButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(475, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(41, 463);
            this.panel1.TabIndex = 0;
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(6, 428);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(32, 32);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "+";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(6, 79);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(32, 32);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "X";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(6, 41);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(32, 32);
            this.downButton.TabIndex = 1;
            this.downButton.Text = "\\/";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(6, 3);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(32, 32);
            this.upButton.TabIndex = 0;
            this.upButton.Text = "/\\";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // MapRotationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rotationGroupBox);
            this.Name = "MapRotationEditor";
            this.Size = new System.Drawing.Size(519, 482);
            this.rotationGroupBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox rotationGroupBox;
        private System.Windows.Forms.ListView rotationListView;
        private System.Windows.Forms.ColumnHeader gameModeColumnHeader;
        private System.Windows.Forms.ColumnHeader mapColumnHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button upButton;
    }
}
