using System.Drawing;
using System.Windows.Forms;

namespace TVMEmulator.helpers
{
    internal static class RichMessageBoxExtensions
    {
        public static void HighlightLine(this RichTextBox theBox, string theTag, FontStyle fontStyle = FontStyle.Bold)
        {
            if (theBox.InvokeRequired)
            {
                theBox.Invoke(new MethodInvoker(delegate { theBox.HighlightLine(theTag, fontStyle); }));
                return;
            }

            int end = 0;
            int begin = 0;

            while (end >= 0)
            {
                begin = theBox.Text.IndexOf(theTag, begin);
                if (begin >= 0)
                {
                    end = theBox.Text.IndexOf("\n", begin); //RTB uses \n not \r\n
                }
                if (begin < end && begin >= 0)
                {
                    theBox.Select(begin, end - begin);
                    theBox.SelectionFont = new Font(theBox.Font, fontStyle);
                    theBox.Select(0, 0);
                    begin = end;
                }
                else
                {
                    break;
                }
            }
        }

        public static void HighlightRange(this RichTextBox box, int start, int length, FontStyle fontStyle)
        {
            if (box.InvokeRequired)
            {
                box.Invoke(new MethodInvoker(delegate { box.HighlightRange(start, length, fontStyle); }));
                return;
            }

            if (start >= 0 && start < box.Text.Length - 1 && length <= box.Text.Length - start)
            {
                box.Select(start, length);
                box.SelectionFont = new Font(box.Font, fontStyle);
                box.DeselectAll();
            }
        }

    }
}
