﻿/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

namespace FileSearch
{
    partial class ExceptionsForm
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
            lstItems = new ListView();
            lstcFile = new ColumnHeader();
            lstcException = new ColumnHeader();
            SuspendLayout();
            // 
            // lstItems
            // 
            lstItems.Columns.AddRange(new ColumnHeader[] { lstcFile, lstcException });
            lstItems.Dock = DockStyle.Fill;
            lstItems.FullRowSelect = true;
            lstItems.GridLines = true;
            lstItems.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lstItems.Location = new Point(0, 0);
            lstItems.Margin = new Padding(4, 3, 4, 3);
            lstItems.Name = "lstItems";
            lstItems.Size = new Size(740, 374);
            lstItems.TabIndex = 2;
            lstItems.UseCompatibleStateImageBehavior = false;
            lstItems.View = View.Details;
            lstItems.VirtualMode = true;
            lstItems.RetrieveVirtualItem += lstItems_RetrieveVirtualItem;
            // 
            // lstcFile
            // 
            lstcFile.Text = "Файл";
            lstcFile.Width = 400;
            // 
            // lstcException
            // 
            lstcException.Text = "Ошибка";
            lstcException.Width = 200;
            // 
            // ExceptionsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(740, 374);
            Controls.Add(lstItems);
            Margin = new Padding(4, 3, 4, 3);
            MinimizeBox = false;
            Name = "ExceptionsForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ошибки";
            ResumeLayout(false);
        }

        #endregion

        private ListView lstItems;
        private ColumnHeader lstcFile;
        private ColumnHeader lstcException;
    }
}