using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boardGame
{
    public partial class othForm : Form
    {
        private void computerWhite(int i) {
            gmRec[ply].lastMove = lastBlack;
            putWhite(i); lastWhite = i;
            whiteFlips(i); whiteLegal = 0;
            ply++;
            whiteRec(); textRec.Refresh();
            oBits = brd.blackBit | brd.whiteBit;
            if (~oBits == 0 || brd.blackBit == 0)
            { othBoard.Refresh(); return; }   //add gameover
            // 換黑方下子
            turn = -turn; picTurn.Image = bStone;
            blkHint(brd);
            othBoard.Refresh();
            picTurn.Refresh();
            textRec.Refresh();

            if (blackLegal == 0) {
                if (brd.blackBit == 0) {
                    //gameOver
                }
                else {
                    MessageBox.Show("black pass");
                    lastBlack = -1;  ply++;
                    gmRec[ply].lastMove = lastBlack;
                    gmRec[ply].blackBits = brd.blackBit;
                    gmRec[ply].whiteBits = brd.whiteBit;
                    textRec.Text += "pass";
                }
                turn = -turn; picTurn.Image = wStone;
                whiteNumberMoves = whiteHint(brd);
                othBoard.Refresh();
                if (whiteLegal != 0)
                {
                    var best = WhiteSearch(brd, 1);
                    computerWhite(best);
                }
                else { 
                    //gameOver
                }
            }
        }
    }

}