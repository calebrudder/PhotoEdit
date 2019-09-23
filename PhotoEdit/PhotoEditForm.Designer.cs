namespace PhotoEdit
{
    partial class PhotoEditForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhotoEditForm));
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.userControls = new System.Windows.Forms.GroupBox();
            this.invertButton = new System.Windows.Forms.Button();
            this.colorButton = new System.Windows.Forms.Button();
            this.brightnessBar = new System.Windows.Forms.TrackBar();
            this.imageView = new System.Windows.Forms.PictureBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.userControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageView)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(736, 797);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(141, 41);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(578, 797);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(141, 41);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click_1);
            // 
            // userControls
            // 
            this.userControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userControls.Controls.Add(this.invertButton);
            this.userControls.Controls.Add(this.colorButton);
            this.userControls.Controls.Add(this.brightnessBar);
            this.userControls.Location = new System.Drawing.Point(28, 622);
            this.userControls.Name = "userControls";
            this.userControls.Size = new System.Drawing.Size(848, 134);
            this.userControls.TabIndex = 2;
            this.userControls.TabStop = false;
            // 
            // invertButton
            // 
            this.invertButton.Location = new System.Drawing.Point(657, 55);
            this.invertButton.Name = "invertButton";
            this.invertButton.Size = new System.Drawing.Size(152, 41);
            this.invertButton.TabIndex = 4;
            this.invertButton.Text = "Invert";
            this.invertButton.UseVisualStyleBackColor = true;
            this.invertButton.Click += new System.EventHandler(this.InvertButton_Click);
            // 
            // colorButton
            // 
            this.colorButton.Location = new System.Drawing.Point(424, 55);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(152, 41);
            this.colorButton.TabIndex = 3;
            this.colorButton.Text = "Color...";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // brightnessBar
            // 
            this.brightnessBar.BackColor = System.Drawing.SystemColors.Control;
            this.brightnessBar.LargeChange = 25;
            this.brightnessBar.Location = new System.Drawing.Point(55, 38);
            this.brightnessBar.Maximum = 100;
            this.brightnessBar.Name = "brightnessBar";
            this.brightnessBar.Size = new System.Drawing.Size(326, 90);
            this.brightnessBar.TabIndex = 0;
            this.brightnessBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.brightnessBar.Value = 50;
            this.brightnessBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BrightnessBar_Scroll);
            // 
            // imageView
            // 
            this.imageView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imageView.Location = new System.Drawing.Point(28, 38);
            this.imageView.Name = "imageView";
            this.imageView.Size = new System.Drawing.Size(848, 567);
            this.imageView.TabIndex = 3;
            this.imageView.TabStop = false;
            // 
            // PhotoEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 873);
            this.Controls.Add(this.imageView);
            this.Controls.Add(this.userControls);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PhotoEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PhotoEditForm";
            this.userControls.ResumeLayout(false);
            this.userControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.GroupBox userControls;
        private System.Windows.Forms.TrackBar brightnessBar;
        private System.Windows.Forms.Button invertButton;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.PictureBox imageView;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}