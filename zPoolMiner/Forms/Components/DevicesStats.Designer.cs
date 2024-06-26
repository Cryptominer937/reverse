﻿namespace HashKingsMiner.Forms.Components
{
    partial class DeviceStats
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
            this.listViewDevices = new System.Windows.Forms.ListView();
            this.columnHeader0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewDevices
            // 
            this.listViewDevices.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.listViewDevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader0});
            this.listViewDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDevices.ForeColor = System.Drawing.Color.White;
            this.listViewDevices.FullRowSelect = true;
            this.listViewDevices.Location = new System.Drawing.Point(0, 0);
            this.listViewDevices.Margin = new System.Windows.Forms.Padding(0);
            this.listViewDevices.Name = "listViewDevices";
            this.listViewDevices.Size = new System.Drawing.Size(377, 229);
            this.listViewDevices.TabIndex = 5;
            this.listViewDevices.UseCompatibleStateImageBehavior = false;
            this.listViewDevices.View = System.Windows.Forms.View.Details;
            this.listViewDevices.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListViewDevices_MouseClick);
            // 
            // columnHeader0
            // 
            this.columnHeader0.Text = "Device Stats";
            this.columnHeader0.Width = 370;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemEnable,
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 48);
            // 
            // toolStripMenuItemEnable
            // 
            this.toolStripMenuItemEnable.Name = "toolStripMenuItemEnable";
            this.toolStripMenuItemEnable.Size = new System.Drawing.Size(196, 22);
            this.toolStripMenuItemEnable.Text = "Enable Benchmark";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(196, 22);
            this.toolStripMenuItem1.Text = "Copy Benchmark From";
            // 
            // DeviceStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.listViewDevices);
            this.Name = "DeviceStats";
            this.Size = new System.Drawing.Size(377, 229);
            this.Resize += new System.EventHandler(this.DeviceStats_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView listViewDevices;
        private System.Windows.Forms.ColumnHeader columnHeader0;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEnable;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}
