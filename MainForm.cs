using ClipboardManager.util;
using System.Diagnostics;

namespace Mainform
{
    public partial class MainForm : Form
    {
        static ClipboardManager.util.ClipboardManager cm;
        static KeyboardListener keyboardListener;

        public MainForm()
        {
            InitializeComponent();
            
            cm = new();
            keyboardListener = new KeyboardListener();
            keyboardListener.OnKeyPressed += KeyPressedHandler;
        }

        private void KeyPressedHandler(KeyEventArgs e)
        {
            // Detect "Ctrl + Shift + X" 
            if (Control.ModifierKeys.HasFlag(Keys.Control) &&
                Control.ModifierKeys.HasFlag(Keys.Shift) &&
                e.KeyCode == Keys.X)
            {
                cm.AlternateClipboards();
            }
        }
       
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Hide();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Hide();
        }
        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {

            base.OnFormClosing(e);
        }

        private void exit_click(object sender, EventArgs e)
        {
            keyboardListener?.Dispose();
            cm?.Dispose();
            Application.Exit();
        }
    }


}
