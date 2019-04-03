using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KolkoKrzyzyk
{
    public partial class Form1 : Form
    {
        bool turn = true; // true = gracz, false = komputer
        bool gameInProgress = false, winMove = false;
        int turnCount = 0;
        int wonCount = 0, lostCount = 0, tiedCount = 0;
        string szPlayer = "x", szAI = "o";

        public Form1()
        {
            InitializeComponent();

            difficultyBox.SelectedIndex = 2;
            startXCheckBox.Checked = true;
            gameInProgress = true;
            AI_MakeMove();
        }


        private void BoxClick(object sender, EventArgs e) // event kliknięcia pola na planszy
        {
            PictureBox picBox = (PictureBox)sender;
            MakeMove(picBox);
            AI_MakeMove();
        } // end boxClick


        private void WinCheck() // sprawdzenie, czy są na planszy 3 takie same figury w rzędzie
        {
            bool gameWon = false;
            
            //poziomo
            if ((A1.Image.Tag == A2.Image.Tag) && (A2.Image.Tag == A3.Image.Tag) && (!A1.Enabled)) gameWon = true;
            else if ((B1.Image.Tag == B2.Image.Tag) && (B2.Image.Tag == B3.Image.Tag) && (!B1.Enabled)) gameWon = true;
            else if ((C1.Image.Tag == C2.Image.Tag) && (C2.Image.Tag == C3.Image.Tag) && (!C1.Enabled)) gameWon = true;

            //pionowo
            else if ((A1.Image.Tag == B1.Image.Tag) && (B1.Image.Tag == C1.Image.Tag) && (!A1.Enabled)) gameWon = true;
            else if ((A2.Image.Tag == B2.Image.Tag) && (B2.Image.Tag == C2.Image.Tag) && (!A2.Enabled)) gameWon = true;
            else if ((A3.Image.Tag == B3.Image.Tag) && (B3.Image.Tag == C3.Image.Tag) && (!A3.Enabled)) gameWon = true;

            //skośnie
            else if ((A1.Image.Tag == B2.Image.Tag) && (B2.Image.Tag == C3.Image.Tag) && (!A1.Enabled)) gameWon = true;
            else if ((A3.Image.Tag == B2.Image.Tag) && (B2.Image.Tag == C1.Image.Tag) && (!A3.Enabled)) gameWon = true;

            if (gameWon)
            {
                DisableBoxes();

                if (!turn)
                {
                    wonCount++;
                    wonCountLabel.Text = wonCount.ToString();
                }
                else
                {
                    lostCount++;
                    lostCountLabel.Text = lostCount.ToString();
                }
            } 
            else if (turnCount == 8)
            {
                DisableBoxes();
                tiedCount++;
                tiedCountLabel.Text = tiedCount.ToString();
            }
            
        } // end winCheck


        private void DisableBoxes() // wyłączenie interakcji z polami na planszy
        {
            foreach (Control c in this.Controls.OfType<PictureBox>())
                ((PictureBox)c).Enabled = false;
            gameInProgress = false;
        } // end disableBoxes


        private void newGameButton_Click(object sender, EventArgs e) // event kliknięcia przycisku "nowa gra"
        {
            foreach (Control c in this.Controls.OfType<PictureBox>())
            {
                ((PictureBox)c).Image = Properties.Resources.xoblank;
                ((PictureBox)c).Image.Tag = "0";
                ((PictureBox)c).Enabled = true;
            }
            turnCount = 0;
            if (!startXCheckBox.Checked)
            {
                turn = false;
                szPlayer = "o";
                szAI = "x";
            }
            else
            {
                turn = true;
                szAI = "o";
                szPlayer = "x";
            }

            gameInProgress = true;
            AI_MakeMove();
        } // end newGameButton_Click

        private void MakeMove(PictureBox picBox) // wstawienie pola na planszę
        {
            if (picBox.Enabled)
            {
                if (turn)
                {
                    picBox.Image = Properties.Resources.oxx;
                    picBox.Image.Tag = "x";
                }

                else
                {
                    picBox.Image = Properties.Resources.oxo;
                    picBox.Image.Tag = "o";
                }

                turn = !turn;
                picBox.Enabled = false;
                WinCheck();
                turnCount++;
            }
        } // end MakeMove

        private void AI_MakeMove() // ruch AI
        {
            if (gameInProgress)
            {
                Random rand = new Random();
                int i, sleepTime;
                i = rand.Next(11);
                sleepTime = rand.Next(150, 800);
                System.Threading.Thread.Sleep(sleepTime);
                if (!turn)
                {
                    switch (difficultyBox.SelectedIndex)
                    {
                        case 0:
                            if (i <= 3) AI_MakePerfectMove();
                            else MakeMove_Random();
                            break;
                        case 1:
                            if (i >= 6) MakeMove_Random();
                            else AI_MakePerfectMove();
                            break;
                        case 2:
                            AI_MakePerfectMove();
                            break;
                        default:
                            break;
                    } // end switch
                } // end if 
            }
        } // end AI_MakeMove

        private void MakeMove_Random() // wstawienie pola na planszę w losowym miejscu
        {
            Random rand = new Random();
            bool moveMade = false;
            int i;
            while (!moveMade)
            {
                i = rand.Next(9);
                switch (i)
                {
                    case 0:
                        if (A1.Enabled)
                        {
                            MakeMove(A1);
                            moveMade = true;
                        }
                        break;
                    case 1:
                        if (A2.Enabled)
                        {
                            MakeMove(A2);
                            moveMade = true;
                        }
                        break;
                    case 2:
                        if (A3.Enabled)
                        {
                            MakeMove(A3);
                            moveMade = true;
                        }
                        break;
                    case 3:
                        if (B1.Enabled)
                        {
                            MakeMove(B1);
                            moveMade = true;
                        }
                        break;
                    case 4:
                        if (B2.Enabled)
                        {
                            MakeMove(B2);
                            moveMade = true;
                        }
                        break;
                    case 5:
                        if (B3.Enabled)
                        {
                            MakeMove(B3);
                            moveMade = true;
                        }
                        break;
                    case 6:
                        if (C1.Enabled)
                        {
                            MakeMove(C1);
                            moveMade = true;
                        }
                        break;
                    case 7:
                        if (C2.Enabled)
                        {
                            MakeMove(C2);
                            moveMade = true;
                        }
                        break;
                    case 8:
                        if (C3.Enabled)
                        {
                            MakeMove(C3);
                            moveMade = true;
                        }
                        break;
                } // end switch
            }// end while
            moveMade = false;
        } // end MakeMove_Random

        private void CheckForWinMove(String player) // sprawdzenie czy są na planszy 2 figury gwarantujące wygraną w następnej turze i dołożenie trzeciej
        {
            winMove = false;
            //poziomo
            if ((A1.Image.Tag == A2.Image.Tag) && (A1.Image.Tag == player) && (A3.Enabled)) { MakeMove(A3); winMove = true; }
            else if ((A1.Image.Tag == A3.Image.Tag) && (A1.Image.Tag == player) && (A2.Enabled)) { MakeMove(A2); winMove = true; }
            else if ((A2.Image.Tag == A3.Image.Tag) && (A2.Image.Tag == player) && (A1.Enabled)) { MakeMove(A1); winMove = true; }
            else if ((B1.Image.Tag == B2.Image.Tag) && (B1.Image.Tag == player) && (B3.Enabled)) { MakeMove(B3); winMove = true; }
            else if ((B1.Image.Tag == B3.Image.Tag) && (B1.Image.Tag == player) && (B2.Enabled)) { MakeMove(B2); winMove = true; }
            else if ((B2.Image.Tag == B3.Image.Tag) && (B2.Image.Tag == player) && (B1.Enabled)) { MakeMove(B1); winMove = true; }
            else if ((C1.Image.Tag == C2.Image.Tag) && (C1.Image.Tag == player) && (C3.Enabled)) { MakeMove(C3); winMove = true; }
            else if ((C1.Image.Tag == C3.Image.Tag) && (C1.Image.Tag == player) && (C2.Enabled)) { MakeMove(C2); winMove = true; }
            else if ((C2.Image.Tag == C3.Image.Tag) && (C2.Image.Tag == player) && (C1.Enabled)) { MakeMove(C1); winMove = true; }

            //pionowo
            else if ((A1.Image.Tag == B1.Image.Tag) && (A1.Image.Tag == player) && (C1.Enabled)) { MakeMove(C1); winMove = true; }
            else if ((A1.Image.Tag == C1.Image.Tag) && (A1.Image.Tag == player) && (B1.Enabled)) { MakeMove(B1); winMove = true; }
            else if ((C1.Image.Tag == B1.Image.Tag) && (C1.Image.Tag == player) && (A1.Enabled)) { MakeMove(A1); winMove = true; }
            else if ((A2.Image.Tag == B2.Image.Tag) && (A2.Image.Tag == player) && (C2.Enabled)) { MakeMove(C2); winMove = true; }
            else if ((A2.Image.Tag == C2.Image.Tag) && (A2.Image.Tag == player) && (B2.Enabled)) { MakeMove(B2); winMove = true; }
            else if ((C2.Image.Tag == B2.Image.Tag) && (C2.Image.Tag == player) && (A2.Enabled)) { MakeMove(A2); winMove = true; }
            else if ((A3.Image.Tag == B3.Image.Tag) && (A3.Image.Tag == player) && (C3.Enabled)) { MakeMove(C3); winMove = true; }
            else if ((A3.Image.Tag == C3.Image.Tag) && (A3.Image.Tag == player) && (B3.Enabled)) { MakeMove(B3); winMove = true; }
            else if ((C3.Image.Tag == B3.Image.Tag) && (C3.Image.Tag == player) && (A3.Enabled)) { MakeMove(A3); winMove = true; }

            //skośnie
            else if ((A1.Image.Tag == B2.Image.Tag) && (A1.Image.Tag == player) && (C3.Enabled)) { MakeMove(C3); winMove = true; }
            else if ((A1.Image.Tag == C3.Image.Tag) && (A1.Image.Tag == player) && (B2.Enabled)) { MakeMove(B2); winMove = true; }
            else if ((C3.Image.Tag == B2.Image.Tag) && (C3.Image.Tag == player) && (A1.Enabled)) { MakeMove(A1); winMove = true; }
            else if ((A3.Image.Tag == B2.Image.Tag) && (A3.Image.Tag == player) && (C1.Enabled)) { MakeMove(C1); winMove = true; }
            else if ((A3.Image.Tag == C1.Image.Tag) && (A3.Image.Tag == player) && (B2.Enabled)) { MakeMove(B2); winMove = true; }
            else if ((C1.Image.Tag == B2.Image.Tag) && (C1.Image.Tag == player) && (A3.Enabled)) { MakeMove(A3); winMove = true; }
        } // end CheckForWinMove

        public void AI_MakePerfectMove() // AI bezbłędnego ruchu
        {
            if (szPlayer == "o")
            {
                CheckForWinMove(szPlayer);
                if (!winMove)  CheckForWinMove(szAI);
            }
            else
            {
                CheckForWinMove(szAI);
                if (!winMove) CheckForWinMove(szPlayer);
            }

            if (!winMove)
            {
                switch(turnCount)
                {
                    case 0:
                        MakeMove(B2);
                        break;
                    case 1:
                        {
                            if (B2.Enabled)
                            {
                                if (A2.Image.Tag == szPlayer) MakeMove(C2);
                                else if (B1.Image.Tag == szPlayer) MakeMove(B3);
                                else if (B3.Image.Tag == szPlayer) MakeMove(B1);
                                else if (C2.Image.Tag == szPlayer) MakeMove(A2);
                                else MakeMove(B2);
                            }
                            else MakeMove(C1);
                            break;
                        }
                    case 3:
                        {
                            if (B2.Image.Tag == szPlayer && A3.Image.Tag == szPlayer) MakeMove(A1);
                            else if (A2.Image.Tag == szPlayer && B1.Image.Tag == szPlayer && A1.Enabled) MakeMove(A1);
                            else if (A2.Image.Tag == szPlayer && B3.Image.Tag == szPlayer && A3.Enabled) MakeMove(A3);
                            else if (B1.Image.Tag == szPlayer && C2.Image.Tag == szPlayer && C1.Enabled) MakeMove(C1);
                            else if (C2.Image.Tag == szPlayer && B3.Image.Tag == szPlayer && C3.Enabled) MakeMove(C3);
                            else MakeMove_Random();
                            break;
                        }
                    default:
                        MakeMove_Random();
                        break;
                }
            }
        } //end MakePerfectMove
    } // end class
}