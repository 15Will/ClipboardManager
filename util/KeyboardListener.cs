using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardManager.util
{
    public class KeyboardListener : IDisposable
    {
        private IKeyboardMouseEvents _globalHook;

        public event Action<KeyEventArgs> OnKeyPressed;

        public KeyboardListener()
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += GlobalKeyDown;
        }

        private void GlobalKeyDown(object sender, KeyEventArgs e)
        {
            OnKeyPressed?.Invoke(e);
        }

        public void Dispose()
        {
            if (_globalHook != null)
            {
                _globalHook.KeyDown -= GlobalKeyDown;
                _globalHook.Dispose();
                _globalHook = null;
            }
        }
    }
}
