using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace _4Vvrsto
{

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
        private int stolpec;
        private int vrstica;
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
            naVrsti.Visible = false;

        }

        /// <summary>
        /// Metoda, ki odstrani napise z zacetnega okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            naVrsti.Visible = true;
            naVrsti.Text = "Na vrsti je rumeni";
            naVrsti.ForeColor = Color.Yellow;
            gameBoardPanel.Invalidate(); 
        }

        /// <summary>
        /// Meotda, ki ponastavi igralno plosco
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gbReset_Click(object sender, EventArgs e)
        {
            // spraznemo igralno plosco
            plosca = new int[7, 6];
            naVrstiRumeni = true;
            konecIgre = false;
            naVrsti.Text = "Na vrsti je rumeni";
            naVrsti.ForeColor = Color.Yellow;
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
            naVrsti.Visible = false;
            naVrsti.Text = "Na vrsti je rumeni";
            naVrsti.ForeColor = Color.Yellow;
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
                int velikostCelice = 50;
                int razmik = 10;

                int sirinaPlosce = stStolpcev * velikostCelice;
                int visinaPlosce = stVrstic * velikostCelice;


                // narisemo mrezo (x koordinate)
                for (int stolpec = 0; stolpec <= stStolpcev; stolpec++)
                {
                    int x = stolpec * velikostCelice + razmik;
                    Rectangle rect = new Rectangle(x, razmik, 1, visinaPlosce);
                    g.FillRectangle(Brushes.White, rect);
                }

                // Narisemo mrezo (y koordinate)
                for (int vrstica = 0; vrstica <= stVrstic; vrstica++)
                {
                    int y = vrstica * velikostCelice + razmik;
                    Rectangle rect = new Rectangle(razmik, y, sirinaPlosce, 1);
                    g.FillRectangle(Brushes.White, rect);
                }
                // Narisemo kroge, ki jih igralca polozita
                for (int vrstica = 0; vrstica < stVrstic; vrstica++)
                {
                    for (int stolpec = 0; stolpec < stStolpcev; stolpec++)
                    {
                        int x = stolpec * velikostCelice + razmik;
                        int y = vrstica * velikostCelice + razmik;

                        if (plosca[stolpec, vrstica] == 1)
                            g.FillEllipse(Brushes.Yellow, x, y, velikostCelice, velikostCelice);
                        else if (plosca[stolpec, vrstica] == 2)
                            g.FillEllipse(Brushes.Red, x, y, velikostCelice, velikostCelice);
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
        private void gameBoardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            naVrsti.Visible = true;
            int stVrstic = plosca.GetLength(1);
            int stStolpcev = plosca.GetLength(0);
            int velikostCelice = 50;
            int razmik = 10;
            stolpec = (e.X - razmik) / velikostCelice;

            vrstica = KjeLahkoPolozim(stolpec);

            if (!(stolpec < 0 || stolpec > 6))
            {
                if (konecIgre)
                    return;

                if (stolpec >= 0 && stolpec < stStolpcev && vrstica >= 0 && vrstica < stVrstic && plosca[stolpec, vrstica] == 0)
                {
                    if (gbLahki_pritisnjen) //  igra z lahkim robotom
                    {
                        naVrsti.Visible = true;
                        naVrsti.Text = "Na vrsti je rumeni";

                        if (naVrstiRumeni) // rumen je na vrsti
                        {
                            plosca[stolpec, vrstica] = 1;
                        }
                        gameBoardPanel.Invalidate(); // Sprozimo Narisi metodo, da doda zeton

                        if (PreveriZmago(stolpec, vrstica))
                        {

                            MessageBox.Show("Rumeni je zmagal!");
                            konecIgre = true;
                            gbReset.Visible = true;
                            stZmagRumeni += 1;
                            zmageRumeni.Text = $"{stZmagRumeni}";
                            return; 
                        }

                        
                        GenerirajNakljucniMet();

                        gameBoardPanel.Invalidate(); // Sprozimo Narisi metodo, da doda zeton

                        if (PreveriZmago(stolpec, vrstica))
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
                        naVrsti.Visible = true;
                        naVrsti.Text = "Na vrsti je rumeni";
                        if (naVrstiRumeni) // rumen je na vrsti
                        {
                            plosca[stolpec, vrstica] = 1;  

                        }
                        gameBoardPanel.Invalidate(); // Sprozimo Narisi metodo, da doda zeton


                        if (PreveriZmago(stolpec, vrstica))
                        {
                            MessageBox.Show("Rumeni je zmagal!");
                            konecIgre = true;
                            gbReset.Visible = true;
                            stZmagRumeni += 1;
                            zmageRumeni.Text = $"{stZmagRumeni}";
                            return;
                        }


                        // preveri ali so tri rdeci v vrsti
                        int potezaS = -1;
                        int potezaV = -1;
                        
                        for (int v = 0; v < stVrstic; v++)
                        {
                            for (int s = 0; s< stStolpcev; s++)
                            {
                                if (v + 1 < stVrstic)
                                {
                                    if (plosca[s, v + 1] == 0)
                                    {
                                        continue;
                                    }
                                }
                                if (plosca[s, v] == 0)
                                {
                                    if (Zmagovalec(s, v) == 2)
                                    {
                                        potezaS = s;
                                        potezaV = v;
                                    }
                                }
                            }
                        }
                        
                        if (potezaS == -1 && potezaV == -1)
                        {

                            GenerirajNakljucniMet(); 
                        }
                        else
                        {
                            PosodobiRdece(potezaS, potezaV);
                        }
                        naVrsti.Text = "Na vrsti je rumeni";
                        naVrsti.ForeColor = Color.Yellow;
                        gameBoardPanel.Invalidate(); // Sprozimo Narisi metodo, da doda zeton
                    }


                    else // igra med dvema clovekoma
                    {
                        naVrsti.Visible = true;
                        naVrsti.Text = naVrstiRumeni ? "Na vrsti je rdeči" : "Na vrsti je rumeni";
                        naVrsti.ForeColor = naVrstiRumeni ? Color.Red: Color.Yellow;

                        plosca[stolpec, vrstica] = naVrstiRumeni ? 1 : 2;
                        gameBoardPanel.Invalidate(); // Sprozimo Narisi metodo, da doda zeton

                        if (PreveriZmago(stolpec, vrstica))
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
                            gbReset.Visible = true;
                        }
                        naVrstiRumeni = !naVrstiRumeni;
                    }

                }
            }
        }

        /// <summary>
        /// Metoda, ki odstrani napise z okna ob kliku na lahko tezavnost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gbLahki_MouseClick(object sender, MouseEventArgs e)
        {
            gbLahki_pritisnjen = true;
            naVrsti.Visible = true;
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

        /// <summary>
        /// Metoda, ki odstrani napise z okna ob kliku na tezka tezavnost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gbTezka_MouseClick(object sender, MouseEventArgs e)
        {
            
            gbTezki_pritisnjen = true;
            naVrsti.Visible = true;
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

        /// <summary>
        /// Generira nakljucen stolpec in ustrezno vrstico
        /// v igralno plosco ustavi 2 na ustrezno mesto
        /// </summary>
        private void GenerirajNakljucniMet()
        {
            naVrsti.Text = "Na vrsti je rumeni";
            naVrsti.ForeColor = Color.Yellow;
            int stStolpcev = plosca.GetLength(0);
            int stVrstic = plosca.GetLength(1);
            int velikostCelice = 50;
            int razmik = 10;


            // Generate random coordinates within the bounds of the game board
            Random random = new Random();
            stolpec = (random.Next(0, stStolpcev) * velikostCelice + razmik) / velikostCelice;

            vrstica = KjeLahkoPolozim(stolpec);
            if (vrstica == -1)
            {
                GenerirajNakljucniMet();
            }

            if (stolpec >= 0 && stolpec < stStolpcev && vrstica >= 0 && vrstica < stVrstic && plosca[stolpec, vrstica] == 0)
            {
                plosca[stolpec, vrstica] = 2;
            }
            

        }

        /// <summary>
        /// Vrne vrstico v katero lahko vrzemo kovanec v podanem stoplcu.
        /// Če vrstice ni mozno najti, vrne -1
        /// </summary>
        /// <param name="stolpec"></param>
        /// <returns></returns>
        private int KjeLahkoPolozim(int stolpec) 
        {
            if ((stolpec < 0 || stolpec > 6))
            { return -1; }
            int StVrstic = plosca.GetLength(1);

            for (int vrstica = StVrstic - 1; vrstica >= 0; vrstica--)
            {
                if (plosca[stolpec, vrstica] == 0)
                    return vrstica;
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

            // preverimo zmago v smeri desno_diag - levo_diag

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

            for (int vrstica = 0; vrstica < stVrstic; vrstica++)
            {
                for (int stolpec = 0; stolpec < stStolpcev; stolpec++)
                {
                    if (plosca[stolpec, vrstica] == 0)
                        return false;
                }
            }

            return true;

        }
        /// <summary>
        /// Preveri, ali je robot zmagal (v kateri koli smeri) z zetonom, ki ga je dodal nazadnje. 
        /// </summary>
        /// <param name="stolpec">Stolpec zadnjega položenega žetona</param>
        /// <param name="vrstica">Vrstica zadnjega položenega žetona</param>
        /// <returns></returns>
        public int Zmagovalec(int stolpec, int vrstica)
        {
            int[,] novaP = plosca.Clone() as int[,];
            novaP[stolpec, vrstica] = 2;
            int stStolpcev = 7;
            int stVrstic = 6;
            // Vrstice
            List<int> vrsta = new List<int>();
            for (int i = 0; i < stVrstic; i++)
            {
                for (int j = 0; j < stStolpcev; j++)
                {
                    vrsta.Add(novaP[j, i]);
                }
                if (Vsebuje4(2, vrsta))
                {
                    return 2;
                }
                vrsta = new List<int>();
            }
            

            // Stolpci
            for (int i = 0; i < stStolpcev; i++)
            {
                for (int j = 0; j < stVrstic; j++)
                {
                    vrsta.Add(novaP[i, j]);
                }

                if (Vsebuje4(2, vrsta))
                {
                    return 2;
                }
                vrsta = new List<int>();
            }

            // Desne diagonale
            for (int lin = 1; lin < stStolpcev + stVrstic - 1; lin++)
            {
                int zacetniStolpec = Math.Max(0, lin - stVrstic);
                List<int> elt = new List<int>() { lin, (stStolpcev - zacetniStolpec), stVrstic };
                int stElt = elt.Min();
                vrsta = new List<int>();
                for (int j = 0; j < stElt; j++)
                {
                    vrsta.Add(novaP[zacetniStolpec + j, Math.Min(stVrstic, lin) - j - 1]);
                }
                if (Vsebuje4(2, vrsta))
                {
                    return 2;
                }
            }

            // Leve diagonale
            for (int lin = 1; lin < stStolpcev + stVrstic; lin++)
            {
                int zacetniStolpec = Math.Max(0, stStolpcev - lin);
                List<int> elt = new List<int>() { lin, (stStolpcev - Math.Max(0, lin - stVrstic)), stVrstic }; 
                int stElt = elt.Min();
                vrsta = new List<int>();
                for (int j = 0; j < stElt; j++)
                {
                    vrsta.Add(novaP[zacetniStolpec + j, Math.Max(0, lin - stStolpcev) + j]);
                }
                if (Vsebuje4(2, vrsta))
                {
                    return 2;
                }
            }
            return 0; // Ni zmagovalca.
        }

        /// <summary>
        /// Preveri, ali so žetoni določene barve prisotni v dani vrstici.
        /// </summary>
        /// <param name="barva">Barva (igralec), ki jo preverjamo</param>
        /// <param name="vrsta">Seznam žetonov v vrstici</param>
        /// <returns>True, če ima dana barva štiri zaporedne žetone v vrstici, sicer false</returns>

        public bool Vsebuje4(int barva, List<int> vrsta)
        {
            int st = 0;
            if (vrsta.Count < 4)
            {
                return false;
            }
            for (int i = 0; i < vrsta.Count; i++)
            {

                if (vrsta[i] == 0 || vrsta[i] != barva)
                {
                    st = 0;
                }

                else if (vrsta[i] == barva)
                {
                    st++;
                }

                if (st == 4)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Posodobi igralno ploščo z rdečim žetonom na določenem stolpcu in vrstici.
        /// Preveri tudi, ali je rdeči igralec zmagal.
        /// </summary>
        /// <param name="stolpec">Stolpec, kamor je postavljen rdeči žeton</param>
        /// <param name="vrstica">Vrstica, kjer je postavljen rdeči žeton</param>

        private void PosodobiRdece(int stolpec, int vrstica)
        {
            plosca[stolpec, vrstica] = 2;
            gameBoardPanel.Invalidate();
            MessageBox.Show("Rdeči je zmagal!");
            konecIgre = true;
            gbReset.Visible = true;
            stZmagRdeci += 1;
            zmageRdec.Text = $"{stZmagRdeci}";
        }
    }
}

