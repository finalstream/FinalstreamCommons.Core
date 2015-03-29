using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalstreamCommons.Utils
{
    public static class MessageUtils
    {

        public static DialogResult ShowQuestionYesNo(string message)
        {
            return MessageBox.Show(message, "Question?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
