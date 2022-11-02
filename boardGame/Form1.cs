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
        BitBoard brd = new BitBoard();
        UInt64 oBits;
        const int brdSize = 64;
        int[] mBx = new int[64];
        UInt64[] id = new UInt64[brdSize];
        const int blackTurn = 1;
        const int whiteTurn = -1;
        int ply, turn;
        UInt64 blackLegal, whiteLegal;
        gameRecord[] gmRec = new gameRecord[100];
        int lastBlack, lastWhite;
        int whiteNumberMoves, blackNumberMoves;
        int[] whiteLegalMoves = new int[20];
        int blackCount, whiteCount;
        int gameType = 1;

        int[,] dirBnd = new int[,] {
            {
                 -1,-1,-1,-1,-1,-1,-1,-1,
                 -1,-1,-1,-1,-1,-1,-1,-1,
                  0, 1, 2, 3, 4, 5, 6, 7,
                  0, 1, 2, 3, 4, 5, 6, 7,
                  0, 1, 2, 3, 4, 5, 6, 7,
                  0, 1, 2, 3, 4, 5, 6, 7,
                  0, 1, 2, 3, 4, 5, 6, 7,
                  0, 1, 2, 3, 4, 5, 6, 7
            },
            //往東北邊檢查
            {
                 -1,-1,-1,-1,-1,-1,-1,-1,
                 -1,-1,-1,-1,-1,-1,-1,-1,
                  2, 3, 4, 5, 6, 7,-1,-1,
                  3, 4, 5, 6, 7,15,-1,-1,
                  4, 5, 6, 7,15,23,-1,-1,
                  5, 6, 7,15,23,31,-1,-1,
                  6, 7,15,23,31,39,-1,-1,
                  7,15,23,31,39,47,-1,-1
            },
            //往東邊檢查
            {
                  7, 7, 7, 7, 7, 7,-1,-1,
                 15,15,15,15,15,15,-1,-1,
                 23,23,23,23,23,23,-1,-1,
                 31,31,31,31,31,31,-1,-1,
                 39,39,39,39,39,39,-1,-1,
                 47,47,47,47,47,47,-1,-1,
                 55,55,55,55,55,55,-1,-1,
                 63,63,63,63,63,63,-1,-1
            },
            //往東南邊檢查
            {
                 63,55,47,39,31,23,-1,-1,
                 62,63,55,47,39,31,-1,-1,
                 61,62,63,55,47,39,-1,-1,
                 60,61,62,63,55,47,-1,-1,
                 59,60,61,62,63,55,-1,-1,
                 58,59,60,61,62,63,-1,-1,
                 -1,-1,-1,-1,-1,-1,-1,-1,
                 -1,-1,-1,-1,-1,-1,-1,-1
            },
            //往南邊檢查
            {
                 56,57,58,59,60,61,62,63,
                 56,57,58,59,60,61,62,63,
                 56,57,58,59,60,61,62,63,
                 56,57,58,59,60,61,62,63,
                 56,57,58,59,60,61,62,63,
                 56,57,58,59,60,61,62,63,
                 -1,-1,-1,-1,-1,-1,-1,-1,
                 -1,-1,-1,-1,-1,-1,-1,-1
            },
            //往西南邊檢查
            {
                 -1,-1,16,24,32,40,48,56,
                 -1,-1,24,32,40,48,56,57,
                 -1,-1,32,40,48,56,57,58,
                 -1,-1,40,48,56,57,58,59,
                 -1,-1,48,56,57,58,59,60,
                 -1,-1,56,57,58,59,60,61,
                 -1,-1,-1,-1,-1,-1,-1,-1,
                 -1,-1,-1,-1,-1,-1,-1,-1
            },
            //往西邊檢查
            {
                 -1,-1, 0, 0, 0, 0, 0, 0,
                 -1,-1, 8, 8, 8, 8, 8, 8,
                 -1,-1,16,16,16,16,16,16,
                 -1,-1,24,24,24,24,24,24,
                 -1,-1,32,32,32,32,32,32,
                 -1,-1,40,40,40,40,40,40,
                 -1,-1,48,48,48,48,48,48,
                 -1,-1,56,56,56,56,56,56
            },
            //往西北邊檢查
            {
                 -1,-1,-1,-1,-1,-1,-1,-1,
                 -1,-1,-1,-1,-1,-1,-1,-1,
                 -1,-1, 0, 1, 2, 3, 4, 5,
                 -1,-1, 8, 0, 1, 2, 3, 4,
                 -1,-1,16, 8, 0, 1, 2, 3,
                 -1,-1,24,16, 8, 0, 1, 2,
                 -1,-1,32,24,16, 8, 0, 1,
                 -1,-1,40,32,24,16, 8, 0
            }
                };

        int[] dir = { -8, -7, 1, 9, 8, 7, -1, -9 };

        public othForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            prtBoard();
            initId();
            initBoard();

        }

        private void prtBoard()
        {
            FileStream fs = File.OpenRead("othello_board_8.jpg");
            othBoard.Image = Image.FromStream(fs);
            fs.Close();
        }

        public void putBlack(int i)
        {
            brd.blackBit |= id[i];
        }

        public void putWhite(int i)
        {
            brd.whiteBit |= id[i];
        }

        private void othBoard_Paint(object sender, PaintEventArgs e)
        {
            Graphics gl = e.Graphics;
            paintStone(gl, brd);
            if (ply > 0 && ply % 2 == 1) {
                blkLast(gl, lastBlack);
            }
            if (turn == blackTurn)
            {
                paintHint(gl, blackLegal);
            }
            else if (turn == whiteTurn) {
                paintHint(gl, whiteLegal);
            }
            
        }

        Bitmap bStone = new Bitmap("black.png");
        Bitmap wStone = new Bitmap("white.png");

        private void paintStone(Graphics g, BitBoard bd)
        {
            int x = 0; int y = 0;

            for (int i = 0; i < 64; i++)
            {
                //MessageBox.Show("i = " + i + " " + (bd.blackBit & id[i]));
                if ((bd.blackBit & id[i]) != 0)
                {
                    x = 25 + (i % 8) * 50;
                    y = 25 + (i / 8) * 50;
                    g.DrawImage(bStone, x + 2, y + 2, 46, 46);
                }


                if ((bd.whiteBit & id[i]) != 0)
                {
                    x = 25 + (i % 8) * 50;
                    y = 25 + (i / 8) * 50;
                    g.DrawImage(wStone, x + 2, y + 2, 46, 46);

                }

            }

        }

        private void othBoard_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            initGame();
        }

        private void lblTurn_Click(object sender, EventArgs e)
        {

        }

        private void initGame()
        {
            ply = 0;
            turn = blackTurn;
            picTurn.Image = bStone;
            blkHint(brd);
            othBoard.Refresh();
            btnStart.Visible = false;
            for (int i = 0; i < 100; i++)
            {
                gmRec[i] = new gameRecord();
            }
            gmRec[0].blackBits = brd.blackBit;
            gmRec[0].whiteBits = brd.whiteBit;


        }

        private void paintHint(Graphics g, UInt64 hint)
        {
            int x = 0, y = 0;
            Pen drawPen = null;
            if (turn == 1) drawPen = new Pen(Color.Black, 10);
            else if (turn == -1) drawPen = new Pen(Color.White, 10);
            for (int i = 0; i < 64; i++)
            {
                if ((id[i] & hint) != 0)
                {
                    x = 25 + (i % 8) * 50 + 20;
                    y = 25 + (i / 8) * 50 + 20;
                    g.DrawRectangle(drawPen, x, y, 10, 10);
                }
            }

        }
        private void blkHint(BitBoard bd)
        {
            oBits = bd.blackBit | bd.whiteBit;
            blackLegal = 0;
            for (int i = 0; i < 64; i++)
            {
                if ((oBits & id[i]) == 0)
                {
                    if (isBlackLegal(i))
                    {
                        blackLegal |= id[i];
                    }
                }
            }
        }

        private int whiteHint(BitBoard bd)
        {
            oBits = bd.blackBit | bd.whiteBit;
            whiteLegal = 0;
            int index = 0;
            for (int i = 0; i < 64; i++)
            {
                if ((oBits & id[i]) == 0)
                {
                    if (isWhiteLegal(i))
                    {
                        //MessageBox.Show("i is: " + i);
                        whiteLegal |= id[i];
                        whiteLegalMoves[index] = i;
                        index++;
                    }
                }
            }

            return index;
        }   

        private void blkLast(Graphics g, int i) {
            int x, y;
            Pen drawPen = new Pen(Color.White, 5);
            if (i >= 0) {
                x = 25 + (i % 8) * 50 + 24;
                y = 25 + (i / 8) * 50 + 24;
                g.DrawEllipse(drawPen, x, y, 5, 5);
            }
        }

        private void mvRec(int i) {
            gmRec[ply].lastMove = i;
            gmRec[ply].blackBits = brd.blackBit;
            gmRec[ply].whiteBits = brd.whiteBit;
        }

        private void blackRec() {
            int x_location = lastBlack % 8; int y_location = lastBlack / 8 + 1;
            string x = Convert.ToString((char)(65 + x_location));
            string LocString = x + Convert.ToString(y_location);
            string plyString = (ply > 9 ? "" : " ") + ply;
            textRec.Text += plyString + ". " + LocString;
        }

        private void whiteRec()
        {
            int x_location = lastWhite % 8; int y_location = lastWhite / 8 + 1;
            string x = Convert.ToString((char)(65 + x_location));
            string LocString = x + Convert.ToString(y_location);
            string plyString = (ply > 9 ? "" : " ") + ply;
            textRec.Text += plyString + ". " + LocString;
        }
        private void othBoard_MouseClick(object sender, MouseEventArgs e)
        {
            int i = getLoc(e);
            oBits = brd.blackBit | brd.whiteBit;
            if ((i >= 0 && i <= 63) && ((id[i] & oBits) == 0))
            {
                gmRec[ply].lastMove = i;

                if (turn == blackTurn && (blackLegal & id[i]) != 0)
                {
                    humanBlack(i);
                }
                else if (turn == whiteTurn && (whiteLegal & id[i]) != 0)
                {
                    humanWhite(i);
                }
                else
                {
                    MessageBox.Show("Error");
                }
                
            }
            else
            {
                MessageBox.Show("Bad Location");
            }
        }

        private int getLoc(MouseEventArgs e)
        {
            bool onBoard;
            onBoard = (e.X >= 25 && e.X <= 425) && (e.Y >= 25 && e.Y <= 425);
            if (onBoard)
            {
                int x, y;
                x = (e.X - 25) / 50;
                y = (e.Y - 25) / 50;
                return x + y * 8;
            }
            else return -1;
        }

        private bool isBlackLegal(int i)
        {
            for (int k = 0; k < 8; k++)
            {
                if (dirBnd[k, i] == -1) continue;
                if ((id[i + dir[k]] & brd.whiteBit) == 0) continue; // if black is on path
                int j = i + dir[k];
                do
                {
                    j += dir[k];
                    if ((id[j] & oBits) == 0) break;
                    if ((id[j] & brd.blackBit) != 0) return true;
                } while (j != dirBnd[k, i]);
            }
            return false;
        }

        private bool isWhiteLegal(int i)
        {
            oBits = brd.blackBit | brd.whiteBit;

            for (int k = 0; k < 8; k++)
            {
                if (dirBnd[k, i] == -1) continue;
                if ((id[i + dir[k]] & brd.blackBit) == 0) continue;

                int j = i + dir[k];
                do
                {
                    j += dir[k];
                    if ((id[j] & oBits) == 0) break;
                    if ((id[j] & brd.whiteBit) != 0) return true;
                } while (j != dirBnd[k, i]);
            }
            return false;
        }

        private void humanBlack(int i)
        {
            putBlack(i); lastBlack = i;
            blackFlips(i); 
            blackLegal = 0;
            ply++;
            mvRec(lastBlack);
            blackRec();
            turn = -turn; picTurn.Image = wStone;
            whiteNumberMoves = whiteHint(brd);
            othBoard.Refresh();

            if (whiteLegal == 0) {
                if (blackLegal == 0) {
                    isGameOver();
                }
            }

            if (gameType == 1) {
                int bestMove = WhiteSearch(brd, depth);
                computerWhite(bestMove);
            }
            
        }

        private void humanWhite(int i)
        {
            //othBoard.Refresh();
            //MessageBox.Show("Before put white");
            putWhite(i);
            lastWhite = i;
            whiteFlips(i);
            whiteLegal = 0;
            ply++;
            mvRec(lastWhite);
            whiteRec();
            turn = -turn; picTurn.Image = bStone;
            blkHint(brd);
            othBoard.Refresh();
        }

        private void blackFlips(int i)
        {
            oBits = brd.blackBit | brd.whiteBit;

            for (int k = 0; k < 8; k++)
            {
                if (dirBnd[k, i] == -1) continue;
                if ((id[i + dir[k]] & brd.whiteBit) == 0) continue;
                int j = i + dir[k];
                do
                {
                    j += dir[k];
                    if ((id[j] & oBits) == 0) break;
                    if ((id[j] & brd.blackBit) != 0)
                    {
                        int jj = i + dir[k];
                        while ((id[jj] & brd.whiteBit) != 0)
                        {
                            brd.whiteBit ^= id[jj];
                            brd.blackBit |= id[jj];
                            jj += dir[k];
                        }
                    }
                }
                while (j != dirBnd[k, i]);
            }
        }

        private void whiteFlips(int i)
        {
            oBits = brd.blackBit | brd.whiteBit;

            for (int k = 0; k < 8; k++)
            {
                othBoard.Refresh();
                if (dirBnd[k, i] == -1) continue;
                if ((id[i + dir[k]] & brd.blackBit) == 0) continue;
                int j = i + dir[k];
                do
                {
                    j += dir[k];
                    if ((id[j] & oBits) == 0) break;
                    if ((id[j] & brd.whiteBit) != 0)
                    {
                        int jj = i + dir[k];
                        //MessageBox.Show("i= " + i + " j= " + j + " jj= " + jj);
                        while ((id[jj] & brd.blackBit) != 0)
                        {
                            //MessageBox.Show(" " + jj);
                            brd.whiteBit |= id[jj];
                            brd.blackBit ^= id[jj];
                            jj += dir[k];
                        }
                    }
                }
                while (j != dirBnd[k, i]);
            }
        }

        private int blkCount() {
            int count = 0;
            for (int i = 0; i < 64; i++) {
                if ((brd.blackBit & id[i]) != 0) count++; 
            }

            return count;
        }

        private int whtCount()
        {
            int count = 0;
            for (int i = 0; i < 64; i++)
            {
                if ((brd.whiteBit & id[i]) != 0) count++;
            }

            return count;
        }

        private bool isGameOver() {
            if (brd.blackBit == 0 ||
               brd.whiteBit == 0 ||
               ~oBits == 0 ||
               (whiteLegal == 0 && blackLegal == 0))
            {
                return true;
            }
            else {
                return false;
            }
        }
    }

}