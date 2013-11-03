namespace DirSyncroClient
{
    partial class DirSyncroForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirSyncroForm));
            this.watcherGrid = new System.Windows.Forms.DataGridView();
            this.enabled = new System.Windows.Forms.DataGridViewImageColumn();
            this.watcher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.syncButton = new System.Windows.Forms.Button();
            this.confButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.watcherGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // watcherGrid
            // 
            this.watcherGrid.AllowUserToAddRows = false;
            this.watcherGrid.AllowUserToDeleteRows = false;
            this.watcherGrid.AllowUserToResizeRows = false;
            this.watcherGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.watcherGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.enabled,
            this.watcher});
            this.watcherGrid.Location = new System.Drawing.Point(12, 12);
            this.watcherGrid.MultiSelect = false;
            this.watcherGrid.Name = "watcherGrid";
            this.watcherGrid.ReadOnly = true;
            this.watcherGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.watcherGrid.RowHeadersVisible = false;
            this.watcherGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.watcherGrid.Size = new System.Drawing.Size(276, 183);
            this.watcherGrid.TabIndex = 2;
            this.watcherGrid.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.watcherGrid_MouseUp);
            // 
            // enabled
            // 
            this.enabled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.enabled.FillWeight = 24F;
            this.enabled.HeaderText = "";
            this.enabled.Name = "enabled";
            this.enabled.ReadOnly = true;
            this.enabled.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.enabled.Width = 24;
            // 
            // watcher
            // 
            this.watcher.FillWeight = 200F;
            this.watcher.HeaderText = "Name";
            this.watcher.Name = "watcher";
            this.watcher.ReadOnly = true;
            this.watcher.Width = 200;
            // 
            // addButton
            // 
            this.addButton.Image = global::DirSyncroClient.Properties.Resources.Add;
            this.addButton.Location = new System.Drawing.Point(12, 201);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(60, 24);
            this.addButton.TabIndex = 3;
            this.addButton.UseVisualStyleBackColor = true;
            // 
            // removeButton
            // 
            this.removeButton.Image = global::DirSyncroClient.Properties.Resources.Remove;
            this.removeButton.Location = new System.Drawing.Point(78, 201);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(60, 24);
            this.removeButton.TabIndex = 4;
            this.removeButton.UseVisualStyleBackColor = true;
            // 
            // syncButton
            // 
            this.syncButton.Image = ((System.Drawing.Image)(resources.GetObject("syncButton.Image")));
            this.syncButton.Location = new System.Drawing.Point(162, 201);
            this.syncButton.Name = "syncButton";
            this.syncButton.Size = new System.Drawing.Size(60, 24);
            this.syncButton.TabIndex = 5;
            this.syncButton.UseVisualStyleBackColor = true;
            // 
            // confButton
            // 
            this.confButton.Image = ((System.Drawing.Image)(resources.GetObject("confButton.Image")));
            this.confButton.Location = new System.Drawing.Point(228, 201);
            this.confButton.Name = "confButton";
            this.confButton.Size = new System.Drawing.Size(60, 24);
            this.confButton.TabIndex = 6;
            this.confButton.UseVisualStyleBackColor = true;
            // 
            // DirSyncroForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 236);
            this.Controls.Add(this.confButton);
            this.Controls.Add(this.syncButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.watcherGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DirSyncroForm";
            this.Text = "DirSyncro";
            ((System.ComponentModel.ISupportInitialize)(this.watcherGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView watcherGrid;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button syncButton;
        private System.Windows.Forms.Button confButton;
        private System.Windows.Forms.DataGridViewImageColumn enabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn watcher;
    }
}

