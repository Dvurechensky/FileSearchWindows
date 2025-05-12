/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:51:53
 * Version: 1.0.7
 */

namespace FileSearch
{
    partial class LargeListViewUserControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            lstItems = new ListView();
            SuspendLayout();
            // 
            // lstItems
            // 
            lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
            lstItems.FullRowSelect = true;
            lstItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            lstItems.Location = new Point(0, 0);
            lstItems.Name = "lstItems";
            lstItems.Size = new Size(483, 307);
            lstItems.TabIndex = 0;
            lstItems.UseCompatibleStateImageBehavior = false;
            lstItems.View = System.Windows.Forms.View.Details;
            lstItems.VirtualMode = true;
            lstItems.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.RetrieveVirtualItem);
            lstItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstItems_KeyDown);
            lstItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstItems_MouseDoubleClick);
            // 
            // LargeListViewUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lstItems);
            Name = "LargeListViewUserControl";
            Size = new Size(483, 307);
            SizeChanged += LargeListViewUserControl_SizeChanged;
            Enter += LargeListViewUserControl_Enter;
            ResumeLayout(false);
        }

        #endregion

        private ListView lstItems;
    }
}
