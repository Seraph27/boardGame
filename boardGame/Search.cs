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
        int depth = 1;
        private int WhiteSearch(BitBoard bd, int depth) {
            BitBoard root = new BitBoard();
            root.blackBit = bd.blackBit; root.whiteBit = bd.whiteBit;

            int[] whiteMoves = new int[brdSize];
            for (int i = 0; i < whiteNumberMoves; i++) {
                whiteMoves[i] = whiteLegalMoves[i];
            }

            int best = -1;
            int min = 99999;
            if (whiteNumberMoves == 1) best = whiteMoves[0];
            else {
                int score = min;
                int n = whiteNumberMoves;
                for (int i = 0; i < n; i++) {
                    score = whatTry(root, whiteMoves[i], depth);
                    if (score < min) {
                        min = score;
                        best = whiteMoves[i];
                    }
                }
            }

            brd.blackBit = bd.blackBit; brd.whiteBit = bd.whiteBit;
            return best;
            
        }

        private int whatTry(BitBoard pbd, int i, int depth) {
            BitBoard saveBoard = new BitBoard();

            saveBoard.blackBit = pbd.blackBit; saveBoard.whiteBit = pbd.whiteBit;
            putWhite(i); whiteFlips(i);
            BitBoard bd = new BitBoard();

            bd.blackBit = saveBoard.blackBit; bd.whiteBit = saveBoard.whiteBit;
            int score;
            if (isGameOver())
            {
                score = blackCount - whiteCount;
            }
            else {
                depth--;
                if (depth == 0)
                {
                    score = blackCount - whiteCount;
                }

                else {
                    score = 0;
                    /*
                    turn = blackTurn;
                    int blackMobility = blackGenMove(bd);
                    if (blackMobility == 0) { score = blackPass(bd, depth); }
                    else { score = blkMax(bd, blackMobility, depth); }
                    turn = whiteTurn;
                    */
                }
            }

            brd.blackBit = saveBoard.blackBit; brd.whiteBit = saveBoard.whiteBit;

            return score;
        }
    }
}
