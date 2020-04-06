using MaterialSkin;
using MaterialSkin.Controls;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RudnevDragProject
{
    public static class MaterialMessageBox
    {
        public static DialogResult Res(string message, string caption, MessageBoxButtons button)
        {
            DialogResult result = DialogResult.None;
            switch (button)
            {
                case MessageBoxButtons.YesNo:
                    using (frmYesNo yesNo = new frmYesNo())
                    {
                        yesNo.Text = caption;
                        yesNo.Message = message;
                        yesNo.setCursor();
                        yesNo.LoadAnimation();
                        result = yesNo.ShowDialog();
                    }
                    break;
                case MessageBoxButtons.OK:
                    using (frmOK ok = new frmOK())
                    {
                        ok.Text = caption;
                        ok.Message = message;
                        ok.setCursor();
                        ok.LoadAnimation();
                        result = ok.ShowDialog();
                    }
                    break;
            }
            return result;
        }
        public static void CustomMessageBox(this MaterialForm form)
        {
            MaterialSkinManager skinManager;
            skinManager = MaterialSkinManager.Instance;
            skinManager.Theme = MaterialSkinManager.Themes.DARK;
            skinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Grey600, Primary.Grey900, Accent.LightBlue200, TextShade.WHITE);
            foreach (MaterialLabel lbl in form.Controls.OfType<MaterialLabel>())
            {
                lbl.BackColor = Color.Transparent;
            }
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.MaximumSize = form.Size;
            form.MinimumSize = form.Size;
            form.setCursor();
        }
    }
}
