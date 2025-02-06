namespace Mainform
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            notifyIcon = new NotifyIcon(components);
            SuspendLayout();
            // 
            // notifyIcon
            // 
            notifyIcon.Text = "ClipboardManager";
            notifyIcon.Icon = new Icon("assets/copy.ico");
            ///
            /// ContextMenuScript
            // 
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            
            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit");

            exitItem.Click += new System.EventHandler(exit_click);

            contextMenu.Items.Add(exitItem);
            notifyIcon.ContextMenuStrip = contextMenu;
            notifyIcon.Visible = true;
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon notifyIcon;
    }
}
