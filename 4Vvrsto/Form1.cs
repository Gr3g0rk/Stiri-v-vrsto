using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4Vvrsto
{

    // TODO: Različna režavnost robota
    // Grafika izboljšava




    public partial class Form1 : Form
    {
        int[,] plosca = new int[7, 6];
        bool naVrstiRumeni = true;
        bool konecIgre = false;
        int stZmagRdeci = 0;
        int stZmagRumeni = 0;
        bool igraZaceta = false;
        bool gbLahki_pritisnjen = false;
        bool gbTezki_pritisnjen = false;
        private int col;
        private int row;
        public Form1()
        {
            InitializeComponent();

            napisZmage.Visible = false;
            zmageRdec.Visible = false;
            zmageRumeni.Visible = false;
            gbReset.Visible = false;
            gbLahki.Visible = false;
            gbTezka.Visible = false;
            napisTezavnost.Visible = false;

            //
            //gameBoardPanel.MouseClick += gameBoardPanel_MouseClick;
        }


        private void gbZacni_Click(object sender, MouseEventArgs e)
        {
            gbZacni.Visible = false;
            napisSVV.Visible = false;
            napisZmage.Visible = true;
            gbLahki_pritisnjen = false;
            zmageRdec.Visible = true;
            zmageRumeni.Visible = true;
            gbRobot.Visible = false;
            igraZaceta = true;
            gameBoardPanel.Invalidate(); // Trigger the Paint event to update the graphics
                                         // gameBoardPanel.Paint += Narisi;
        }


        private void gbReset_Click(object sender, EventArgs e)
        {
            // spraznemo igralno plosco
            plosca = new int[7, 6];
            naVrstiRumeni = true;
            konecIgre = false;
            gameBoardPanel.Invalidate();  // Osvezimo gameBoardPanel, da se novi krogi gor narisejo (klice metodo Narisi
            gbReset.Visible = false;



        }

        /// <summary>
        /// Spremenimo napise ob kliku na igro z robotom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gbRobot_Click(object sender, MouseEventArgs e)
        {

            gbZacni.Visible = false;
            napisSVV.Visible = false;
            napisZmage.Visible = true;
            zmageRdec.Visible = true;
            zmageRumeni.Visible = true;
            napisZmage.Visible = false;
            zmageRdec.Visible = false;
            zmageRumeni.Visible = false;
            gbRobot.Visible = false;
            gbTezka.Visible = true;
            gbLahki.Visible = true;
            napisTezavnost.Visible = true;


        }


        /// <summary>
        /// Narise mrezo in ustrezne kroge, ki jih igralca polozita
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Narisi(object sender, PaintEventArgs e)
        {
            if (igraZaceta)
            {
                Graphics g = e.Graphics;


                int stVrstic = plosca.GetLength(1);
                int stStolpcev = plosca.GetLength(0);
                int cellSize = 50;
                int padding = 10;

                int sirinaPlosce = stStolpcev * cellSize;
                int visinaPlosce = stVrstic * cellSize;


                // narisemo mrezo (x koordinate)
                for (int col = 0; col <= stStolpcev; col++)
                {
                    int x = col * cellSize + padding;
                    Rectangle rect = new Rectangle(x, padding, 1, visinaPlosce);
                    g.FillRectangle(Brushes.White, rect);
                }

                // Narisemo mrezo (y koordinate)
                for (int row = 0; row <= stVrstic; row++)
                {
                    int y = row * cellSize + padding;
                    Rectangle rect = new Rectangle(padding, y, sirinaPlosce, 1);
                    g.FillRectangle(Brushes.White, rect);
                }
                // Narisemo kroge, ki jih igralca polozita
                for (int row = 0; row < stVrstic; row++)
                {
                    for (int col = 0; col < stStolpcev; col++)
                    {
                        int x = col * cellSize + padding;
                        int y = row * cellSize + padding;

                        if (plosca[col, row] == 1)
                            g.FillEllipse(Brushes.Yellow, x, y, cellSize, cellSize);
                        else if (plosca[col, row] == 2)
                            g.FillEllipse(Brushes.Red, x, y, cellSize, cellSize);
                    }
                }
            }
        }


        /// <summary>
        /// metoda, ki se klice ob kliku na panel.
        /// Ce je igre konec, se metoda neha izvajat.
        /// Ce je prostor, na katerega je uporabnik pritisnil veljaven,
        /// se preveri ali je kdo zmagal, ali je igralna plosca polna in ali je igre konec.
        /// Ce se nic od tega ni zgodilo, se spremeni igralec, ki je na vrsti za postavitev zetona
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void gameBoardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            int stVrstic = plosca.GetLength(1);
            int stStolpcev = plosca.GetLength(0);
            int cellSize = 50;
            int padding = 10;
            col = (e.X - padding) / cellSize;
            row = KjeLahkoPolozim(col);

            if (!(col < 0 || col > 6))
            {
                if (konecIgre)
                    return;

                if (col >= 0 && col < stStolpcev && row >= 0 && row < stVrstic && plosca[col, row] == 0)
                {
                    if (gbLahki_pritisnjen) //  igra z robotom
                    {

                        if (naVrstiRumeni) // rumen je na vrsti
                        {
                            plosca[col, row] = 1;
                        }
                        gameBoardPanel.Invalidate(); // Sprozimo Narisi metodo, da doda zeton

                        if (PreveriZmago(col, row))
                        {
                            // ce naVrstiRumeni vrne True, je string Rumeni, ce ne je Rdeči

                            MessageBox.Show("Rumeni je zmagal!");
                            konecIgre = true;
                            gbReset.Visible = true;
                            stZmagRumeni += 1;
                            zmageRumeni.Text = $"{stZmagRumeni}";
                            return; 
                        }

                        // rdeci je na vrsti
                        //await Task.Delay(750);
                        GenerateRandomMove();

                        gameBoardPanel.Invalidate(); // Sprozimo Narisi metodo, da doda zeton

                        if (PreveriZmago(col, row))
                        {
                            // ce naVrstiRumeni vrne True, je string Rumeni, ce ne je Rdeči
                            MessageBox.Show("Rdeči je zmagal!");
                            konecIgre = true;
                            gbReset.Visible = true;
                            stZmagRdeci += 1;
                            zmageRdec.Text = $"{stZmagRdeci}";

                        }
                        else if (PloscaJePolna())
                        {
                            MessageBox.Show("Nihče ni zmagal.");
                            konecIgre = true;
                        }
                    }

                    // clovek in pametnejsi robot
                    else if (gbTezki_pritisnjen)
                    {
                        if (naVrstiRumeni) // rumen je na vrsti
                        {
                            plosca[col, row] = 1;
                        }
                        gameBoardPanel.Invalidate(); // Sprozimo Narisi metodo, da doda zeton

                        if (PreveriZmago(col, row))
                        {
                            MessageBox.Show("Rumeni je zmagal!");
                            konecIgre = true;
                            gbReset.Visible = true;
                            stZmagRumeni += 1;
                            zmageRumeni.Text = $"{stZmagRumeni}";
                            return;
                        }

                        // Check if there are three red coins in a row
                        if (CountRedCoinsInRow(col, row) >= 3)
                        {
                            UpdateRedCoinsSequence(); // Generate a smart move
                        }
                        else
                        {
                            GenerateRandomMove(); // Generate a random move
                        }

                        gameBoardPanel.Invalidate(); // Sprozimo Narisi metodo, da doda zeton
                    }


                    else // igra med dvema clovekoma
                    {

                        plosca[col, row] = naVrstiRumeni ? 1 : 2;
                        gameBoardPanel.Invalidate(); // Sprozimo Narisi metodo, da doda zeton

                        if (PreveriZmago(col, row))
                        {
                            // ce naVrstiRumeni vrne True, je string Rumeni, ce ne je Rdeči
                            string zmagovalec = naVrstiRumeni ? "Rumeni" : "Rdeči";
                            MessageBox.Show(zmagovalec + " je zmagal!");
                            konecIgre = true;
                            gbReset.Visible = true;
                            if (naVrstiRumeni)
                            {
                                stZmagRumeni += 1;
                                zmageRumeni.Text = $"{stZmagRumeni}";
                            }
                            else
                            {
                                stZmagRdeci += 1;
                                zmageRdec.Text = $"{stZmagRdeci}";
                            }
                        }
                        else if (PloscaJePolna())
                        {
                            MessageBox.Show("Nihče ni zmagal.");
                            konecIgre = true;
                        }
                        naVrstiRumeni = !naVrstiRumeni;
                    }

                }
            }
        }

        private void gbLahki_MouseClick(object sender, MouseEventArgs e)
        {
            gbLahki_pritisnjen = true;
            gbZacni.Visible = false;
            gbLahki.Visible = false;
            gbTezka.Visible = false;
            napisTezavnost.Visible = false;
            napisSVV.Visible = false;
            napisZmage.Visible = true;
            naVrstiRumeni = true;
            zmageRdec.Visible = true;
            zmageRumeni.Visible = true;
            gbRobot.Visible = false;
            igraZaceta = true;
            gameBoardPanel.Invalidate();

        }

        private void gbTezka_MouseClick(object sender, MouseEventArgs e)
        {
            gbLahki_pritisnjen = true;
            gbZacni.Visible = false;
            gbLahki.Visible = false;
            gbTezka.Visible = false;
            napisTezavnost.Visible = false;
            napisSVV.Visible = false;
            napisZmage.Visible = true;
            naVrstiRumeni = true;
            zmageRdec.Visible = true;
            zmageRumeni.Visible = true;
            gbRobot.Visible = false;
            igraZaceta = true;
            gameBoardPanel.Invalidate();

        }


        private void GenerateRandomMove()
        {
            
            int stStolpcev = plosca.GetLength(0);
            int stVrstic = plosca.GetLength(1);
            int cellSize = 50;
            int padding = 10;


            // Generate random coordinates within the bounds of the game board
            Random random = new Random();
            col = (random.Next(0, stStolpcev) * cellSize + padding) / cellSize;

            row = KjeLahkoPolozim(col);
            if (row == -1)
            {
                GenerateRandomMove();
            }

            if (col >= 0 && col < stStolpcev && row >= 0 && row < stVrstic && plosca[col, row] == 0)
            {
                plosca[col, row] = 2;
            }
        }

        /// <summary>
        /// Vrne vrstico v katero lahko vrzemo kovanec v podanem stoplcu.
        /// Če vrstice ni mozno najti, vrne -1
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        private int KjeLahkoPolozim(int col) // DARJO
        {
            if ((col < 0 || col > 6))
            { return -1; }
            int StVrstic = plosca.GetLength(1);

            for (int row = StVrstic - 1; row >= 0; row--)
            {
                if (plosca[col, row] == 0)
                    return row;
            }

            return -1;
        }

        /// <summary>
        /// Metoda preveri ali je nekdo zmagal igro ali ne. (Vrne true ali false)
        /// </summary>
        /// <param name="stolpec"></param>
        /// <param name="vrstica"></param>
        /// <returns></returns>
        private bool PreveriZmago(int stolpec, int vrstica)
        {
            int igralec = plosca[stolpec, vrstica];

            // Preverimo zmago v smeri levo-desno
            int st = 1;
            st += StejPonovitve(stolpec, vrstica, -1, 0, igralec);
            st += StejPonovitve(stolpec, vrstica, 1, 0, igralec);
            if (st >= 4)
                return true;

            // preverimo zmago v smeri gor-dol
            st = 1;
            st += StejPonovitve(stolpec, vrstica, 0, -1, igralec);
            st += StejPonovitve(stolpec, vrstica, 0, 1, igralec);
            if (st >= 4)
                return true;

            // preverimo zmago v smeri levo_diag - desno_diag
            st = 1;
            st += StejPonovitve(stolpec, vrstica, -1, -1, igralec);
            st += StejPonovitve(stolpec, vrstica, 1, 1, igralec);
            if (st >= 4)
                return true;

            st = 1;
            st += StejPonovitve(stolpec, vrstica, -1, 1, igralec);
            st += StejPonovitve(stolpec, vrstica, 1, -1, igralec);
            if (st >= 4)
                return true;

            return false;
        }


        /// <summary>
        /// metoda, ki preveri stevilo ponovitev rumenih/rdecih zetonov
        /// </summary>
        /// <param name="stolpec">stolpec, kamor je zeton postavljen</param>
        /// <param name="vrstica">vrstica, v katero je postavljen zeton</param>
        /// <param name="dx">sprememba x smeri (v katero smer bomo steli ponovitve)</param>
        /// <param name="dy">sprememba v y smeri (v katero smer bomo steli ponovitve)</param>
        /// <param name="igralec">kateri igralec je na vrsti (rdec ali rumen)</param>
        /// <returns></returns>
        private int StejPonovitve(int stolpec, int vrstica, int dx, int dy, int igralec)
        {
            int st = 0;
            int x = stolpec + dx;
            int y = vrstica + dy;

            while (x >= 0 && x < plosca.GetLength(0) && y >= 0 && y < plosca.GetLength(1) && plosca[x, y] == igralec)
            {
                st++;
                x += dx;
                y += dy;
            }

            return st;
        }

        /// <summary>
        /// metoda, ki preveri ali je igralna plosca polna ali ne
        /// </summary>
        /// <returns></returns>
        private bool PloscaJePolna()
        {
            int stVrstic = plosca.GetLength(1);
            int stStolpcev = plosca.GetLength(0);

            for (int row = 0; row < stVrstic; row++)
            {
                for (int col = 0; col < stStolpcev; col++)
                {
                    if (plosca[col, row] == 0)
                        return false;
                }
            }

            return true;

        }
        private int CountRedCoinsInRow(int col, int row)
        {
            int stStolpcev = plosca.GetLength(0);
            int igralec = 2; // Red player

            // Count horizontally to the left
            int count = 1;
            for (int c = col - 1; c >= 0 && plosca[c, row] == igralec; c--)
            {
                count++;
            }

            // Count horizontally to the right
            for (int c = col + 1; c < stStolpcev && plosca[c, row] == igralec; c++)
            {
                count++;
            }

            return count;
        }

        private void UpdateRedCoinsSequence()
        {
            int stVrstic = plosca.GetLength(1);
            int stStolpcev = plosca.GetLength(0);

            // Check horizontally
            for (int row = 0; row < stVrstic; row++)
            {
                for (int col = 0; col < stStolpcev - 3; col++)
                {
                    if (plosca[col, row] == 2 && plosca[col + 1, row] == 2 && plosca[col + 2, row] == 2 && plosca[col + 3, row] == 0)
                    {
                        plosca[col + 3, row] = 2;
                        gameBoardPanel.Invalidate(); // Update the game board with the new move
                        if (PreveriZmago(col + 3, row)) // Check if this move wins the game
                        {
                            MessageBox.Show("Rdeči je zmagal!"); // Red player wins
                            konecIgre = true;
                            gbReset.Visible = true;
                            stZmagRdeci += 1;
                            zmageRdec.Text = $"{stZmagRdeci}";
                        }
                        return;
                    }
                }
            }

            // Check vertically
            for (int col = 0; col < stStolpcev; col++)
            {
                for (int row = 0; row < stVrstic - 3; row++)
                {
                    if (plosca[col, row] == 2 && plosca[col, row + 1] == 2 && plosca[col, row + 2] == 2 && plosca[col, row + 3] == 0)
                    {
                        plosca[col, row + 3] = 2;
                        gameBoardPanel.Invalidate(); // Update the game board with the new move
                        if (PreveriZmago(col, row + 3)) // Check if this move wins the game
                        {
                            MessageBox.Show("Rdeči je zmagal!"); // Red player wins
                            konecIgre = true;
                            gbReset.Visible = true;
                            stZmagRdeci += 1;
                            zmageRdec.Text = $"{stZmagRdeci}";
                        }
                        return;
                    }
                }
            }

            // Check diagonally (from top-left to bottom-right)
            for (int col = 0; col < stStolpcev - 3; col++)
            {
                for (int row = 0; row < stVrstic - 3; row++)
                {
                    if (plosca[col, row] == 2 && plosca[col + 1, row + 1] == 2 && plosca[col + 2, row + 2] == 2 && plosca[col + 3, row + 3] == 0)
                    {
                        plosca[col + 3, row + 3] = 2;
                        gameBoardPanel.Invalidate(); // Update the game board with the new move
                        if (PreveriZmago(col + 3, row + 3)) // Check if this move wins the game
                        {
                            MessageBox.Show("Rdeči je zmagal!"); // Red player wins
                            konecIgre = true;
                            gbReset.Visible = true;
                            stZmagRdeci += 1;
                            zmageRdec.Text = $"{stZmagRdeci}";
                        }
                        return;
                    }
                }
            }

            // Check diagonally (from top-right to bottom-left)
            for (int col = 3; col < stStolpcev; col++)
            {
                for (int row = 0; row < stVrstic - 3; row++)
                {
                    if (plosca[col, row] == 2 && plosca[col - 1, row + 1] == 2 && plosca[col - 2, row + 2] == 2 && plosca[col - 3, row + 3] == 0)
                    {
                        plosca[col - 3, row + 3] = 2;
                        gameBoardPanel.Invalidate(); // Update the game board with the new move
                        if (PreveriZmago(col - 3, row + 3)) // Check if this move wins the game
                        {
                            MessageBox.Show("Rdeči je zmagal!"); // Red player wins
                            konecIgre = true;
                            gbReset.Visible = true;
                            stZmagRdeci += 1;
                            zmageRdec.Text = $"{stZmagRdeci}";
                        }
                        return;
                    }
                }
            }
        }
    }
}

