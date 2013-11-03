namespace DirSyncroClient
{
    partial class ConfigureForm
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
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.enabledCheck = new System.Windows.Forms.CheckBox();
            this.targetLabel = new System.Windows.Forms.Label();
            this.targetList = new System.Windows.Forms.ListBox();
            this.targetButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.versionsLabel = new System.Windows.Forms.Label();
            this.retentionLabel = new System.Windows.Forms.Label();
            this.includeLabel = new System.Windows.Forms.Label();
            this.excludeLabel = new System.Windows.Forms.Label();
            this.includeText = new System.Windows.Forms.TextBox();
            this.sourceText = new System.Windows.Forms.TextBox();
            this.nameText = new System.Windows.Forms.TextBox();
            this.excludeText = new System.Windows.Forms.TextBox();
            this.sourceButton = new System.Windows.Forms.Button();
            this.retentionText = new System.Windows.Forms.TextBox();
            this.versionsText = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.enabledLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(232, 404);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(82, 33);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(329, 404);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(82, 33);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(15, 26);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(86, 13);
            this.sourceLabel.TabIndex = 3;
            this.sourceLabel.Text = "Source Directory";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(15, 58);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(35, 13);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Name";
            this.nameLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // enabledCheck
            // 
            this.enabledCheck.AutoSize = true;
            this.enabledCheck.Location = new System.Drawing.Point(122, 28);
            this.enabledCheck.Name = "enabledCheck";
            this.enabledCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.enabledCheck.Size = new System.Drawing.Size(15, 14);
            this.enabledCheck.TabIndex = 0;
            this.enabledCheck.UseVisualStyleBackColor = true;
            // 
            // targetLabel
            // 
            this.targetLabel.AutoSize = true;
            this.targetLabel.Location = new System.Drawing.Point(15, 94);
            this.targetLabel.Name = "targetLabel";
            this.targetLabel.Size = new System.Drawing.Size(91, 13);
            this.targetLabel.TabIndex = 4;
            this.targetLabel.Text = "Target Directories";
            this.targetLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // targetList
            // 
            this.targetList.Enabled = false;
            this.targetList.FormattingEnabled = true;
            this.targetList.Location = new System.Drawing.Point(122, 65);
            this.targetList.Name = "targetList";
            this.targetList.Size = new System.Drawing.Size(224, 69);
            this.targetList.TabIndex = 5;
            // 
            // targetButton
            // 
            this.targetButton.Image = global::DirSyncroClient.Properties.Resources.Folder;
            this.targetButton.Location = new System.Drawing.Point(353, 65);
            this.targetButton.Name = "targetButton";
            this.targetButton.Size = new System.Drawing.Size(24, 23);
            this.targetButton.TabIndex = 4;
            this.targetButton.UseVisualStyleBackColor = true;
            // 
            // removeButton
            // 
            this.removeButton.Image = global::DirSyncroClient.Properties.Resources.Delete;
            this.removeButton.Location = new System.Drawing.Point(353, 94);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(24, 24);
            this.removeButton.TabIndex = 8;
            this.removeButton.UseVisualStyleBackColor = true;
            // 
            // versionsLabel
            // 
            this.versionsLabel.AutoSize = true;
            this.versionsLabel.Location = new System.Drawing.Point(271, 27);
            this.versionsLabel.Name = "versionsLabel";
            this.versionsLabel.Size = new System.Drawing.Size(47, 13);
            this.versionsLabel.TabIndex = 9;
            this.versionsLabel.Text = "Versions";
            // 
            // retentionLabel
            // 
            this.retentionLabel.AutoSize = true;
            this.retentionLabel.Location = new System.Drawing.Point(271, 62);
            this.retentionLabel.Name = "retentionLabel";
            this.retentionLabel.Size = new System.Drawing.Size(53, 13);
            this.retentionLabel.TabIndex = 10;
            this.retentionLabel.Text = "Retention";
            // 
            // includeLabel
            // 
            this.includeLabel.AutoSize = true;
            this.includeLabel.Location = new System.Drawing.Point(16, 31);
            this.includeLabel.Name = "includeLabel";
            this.includeLabel.Size = new System.Drawing.Size(42, 13);
            this.includeLabel.TabIndex = 11;
            this.includeLabel.Text = "Include";
            // 
            // excludeLabel
            // 
            this.excludeLabel.AutoSize = true;
            this.excludeLabel.Location = new System.Drawing.Point(16, 62);
            this.excludeLabel.Name = "excludeLabel";
            this.excludeLabel.Size = new System.Drawing.Size(45, 13);
            this.excludeLabel.TabIndex = 12;
            this.excludeLabel.Text = "Exclude";
            this.excludeLabel.Click += new System.EventHandler(this.label7_Click);
            // 
            // includeText
            // 
            this.includeText.Location = new System.Drawing.Point(122, 24);
            this.includeText.Name = "includeText";
            this.includeText.Size = new System.Drawing.Size(131, 20);
            this.includeText.TabIndex = 5;
            // 
            // sourceText
            // 
            this.sourceText.Enabled = false;
            this.sourceText.Location = new System.Drawing.Point(122, 23);
            this.sourceText.MaxLength = 255;
            this.sourceText.Name = "sourceText";
            this.sourceText.Size = new System.Drawing.Size(224, 20);
            this.sourceText.TabIndex = 3;
            this.sourceText.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // nameText
            // 
            this.nameText.Location = new System.Drawing.Point(122, 55);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(224, 20);
            this.nameText.TabIndex = 2;
            this.nameText.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // excludeText
            // 
            this.excludeText.Location = new System.Drawing.Point(122, 59);
            this.excludeText.Name = "excludeText";
            this.excludeText.Size = new System.Drawing.Size(131, 20);
            this.excludeText.TabIndex = 7;
            // 
            // sourceButton
            // 
            this.sourceButton.Image = global::DirSyncroClient.Properties.Resources.Folder;
            this.sourceButton.Location = new System.Drawing.Point(352, 20);
            this.sourceButton.Name = "sourceButton";
            this.sourceButton.Size = new System.Drawing.Size(24, 24);
            this.sourceButton.TabIndex = 3;
            this.sourceButton.UseVisualStyleBackColor = true;
            // 
            // retentionText
            // 
            this.retentionText.Location = new System.Drawing.Point(334, 59);
            this.retentionText.MaxLength = 3;
            this.retentionText.Name = "retentionText";
            this.retentionText.Size = new System.Drawing.Size(43, 20);
            this.retentionText.TabIndex = 8;
            // 
            // versionsText
            // 
            this.versionsText.Location = new System.Drawing.Point(334, 24);
            this.versionsText.MaxLength = 4;
            this.versionsText.Name = "versionsText";
            this.versionsText.Size = new System.Drawing.Size(43, 20);
            this.versionsText.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.enabledLabel);
            this.groupBox1.Controls.Add(this.enabledCheck);
            this.groupBox1.Controls.Add(this.nameLabel);
            this.groupBox1.Controls.Add(this.nameText);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 98);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Definition";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // enabledLabel
            // 
            this.enabledLabel.AutoSize = true;
            this.enabledLabel.Location = new System.Drawing.Point(15, 28);
            this.enabledLabel.Name = "enabledLabel";
            this.enabledLabel.Size = new System.Drawing.Size(46, 13);
            this.enabledLabel.TabIndex = 3;
            this.enabledLabel.Text = "Enabled";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.sourceText);
            this.groupBox2.Controls.Add(this.sourceLabel);
            this.groupBox2.Controls.Add(this.sourceButton);
            this.groupBox2.Controls.Add(this.targetList);
            this.groupBox2.Controls.Add(this.targetButton);
            this.groupBox2.Controls.Add(this.removeButton);
            this.groupBox2.Controls.Add(this.targetLabel);
            this.groupBox2.Location = new System.Drawing.Point(13, 117);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(399, 161);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Source / Target Directories";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.excludeLabel);
            this.groupBox3.Controls.Add(this.includeLabel);
            this.groupBox3.Controls.Add(this.includeText);
            this.groupBox3.Controls.Add(this.versionsText);
            this.groupBox3.Controls.Add(this.retentionText);
            this.groupBox3.Controls.Add(this.retentionLabel);
            this.groupBox3.Controls.Add(this.excludeText);
            this.groupBox3.Controls.Add(this.versionsLabel);
            this.groupBox3.Location = new System.Drawing.Point(12, 284);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(399, 104);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Behaviour";
            // 
            // Configure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 451);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Configure";
            this.Text = "Configure";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.CheckBox enabledCheck;
        private System.Windows.Forms.Label targetLabel;
        private System.Windows.Forms.ListBox targetList;
        private System.Windows.Forms.Button targetButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Label versionsLabel;
        private System.Windows.Forms.Label retentionLabel;
        private System.Windows.Forms.Label includeLabel;
        private System.Windows.Forms.Label excludeLabel;
        private System.Windows.Forms.TextBox includeText;
        private System.Windows.Forms.TextBox sourceText;
        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.TextBox excludeText;
        private System.Windows.Forms.Button sourceButton;
        private System.Windows.Forms.TextBox retentionText;
        private System.Windows.Forms.TextBox versionsText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label enabledLabel;
    }
}