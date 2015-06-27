using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ouroboros { 
    public class CardListBox : ListBox { // Custom ListBox that allows for multiple characters of searching
        protected override CreateParams CreateParams {
            get {
                var returnValue = base.CreateParams;
                returnValue.Style |= 0x2; // Add LBS_SORT (Allows for multiple characters of searching).
                returnValue.Style ^= 128; // Remove LBS_USETABSTOPS (gain an edit caret for matching)
                return returnValue;
            }
        }
    }
}
