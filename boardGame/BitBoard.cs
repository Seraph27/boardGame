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
         class BitBoard
         {
            public UInt64 blackBit;
            public UInt64 whiteBit;
         }
    }

    public class gameRecord {
        public UInt64 blackBits;
        public UInt64 whiteBits;
        public int lastMove;
    }
}
