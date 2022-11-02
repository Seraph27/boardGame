using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boardGame
{
    public partial class othForm : Form
    {
        private void initId()
        {
            id[0] = 1;
            for (int i = 1; i < brdSize; i++)
            {
                id[i] = id[i - 1] << 1; //10000000000....
            }
        }

        public void initBoard()
        {
            brd.blackBit = brd.whiteBit = 0;
            putWhite(27);
            putBlack(28);
            putBlack(35); 
            putWhite(36);
            othBoard.Refresh();
        }
    }
}
