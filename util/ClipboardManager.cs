using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClipboardManager.util
{
    public class ClipboardManager : NativeWindow, IDisposable
    {
        public Action onCopyEvent;
        private const int WM_CLIPBOARDUPDATE = 0x031D;
        private int MaxHistorySize = 10;
        private bool isProcessingClipboard = false;
        private static List<string> clipboardHistory = new();

        [DllImport("user32.dll")]
        private static extern bool AddClipboardFormatListener(IntPtr hWnd);

        public ClipboardManager()
        {
            CreateHandle(new CreateParams());
            AddClipboardFormatListener(this.Handle);
            onCopyEvent += OnCopy;
        }

        // Override wndproc to detect clipboard change
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_CLIPBOARDUPDATE)
            {
                onCopyEvent?.Invoke();
            }
            base.WndProc(ref m);
        }

        public void OnCopy()
        {
            // Prevent re-entry if still processing
            if (isProcessingClipboard) return;
            isProcessingClipboard = true;

            StoreCurrentClipboard();

            isProcessingClipboard = false;
        }

        public void StoreCurrentClipboard()
        {
            try
            {
                // Only store text content
                if (!Clipboard.ContainsText()) return;

                string clipboardText = Clipboard.GetText();

                // Avoid storing accidental duplicate entries
                if (clipboardHistory.Count > 0 && clipboardHistory[0] == clipboardText)
                    return;

                if (clipboardHistory.Count == MaxHistorySize)
                {
                    clipboardHistory.RemoveAt(clipboardHistory.Count - 1);
                }

                clipboardHistory.Insert(0, clipboardText);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Clipboard read error: {ex.Message}");
            }
        }

        public void AlternateClipboards()
        {
            if (clipboardHistory.Count < 2)
            {
                System.Media.SystemSounds.Hand.Play();
                return;
            }

            // Swap the most recent two clipboard entries
            (clipboardHistory[0], clipboardHistory[1]) = (clipboardHistory[1], clipboardHistory[0]);

            try
            {
                Clipboard.Clear();
                System.Threading.Thread.Sleep(50);

                Clipboard.SetText(clipboardHistory[0]);
                System.Media.SystemSounds.Asterisk.Play();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Clipboard swap failed: {ex.Message}");
            }
        }

        public void Dispose()
        {
            DestroyHandle(); // Releases the native window handle
        }
    }
}
