using System.Windows.Forms;

namespace csharp_project
{
    public class MessageAlert
    {
        public static void ShowMessage(Form owner, MessageBoxIcon type, string header, string text)
        {
            MessageBox.Show(owner, text, header, MessageBoxButtons.OK, type);
        }

        public static void ShowErrorMessage(Form owner, string text)
        {
            MessageBox.Show(owner, text, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}