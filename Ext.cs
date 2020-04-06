using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using RudnevDragProject.Properties;

namespace RudnevDragProject
{
    public static class Ext
    {
        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);
        public static void setCursor(this Form form)
        {
            IntPtr hCursor;
            hCursor = CreateIconFromResource(Resources.Pointer, (uint)Resources.Pointer.Length, false, 0x00030000);
            form.Cursor = new Cursor(hCursor);
            foreach (var lbl in form.Controls.OfType<MaterialLabel>())
            {
                lbl.Cursor = new Cursor(hCursor);
            }
            foreach (var lbl in form.Controls.OfType<Label>())
            {
                lbl.Cursor = new Cursor(hCursor);
            }
            form.MouseEnter += (s, a) =>
            {
                form.Cursor = new Cursor(hCursor);
            };
            foreach (Button btn in form.Controls.OfType<Button>())
            {
                btn.MouseEnter += (s, a) =>
                {
                    hCursor = CreateIconFromResource(Resources.Link, (uint)Resources.Link.Length, false, 0x00030000);
                    form.Cursor = new Cursor(hCursor);
                };
                btn.MouseLeave += (s, a) =>
                {
                    hCursor = CreateIconFromResource(Resources.Pointer, (uint)Resources.Pointer.Length, false, 0x00030000);
                    form.Cursor = new Cursor(hCursor);
                };
            }
        }

        public static void setCursorControl(this Control control)
        {
            IntPtr hCursor;
            hCursor = CreateIconFromResource(Resources.Pointer, (uint)Resources.Pointer.Length, false, 0x00030000);
            control.Cursor = new Cursor(hCursor);
        }

        public static async void LoadAnimation(this Form form)
        {
            foreach (Control controlCollection in form.Controls)
            {
                controlCollection.Enabled = false;
            }
            for (form.Opacity = 0; form.Opacity < 1; form.Opacity += 0.01)
            {
                await Task.Delay(10);
            }
            await Task.Delay(200);
            foreach (Control controlCollection in form.Controls)
            {
                controlCollection.Enabled = true;
            }
        }
        public static async void AnimateText(this Label label)
        {
            char[] lbltext = label.Text.ToCharArray();
            label.Text = "";
            foreach (char ch in lbltext)
            {
                label.Text += ch.ToString();
                await Task.Delay(100);
            }
        }

        public static async void AnimateStr(string str)
        {
            str = "Finished picture";
            char[] lbltext = str.ToCharArray();
            str = "";
            foreach (char ch in lbltext)
            {
                str += ch.ToString();
                await Task.Delay(100);
            }
        }
    }
}
