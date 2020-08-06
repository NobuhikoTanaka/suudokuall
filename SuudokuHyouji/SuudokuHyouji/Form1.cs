using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuudokuHyouji
{
    public partial class Form1 : Form
    {
        int[,,,] No= new int[Con.CalcMax, 10, 10, 10];
        int[,] rr = new int[11, 11];
        Boolean FlgSet;
        int Shin = 0;
        int[] Undo = new int[Con.CalcMax];
        int SShin = 0;
        int UShin;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string result = dt.ToString();

            label1.Text += result;
            
            get_No();
            Gamen();

            while (Cunt_Zelo() > 0)
            {
                FlgSet = false;

                for(int jj = 1; jj <= 9; jj++)
                {
                    TowD_Set(jj);
                }

                for (int jj = 1; jj <= 9; jj++)
                {
                    for (int kk = 1; kk <= 9; kk++)
                    {
                        Tate_Set( No[Shin, 0, jj, kk], kk);
                        Yoko_Set( No[Shin, 0, jj, kk], jj);
                        A_Set( No[Shin, 0, jj, kk], jj, kk);
                        B_Set( No[Shin, 0, jj, kk], jj, kk);
                        C_Set( No[Shin, 0, jj, kk], jj, kk);
                        D_Set( No[Shin, 0, jj, kk], jj, kk);
                        E_Set( No[Shin, 0, jj, kk], jj, kk);
                        F_Set( No[Shin, 0, jj, kk], jj, kk);
                        G_Set( No[Shin, 0, jj, kk], jj, kk);
                        H_Set( No[Shin, 0, jj, kk], jj, kk);
                        I_Set( No[Shin, 0, jj, kk], jj, kk);
                    }
                }

                for (int jj = 1; jj <= 9; jj++)
                {
                    for (int kk = 1; kk <= 9; kk++)
                    {
                        One_Kennsaku(jj, kk);
                    }
                }

                Set_No();
                // メッセージ・キューにあるWindowsメッセージをすべて処理する
                Application.DoEvents();

                if (FlgSet == false)
                {
                    kuuhaku_Kennsaku();
                }

                if (FlgSet == false)
                {
                    Undo[Shin] = 1;
                    if (Undo_kennsaku() == 0 )
                    {
                        label2.Text = "ギブアップ";
                        break;
                    }
                }

            }

            DateTime dte = DateTime.Now;
            string resulte = dte.ToString();
            label2.Text += resulte;

        }

        private void get_No()
        {
             No[Shin, 0, 1, 1] = int.Parse(textBox11.Text);
             No[Shin, 0, 1, 2] = int.Parse(textBox12.Text);
             No[Shin, 0, 1, 3] = int.Parse(textBox13.Text);
             No[Shin, 0, 1, 4] = int.Parse(textBox14.Text);
             No[Shin, 0, 1, 5] = int.Parse(textBox15.Text);
             No[Shin, 0, 1, 6] = int.Parse(textBox16.Text);
             No[Shin, 0, 1, 7] = int.Parse(textBox17.Text);
             No[Shin, 0, 1, 8] = int.Parse(textBox18.Text);
             No[Shin, 0, 1, 9] = int.Parse(textBox19.Text);

             No[Shin, 0, 2, 1] = int.Parse(textBox21.Text);
             No[Shin, 0, 2, 2] = int.Parse(textBox22.Text);
             No[Shin, 0, 2, 3] = int.Parse(textBox23.Text);
             No[Shin, 0, 2, 4] = int.Parse(textBox24.Text);
             No[Shin, 0, 2, 5] = int.Parse(textBox25.Text);
             No[Shin, 0, 2, 6] = int.Parse(textBox26.Text);
             No[Shin, 0, 2, 7] = int.Parse(textBox27.Text);
             No[Shin, 0, 2, 8] = int.Parse(textBox28.Text);
             No[Shin, 0, 2, 9] = int.Parse(textBox29.Text);

             No[Shin, 0, 3, 1] = int.Parse(textBox31.Text);
             No[Shin, 0, 3, 2] = int.Parse(textBox32.Text);
             No[Shin, 0, 3, 3] = int.Parse(textBox33.Text);
             No[Shin, 0, 3, 4] = int.Parse(textBox34.Text);
             No[Shin, 0, 3, 5] = int.Parse(textBox35.Text);
             No[Shin, 0, 3, 6] = int.Parse(textBox36.Text);
             No[Shin, 0, 3, 7] = int.Parse(textBox37.Text);
             No[Shin, 0, 3, 8] = int.Parse(textBox38.Text);
             No[Shin, 0, 3, 9] = int.Parse(textBox39.Text);

             No[Shin, 0, 4, 1] = int.Parse(textBox41.Text);
             No[Shin, 0, 4, 2] = int.Parse(textBox42.Text);
             No[Shin, 0, 4, 3] = int.Parse(textBox43.Text);
             No[Shin, 0, 4, 4] = int.Parse(textBox44.Text);
             No[Shin, 0, 4, 5] = int.Parse(textBox45.Text);
             No[Shin, 0, 4, 6] = int.Parse(textBox46.Text);
             No[Shin, 0, 4, 7] = int.Parse(textBox47.Text);
             No[Shin, 0, 4, 8] = int.Parse(textBox48.Text);
             No[Shin, 0, 4, 9] = int.Parse(textBox49.Text);

             No[Shin, 0, 5, 1] = int.Parse(textBox51.Text);
             No[Shin, 0, 5, 2] = int.Parse(textBox52.Text);
             No[Shin, 0, 5, 3] = int.Parse(textBox53.Text);
             No[Shin, 0, 5, 4] = int.Parse(textBox54.Text);
             No[Shin, 0, 5, 5] = int.Parse(textBox55.Text);
             No[Shin, 0, 5, 6] = int.Parse(textBox56.Text);
             No[Shin, 0, 5, 7] = int.Parse(textBox57.Text);
             No[Shin, 0, 5, 8] = int.Parse(textBox58.Text);
             No[Shin, 0, 5, 9] = int.Parse(textBox59.Text);

             No[Shin, 0, 6, 1] = int.Parse(textBox61.Text);
             No[Shin, 0, 6, 2] = int.Parse(textBox62.Text);
             No[Shin, 0, 6, 3] = int.Parse(textBox63.Text);
             No[Shin, 0, 6, 4] = int.Parse(textBox64.Text);
             No[Shin, 0, 6, 5] = int.Parse(textBox65.Text);
             No[Shin, 0, 6, 6] = int.Parse(textBox66.Text);
             No[Shin, 0, 6, 7] = int.Parse(textBox67.Text);
             No[Shin, 0, 6, 8] = int.Parse(textBox68.Text);
             No[Shin, 0, 6, 9] = int.Parse(textBox69.Text);

             No[Shin, 0, 7, 1] = int.Parse(textBox71.Text);
             No[Shin, 0, 7, 2] = int.Parse(textBox72.Text);
             No[Shin, 0, 7, 3] = int.Parse(textBox73.Text);
             No[Shin, 0, 7, 4] = int.Parse(textBox74.Text);
             No[Shin, 0, 7, 5] = int.Parse(textBox75.Text);
             No[Shin, 0, 7, 6] = int.Parse(textBox76.Text);
             No[Shin, 0, 7, 7] = int.Parse(textBox77.Text);
             No[Shin, 0, 7, 8] = int.Parse(textBox78.Text);
             No[Shin, 0, 7, 9] = int.Parse(textBox79.Text);

             No[Shin, 0, 8, 1] = int.Parse(textBox81.Text);
             No[Shin, 0, 8, 2] = int.Parse(textBox82.Text);
             No[Shin, 0, 8, 3] = int.Parse(textBox83.Text);
             No[Shin, 0, 8, 4] = int.Parse(textBox84.Text);
             No[Shin, 0, 8, 5] = int.Parse(textBox85.Text);
             No[Shin, 0, 8, 6] = int.Parse(textBox86.Text);
             No[Shin, 0, 8, 7] = int.Parse(textBox87.Text);
             No[Shin, 0, 8, 8] = int.Parse(textBox88.Text);
             No[Shin, 0, 8, 9] = int.Parse(textBox89.Text);

             No[Shin, 0, 9, 1] = int.Parse(textBox91.Text);
             No[Shin, 0, 9, 2] = int.Parse(textBox92.Text);
             No[Shin, 0, 9, 3] = int.Parse(textBox93.Text);
             No[Shin, 0, 9, 4] = int.Parse(textBox94.Text);
             No[Shin, 0, 9, 5] = int.Parse(textBox95.Text);
             No[Shin, 0, 9, 6] = int.Parse(textBox96.Text);
             No[Shin, 0, 9, 7] = int.Parse(textBox97.Text);
             No[Shin, 0, 9, 8] = int.Parse(textBox98.Text);
             No[Shin, 0, 9, 9] = int.Parse(textBox99.Text);

        }

        private void Set_No()
        {
            textBox11.Text =  No[Shin, 0, 1, 1].ToString();
            textBox12.Text =  No[Shin, 0, 1, 2].ToString();
            textBox13.Text =  No[Shin, 0, 1, 3].ToString();
            textBox14.Text =  No[Shin, 0, 1, 4].ToString();
            textBox15.Text =  No[Shin, 0, 1, 5].ToString();
            textBox16.Text =  No[Shin, 0, 1, 6].ToString();
            textBox17.Text =  No[Shin, 0, 1, 7].ToString();
            textBox18.Text =  No[Shin, 0, 1, 8].ToString();
            textBox19.Text =  No[Shin, 0, 1, 9].ToString();

            textBox21.Text =  No[Shin, 0, 2, 1].ToString();
            textBox22.Text =  No[Shin, 0, 2, 2].ToString();
            textBox23.Text =  No[Shin, 0, 2, 3].ToString();
            textBox24.Text =  No[Shin, 0, 2, 4].ToString();
            textBox25.Text =  No[Shin, 0, 2, 5].ToString();
            textBox26.Text =  No[Shin, 0, 2, 6].ToString();
            textBox27.Text =  No[Shin, 0, 2, 7].ToString();
            textBox28.Text =  No[Shin, 0, 2, 8].ToString();
            textBox29.Text =  No[Shin, 0, 2, 9].ToString();

            textBox31.Text =  No[Shin, 0, 3, 1].ToString();
            textBox32.Text =  No[Shin, 0, 3, 2].ToString();
            textBox33.Text =  No[Shin, 0, 3, 3].ToString();
            textBox34.Text =  No[Shin, 0, 3, 4].ToString();
            textBox35.Text =  No[Shin, 0, 3, 5].ToString();
            textBox36.Text =  No[Shin, 0, 3, 6].ToString();
            textBox37.Text =  No[Shin, 0, 3, 7].ToString();
            textBox38.Text =  No[Shin, 0, 3, 8].ToString();
            textBox39.Text =  No[Shin, 0, 3, 9].ToString();

            textBox41.Text =  No[Shin, 0, 4, 1].ToString();
            textBox42.Text =  No[Shin, 0, 4, 2].ToString();
            textBox43.Text =  No[Shin, 0, 4, 3].ToString();
            textBox44.Text =  No[Shin, 0, 4, 4].ToString();
            textBox45.Text =  No[Shin, 0, 4, 5].ToString();
            textBox46.Text =  No[Shin, 0, 4, 6].ToString();
            textBox47.Text =  No[Shin, 0, 4, 7].ToString();
            textBox48.Text =  No[Shin, 0, 4, 8].ToString();
            textBox49.Text =  No[Shin, 0, 4, 9].ToString();

            textBox51.Text =  No[Shin, 0, 5, 1].ToString();
            textBox52.Text =  No[Shin, 0, 5, 2].ToString();
            textBox53.Text =  No[Shin, 0, 5, 3].ToString();
            textBox54.Text =  No[Shin, 0, 5, 4].ToString();
            textBox55.Text =  No[Shin, 0, 5, 5].ToString();
            textBox56.Text =  No[Shin, 0, 5, 6].ToString();
            textBox57.Text =  No[Shin, 0, 5, 7].ToString();
            textBox58.Text =  No[Shin, 0, 5, 8].ToString();
            textBox59.Text =  No[Shin, 0, 5, 9].ToString();

            textBox61.Text =  No[Shin, 0, 6, 1].ToString();
            textBox62.Text =  No[Shin, 0, 6, 2].ToString();
            textBox63.Text =  No[Shin, 0, 6, 3].ToString();
            textBox64.Text =  No[Shin, 0, 6, 4].ToString();
            textBox65.Text =  No[Shin, 0, 6, 5].ToString();
            textBox66.Text =  No[Shin, 0, 6, 6].ToString();
            textBox67.Text =  No[Shin, 0, 6, 7].ToString();
            textBox68.Text =  No[Shin, 0, 6, 8].ToString();
            textBox69.Text =  No[Shin, 0, 6, 9].ToString();

            textBox71.Text =  No[Shin, 0, 7, 1].ToString();
            textBox72.Text =  No[Shin, 0, 7, 2].ToString();
            textBox73.Text =  No[Shin, 0, 7, 3].ToString();
            textBox74.Text =  No[Shin, 0, 7, 4].ToString();
            textBox75.Text =  No[Shin, 0, 7, 5].ToString();
            textBox76.Text =  No[Shin, 0, 7, 6].ToString();
            textBox77.Text =  No[Shin, 0, 7, 7].ToString();
            textBox78.Text =  No[Shin, 0, 7, 8].ToString();
            textBox79.Text =  No[Shin, 0, 7, 9].ToString();

            textBox81.Text =  No[Shin, 0, 8, 1].ToString();
            textBox82.Text =  No[Shin, 0, 8, 2].ToString();
            textBox83.Text =  No[Shin, 0, 8, 3].ToString();
            textBox84.Text =  No[Shin, 0, 8, 4].ToString();
            textBox85.Text =  No[Shin, 0, 8, 5].ToString();
            textBox86.Text =  No[Shin, 0, 8, 6].ToString();
            textBox87.Text =  No[Shin, 0, 8, 7].ToString();
            textBox88.Text =  No[Shin, 0, 8, 8].ToString();
            textBox89.Text =  No[Shin, 0, 8, 9].ToString();

            textBox91.Text =  No[Shin, 0, 9, 1].ToString();
            textBox92.Text =  No[Shin, 0, 9, 2].ToString();
            textBox93.Text =  No[Shin, 0, 9, 3].ToString();
            textBox94.Text =  No[Shin, 0, 9, 4].ToString();
            textBox95.Text =  No[Shin, 0, 9, 5].ToString();
            textBox96.Text =  No[Shin, 0, 9, 6].ToString();
            textBox97.Text =  No[Shin, 0, 9, 7].ToString();
            textBox98.Text =  No[Shin, 0, 9, 8].ToString();
            textBox99.Text =  No[Shin, 0, 9, 9].ToString();

        }

        private void Gamen()
        {
            Set_Color(textBox11);
            Set_Color(textBox12);
            Set_Color(textBox13);
            Set_Color(textBox14);
            Set_Color(textBox15);
            Set_Color(textBox16);
            Set_Color(textBox17);
            Set_Color(textBox18);
            Set_Color(textBox19);

            Set_Color(textBox21);
            Set_Color(textBox22);
            Set_Color(textBox23);
            Set_Color(textBox24);
            Set_Color(textBox25);
            Set_Color(textBox26);
            Set_Color(textBox27);
            Set_Color(textBox28);
            Set_Color(textBox29);

            Set_Color(textBox31);
            Set_Color(textBox32);
            Set_Color(textBox33);
            Set_Color(textBox34);
            Set_Color(textBox35);
            Set_Color(textBox36);
            Set_Color(textBox37);
            Set_Color(textBox38);
            Set_Color(textBox39);

            Set_Color(textBox41);
            Set_Color(textBox42);
            Set_Color(textBox43);
            Set_Color(textBox44);
            Set_Color(textBox45);
            Set_Color(textBox46);
            Set_Color(textBox47);
            Set_Color(textBox48);
            Set_Color(textBox49);

            Set_Color(textBox51);
            Set_Color(textBox52);
            Set_Color(textBox53);
            Set_Color(textBox54);
            Set_Color(textBox55);
            Set_Color(textBox56);
            Set_Color(textBox57);
            Set_Color(textBox58);
            Set_Color(textBox59);

            Set_Color(textBox61);
            Set_Color(textBox62);
            Set_Color(textBox63);
            Set_Color(textBox64);
            Set_Color(textBox65);
            Set_Color(textBox66);
            Set_Color(textBox67);
            Set_Color(textBox68);
            Set_Color(textBox69);

            Set_Color(textBox71);
            Set_Color(textBox72);
            Set_Color(textBox73);
            Set_Color(textBox74);
            Set_Color(textBox75);
            Set_Color(textBox76);
            Set_Color(textBox77);
            Set_Color(textBox78);
            Set_Color(textBox79);

            Set_Color(textBox81);
            Set_Color(textBox82);
            Set_Color(textBox83);
            Set_Color(textBox84);
            Set_Color(textBox85);
            Set_Color(textBox86);
            Set_Color(textBox87);
            Set_Color(textBox88);
            Set_Color(textBox89);

            Set_Color(textBox91);
            Set_Color(textBox92);
            Set_Color(textBox93);
            Set_Color(textBox94);
            Set_Color(textBox95);
            Set_Color(textBox96);
            Set_Color(textBox97);
            Set_Color(textBox98);
            Set_Color(textBox99);

        }

        //赤くするのサブルーチン
        private void Set_Color(TextBox textBox)
        {
            if (textBox.Text != "0")
            {
                textBox.ForeColor = Color.Red;
            }
        }


        private void TowD_Set(int mm)
        {
            Boolean SFlg = true;

            while (SFlg == true)
            {
                SFlg = false;

                for (int i = 1; i <= 9; i++)
                {
                    for (int j = 1; j <= 9; j++)
                    {
                        if ( No[Shin, 0, i, j] == 0)
                        {
                            rr[i, j] = 0;
                        }
                        else
                        {
                            rr[i, j] = 1;
                        }
                    }
                }

                for (int i1 = 1; i1 <= 9; i1++)
                {
                    for (int j1 = 1; j1 <= 9; j1++)
                    {
                        if ( No[Shin, 0, i1, j1] == mm)
                        {
                            Tate_Set(j1);
                            Yoko_Set(i1);
                            A_Set(i1, j1);
                            B_Set(i1, j1);
                            C_Set(i1, j1);
                            D_Set(i1, j1);
                            E_Set(i1, j1);
                            F_Set(i1, j1);
                            G_Set(i1, j1);
                            H_Set(i1, j1);
                            I_Set(i1, j1);
                        }
                    }
                }

                for (int jj = 1; jj <= 9; jj++)
                {
                    for (int kk = 1; kk <= 9; kk++)
                    {
                        int[] ss = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                        if(rr[jj,kk] != 0)
                        {
                            continue;
                        }

                        ss[0] = A_Cnt(jj, kk);
                        ss[1] = B_Cnt(jj, kk);
                        ss[2] = C_Cnt(jj, kk);
                        ss[3] = D_Cnt(jj, kk);
                        ss[4] = E_Cnt(jj, kk);
                        ss[5] = F_Cnt(jj, kk);
                        ss[6] = G_Cnt(jj, kk);
                        ss[7] = H_Cnt(jj, kk);
                        ss[8] = I_Cnt(jj, kk);

                        if (ss[0] == 1 || ss[1] == 1 || ss[2] == 1 ||
                            ss[3] == 1 || ss[4] == 1 || ss[5] == 1 ||
                            ss[6] == 1 || ss[7] == 1 || ss[8] == 1)
                        {
                             No[Shin, 0, jj, kk] = mm;
                            rr[jj, kk] = 1;
                            FlgSet = true;
                            SFlg = true;
                            Set_No();
                            break;
                        }
                    }

                    if (SFlg == true)
                    {
                        break;
                    }
                }
            }

            return;
        }

        private void Tate_Set(int mm, int j)
        {
            if(mm == 0)
            {
                return;
            }

            for( int i = 1; i <= 9; i++ )
            {
                 No[Shin, mm, i, j] = 1;
            }

            return;
        }

        private void Yoko_Set(int mm, int j)
        {
            if (mm == 0)
            {
                return;
            }

            for (int i = 1; i <= 9; i++)
            {
                 No[Shin, mm, j, i] = 1;
            }

            return;

        }

        private void A_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 1, 1 }, { 1, 2 }, { 1, 3 }, { 2, 1 }, { 2, 2 }, { 2, 3 }, { 3, 1 }, { 3, 2 }, { 3, 3 } };

            if (mm == 0)
            {
                return;
            }

            if(kk > 3 || jj > 3 )
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                 No[Shin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }


            return;
        }

        private void B_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 4, 1 }, { 4, 2 }, { 4, 3 }, { 5, 1 }, { 5, 2 }, { 5, 3 }, { 6, 1 }, { 6, 2 }, { 6, 3 } };

            if (mm == 0)
            {
                return;
            }

            if (kk < 4 || kk > 6 || jj > 3)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                 No[Shin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void C_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 7, 1 }, { 7, 2 }, { 7, 3 }, { 8, 1 }, { 8, 2 }, { 8, 3 }, { 9, 1 }, { 9, 2 }, { 9, 3 } };

            if (mm == 0)
            {
                return;
            }

            if (kk < 7  || jj > 3)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                 No[Shin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void D_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 1, 4 }, { 1, 5 }, { 1, 6 }, { 2, 4 }, { 2, 5 }, { 2, 6 }, { 3, 4 }, { 3, 5 }, { 3, 6 } };

            if (mm == 0)
            {
                return;
            }

            if (kk > 3 || jj < 4 ||　jj > 6)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                 No[Shin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        private void E_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 4, 4 }, { 4, 5 }, { 4, 6 }, { 5, 4 }, { 5, 5 }, { 5, 6 }, { 6, 4 }, { 6, 5 }, { 6, 6 } };

            if (mm == 0)
            {
                return;
            }

            if (kk < 4 || kk > 6 || jj < 4 || jj > 6)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                 No[Shin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void F_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 7, 4 }, { 7, 5 }, { 7, 6 }, { 8, 4 }, { 8, 5 }, { 8, 6 }, { 9, 4 }, { 9, 5 }, { 9, 6 } };

            if (mm == 0)
            {
                return;
            }

            if (kk < 7 || jj < 4 || jj > 6)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                 No[Shin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void G_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 1, 7 }, { 1, 8 }, { 1, 9 }, { 2, 7 }, { 2, 8 }, { 2, 9 }, { 3, 7 }, { 3, 8 }, { 3, 9 } };

            if (mm == 0)
            {
                return;
            }

            if (kk > 3 || jj < 7)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                 No[Shin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        private void H_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 4, 7 }, { 4, 8 }, { 4, 9 }, { 5, 7 }, { 5, 8 }, { 5, 9 }, { 6, 7 }, { 6, 8 }, { 6, 9 } };

            if (mm == 0)
            {
                return;
            }

            if (kk < 4 || kk > 6 || jj < 7)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                 No[Shin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void I_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 7, 7 }, { 7, 8 }, { 7, 9 }, { 8, 7 }, { 8, 8 }, { 8, 9 }, { 9, 7 }, { 9, 8 }, { 9, 9 } };

            if (mm == 0)
            {
                return;
            }

            if (kk < 7 || jj < 7)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                 No[Shin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        private void Tate_Set(int j)
        {
            for (int i = 1; i <= 9; i++)
            {
                rr[i, j] = 1;
            }

            return;
        }

        private void Yoko_Set(int j)
        {
            for (int i = 1; i <= 9; i++)
            {
                rr[j, i] = 1;
            }

            return;

        }

        private void A_Set(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 1, 1 }, { 1, 2 }, { 1, 3 }, { 2, 1 }, { 2, 2 }, { 2, 3 }, { 3, 1 }, { 3, 2 }, { 3, 3 } };

            if (kk > 3 || jj > 3)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                rr[gloup[i, 0], gloup[i, 1]] = 1;
            }


            return;
        }

        private void B_Set(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 4, 1 }, { 4, 2 }, { 4, 3 }, { 5, 1 }, { 5, 2 }, { 5, 3 }, { 6, 1 }, { 6, 2 }, { 6, 3 } };

            if (kk < 4 || kk > 6 || jj > 3)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                rr[gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void C_Set(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 7, 1 }, { 7, 2 }, { 7, 3 }, { 8, 1 }, { 8, 2 }, { 8, 3 }, { 9, 1 }, { 9, 2 }, { 9, 3 } };

            if (kk < 7 || jj > 3)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                rr[gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void D_Set(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 1, 4 }, { 1, 5 }, { 1, 6 }, { 2, 4 }, { 2, 5 }, { 2, 6 }, { 3, 4 }, { 3, 5 }, { 3, 6 } };

            if (kk > 3 || jj < 4 || jj > 6)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                rr[gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        private void E_Set(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 4, 4 }, { 4, 5 }, { 4, 6 }, { 5, 4 }, { 5, 5 }, { 5, 6 }, { 6, 4 }, { 6, 5 }, { 6, 6 } };

            if (kk < 4 || kk > 6 || jj < 4 || jj > 6)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                rr[gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void F_Set(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 7, 4 }, { 7, 5 }, { 7, 6 }, { 8, 4 }, { 8, 5 }, { 8, 6 }, { 9, 4 }, { 9, 5 }, { 9, 6 } };

            if (kk < 7 || jj < 4 || jj > 6)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                rr[gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void G_Set(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 1, 7 }, { 1, 8 }, { 1, 9 }, { 2, 7 }, { 2, 8 }, { 2, 9 }, { 3, 7 }, { 3, 8 }, { 3, 9 } };

            if (kk > 3 || jj < 7)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                rr[gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        private void H_Set(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 4, 7 }, { 4, 8 }, { 4, 9 }, { 5, 7 }, { 5, 8 }, { 5, 9 }, { 6, 7 }, { 6, 8 }, { 6, 9 } };

            if (kk < 4 || kk > 6 || jj < 7)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                rr[gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void I_Set(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 7, 7 }, { 7, 8 }, { 7, 9 }, { 8, 7 }, { 8, 8 }, { 8, 9 }, { 9, 7 }, { 9, 8 }, { 9, 9 } };

            if (kk < 7 || jj < 7)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                rr[gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        private int A_Cnt(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 1, 1 }, { 1, 2 }, { 1, 3 }, { 2, 1 }, { 2, 2 }, { 2, 3 }, { 3, 1 }, { 3, 2 }, { 3, 3 } };
            int Rc = 0;

            if (kk > 3 || jj > 3)
            {
                return Rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (rr[gloup[i, 0], gloup[i, 1]] == 0)
                {
                    Rc++;
                }
            }


            return Rc;
        }

        private int B_Cnt(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 4, 1 }, { 4, 2 }, { 4, 3 }, { 5, 1 }, { 5, 2 }, { 5, 3 }, { 6, 1 }, { 6, 2 }, { 6, 3 } };
            int Rc = 0;

            if (kk < 4 || kk > 6 || jj > 3)
            {
                return Rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (rr[gloup[i, 0], gloup[i, 1]] == 0)
                {
                    Rc++;
                }
            }

            return Rc;
        }
        private int C_Cnt(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 7, 1 }, { 7, 2 }, { 7, 3 }, { 8, 1 }, { 8, 2 }, { 8, 3 }, { 9, 1 }, { 9, 2 }, { 9, 3 } };
            int Rc = 0;

            if (kk < 7 || jj > 3)
            {
                return Rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if(rr[gloup[i, 0], gloup[i, 1]] == 0)
                {
                    Rc++;
                }
            }

            return Rc;
        }
        private int D_Cnt(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 1, 4 }, { 1, 5 }, { 1, 6 }, { 2, 4 }, { 2, 5 }, { 2, 6 }, { 3, 4 }, { 3, 5 }, { 3, 6 } };
            int Rc = 0;

            if (kk > 3 || jj < 4 || jj > 6)
            {
                return Rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (rr[gloup[i, 0], gloup[i, 1]] == 0)
                {
                    Rc++;
                }
            }

            return Rc;
        }

        private int E_Cnt(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 4, 4 }, { 4, 5 }, { 4, 6 }, { 5, 4 }, { 5, 5 }, { 5, 6 }, { 6, 4 }, { 6, 5 }, { 6, 6 } };
            int Rc = 0;

            if (kk < 4 || kk > 6 || jj < 4 || jj > 6)
            {
                return Rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (rr[gloup[i, 0], gloup[i, 1]] == 0)
                {
                    Rc++;
                }
            }

            return Rc;
        }
        private int F_Cnt(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 7, 4 }, { 7, 5 }, { 7, 6 }, { 8, 4 }, { 8, 5 }, { 8, 6 }, { 9, 4 }, { 9, 5 }, { 9, 6 } };
            int Rc = 0;

            if (kk < 7 || jj < 4 || jj > 6)
            {
                return Rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (rr[gloup[i, 0], gloup[i, 1]] == 0)
                {
                    Rc++;
                }
            }

            return Rc;
        }
        private int G_Cnt(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 1, 7 }, { 1, 8 }, { 1, 9 }, { 2, 7 }, { 2, 8 }, { 2, 9 }, { 3, 7 }, { 3, 8 }, { 3, 9 } };
            int Rc = 0;

            if (kk > 3 || jj < 7)
            {
                return Rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (rr[gloup[i, 0], gloup[i, 1]] == 0)
                {
                    Rc++;
                }
            }

            return Rc;
        }

        private int H_Cnt(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 4, 7 }, { 4, 8 }, { 4, 9 }, { 5, 7 }, { 5, 8 }, { 5, 9 }, { 6, 7 }, { 6, 8 }, { 6, 9 } };
            int Rc = 0;

            if (kk < 4 || kk > 6 || jj < 7)
            {
                return Rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (rr[gloup[i, 0], gloup[i, 1]] == 0)
                {
                    Rc++;
                }
            }

            return Rc;
        }
        private int I_Cnt(int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 7, 7 }, { 7, 8 }, { 7, 9 }, { 8, 7 }, { 8, 8 }, { 8, 9 }, { 9, 7 }, { 9, 8 }, { 9, 9 } };
            int Rc = 0;

            if (kk < 7 || jj < 7)
            {
                return Rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (rr[gloup[i, 0], gloup[i, 1]] == 0)
                {
                    Rc++;
                }
            }

            return Rc;
        }

        private int Cunt_Zelo()
        {
            int Rc = 0;

            for( int i = 1; i <= 9; i++)
            {
                for( int j = 1; j <= 9; j++)
                {
                    if ( No[Shin, 0, i,j] == 0)
                    {
                        Rc++;
                    }
                }
            }

            return Rc;
        }

        private void One_Kennsaku(int jj, int kk)
        {
            int Cnt = 0;
            int work = 0;

            if ( No[Shin, 0, jj, kk] != 0)
            {
                return;
            }

            for (int i = 1; i <= 9; i++)
            {
                if ( No[Shin, i, jj, kk] == 0)
                {
                    Cnt++;
                    work = i;
                }
            }

            if (Cnt == 1)
            {
                 No[Shin, 0, jj, kk] = work;
                FlgSet = true;
            }

            return;
        }

        private int kuuhaku_Kennsaku()
        {
            int rc = 0;
            int Cnt = 0;
            int SetCnt = 0;
            int[] work = new int [10];
            Boolean SetFlag = false;

            for (int jj = 1; jj <= 9; jj++)
            {
                for (int kk = 1; kk <= 9; kk++)
                {
                    if (No[Shin, 0, jj, kk] != 0)
                    {
                        SetCnt++;
                        if (SetCnt >= 81)
                        {
                            FlgSet = true;
                            return rc;
                        }
                        continue;
                    }

                    Cnt = 0;
                    for (int i = 1; i <= 9; i++)
                    {
                        if (No[Shin, i, jj, kk] == 0)
                        {
                            work[Cnt++] = i;
                        }
                    }

                    if (Cnt == 0)
                    {
                        SetFlag = true;
                        break;
                    }

                    if (SShin + 1 >= Con.CalcMax)
                    {
                        return rc;
                    }

                    CopyMap(SShin + 1, Shin);
                    No[SShin + 1, 0, jj, kk] = work[0];
                    UShin = SShin + 1;
                    SShin++;
                    rc = 100;

                    while(rc == 100)
                    {
                        rc = 0;
                        for (int j5 = 1; j5 <= 9; j5++)
                        {
                            rc = STowD_Set(j5);
                            if (rc == 100)
                            {
                                if (UCunt_Zelo() == 0)
                                {
                                    Shin = UShin;
                                    FlgSet = true;
                                    return 0;
                                }

                                USet_No();
                                if (No[UShin, 0, jj, kk] != 0)
                                {
                                    continue;
                                }

                                Cnt = 0;
                                for (int i = 1; i <= 9; i++)
                                {
                                    if (No[UShin, i, jj, kk] == 0)
                                    {
                                        work[Cnt++] = i;
                                    }
                                }

                                if (Cnt == 0)
                                {
                                    SetFlag = true;
                                    break;
                                }

                                if (SShin + 1 >= Con.CalcMax)
                                {
                                    return rc;
                                }

                                CopyMap(SShin + 1, UShin);
                                No[SShin + 1, 0, jj, kk] = work[0];
                                UShin = SShin + 1;
                                SShin++;
                                break;
                            }
                        }

                        // メッセージ・キューにあるWindowsメッセージをすべて処理する
                        Application.DoEvents();

                    }

                }

                if (SetFlag == true)
                {
                    break;
                }

            }

            FlgSet = true;
            Undo[Shin] = 1;
            Shin = SShin;

            return rc;
        }

        private int STowD_Set(int mm)
        {
            int rc = 0;

            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    if (No[UShin, 0, i, j] == 0)
                    {
                        rr[i, j] = 0;
                    }
                    else
                    {
                        rr[i, j] = 1;
                    }
                }
            }

            for (int i1 = 1; i1 <= 9; i1++)
            {
                for (int j1 = 1; j1 <= 9; j1++)
                {
                    if (No[UShin, 0, i1, j1] == mm)
                    {
                        Tate_SSet(mm, j1);
                        Yoko_SSet(mm, i1);
                        A_SSet(mm, i1, j1);
                        B_SSet(mm, i1, j1);
                        C_SSet(mm, i1, j1);
                        D_SSet(mm, i1, j1);
                        E_SSet(mm, i1, j1);
                        F_SSet(mm, i1, j1);
                        G_SSet(mm, i1, j1);
                        H_SSet(mm, i1, j1);
                        I_SSet(mm, i1, j1);
                    }
                }
            }

            for (int jj = 1; jj <= 9; jj++)
            {
                for (int kk = 1; kk <= 9; kk++)
                {
                    int[] ss = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                    if (rr[jj, kk] != 0)
                    {
                        continue;
                    }

                    ss[0] = A_Cnt(jj, kk);
                    ss[1] = B_Cnt(jj, kk);
                    ss[2] = C_Cnt(jj, kk);
                    ss[3] = D_Cnt(jj, kk);
                    ss[4] = E_Cnt(jj, kk);
                    ss[5] = F_Cnt(jj, kk);
                    ss[6] = G_Cnt(jj, kk);
                    ss[7] = H_Cnt(jj, kk);
                    ss[8] = I_Cnt(jj, kk);

                    if (ss[0] == 1 || ss[1] == 1 || ss[2] == 1 ||
                        ss[3] == 1 || ss[4] == 1 || ss[5] == 1 ||
                        ss[6] == 1 || ss[7] == 1 || ss[8] == 1)
                    {
                        No[UShin, 0, jj, kk] = mm;
                        rr[jj, kk] = 1;
                        rc = 100;
                        return rc;
                    }
                }

            }

            return rc;
        }

        private void Tate_SSet(int mm, int j)
        {
            if (mm == 0)
            {
                return;
            }

            for (int i = 1; i <= 9; i++)
            {
                No[UShin, mm, i, j] = 1;
            }

            return;
        }

        private void Yoko_SSet(int mm, int j)
        {
            if (mm == 0)
            {
                return;
            }

            for (int i = 1; i <= 9; i++)
            {
                No[UShin, mm, j, i] = 1;
            }

            return;

        }

        private void A_SSet(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 1, 1 }, { 1, 2 }, { 1, 3 }, { 2, 1 }, { 2, 2 }, { 2, 3 }, { 3, 1 }, { 3, 2 }, { 3, 3 } };

            if (mm == 0)
            {
                return;
            }

            if (kk > 3 || jj > 3)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                No[UShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }


            return;
        }

        private void B_SSet(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 4, 1 }, { 4, 2 }, { 4, 3 }, { 5, 1 }, { 5, 2 }, { 5, 3 }, { 6, 1 }, { 6, 2 }, { 6, 3 } };

            if (mm == 0)
            {
                return;
            }

            if (kk < 4 || kk > 6 || jj > 3)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                No[UShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void C_SSet(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 7, 1 }, { 7, 2 }, { 7, 3 }, { 8, 1 }, { 8, 2 }, { 8, 3 }, { 9, 1 }, { 9, 2 }, { 9, 3 } };

            if (mm == 0)
            {
                return;
            }

            if (kk < 7 || jj > 3)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                No[UShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void D_SSet(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 1, 4 }, { 1, 5 }, { 1, 6 }, { 2, 4 }, { 2, 5 }, { 2, 6 }, { 3, 4 }, { 3, 5 }, { 3, 6 } };

            if (mm == 0)
            {
                return;
            }

            if (kk > 3 || jj < 4 || jj > 6)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                No[UShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        private void E_SSet(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 4, 4 }, { 4, 5 }, { 4, 6 }, { 5, 4 }, { 5, 5 }, { 5, 6 }, { 6, 4 }, { 6, 5 }, { 6, 6 } };

            if (mm == 0)
            {
                return;
            }

            if (kk < 4 || kk > 6 || jj < 4 || jj > 6)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                No[UShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void F_SSet(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 7, 4 }, { 7, 5 }, { 7, 6 }, { 8, 4 }, { 8, 5 }, { 8, 6 }, { 9, 4 }, { 9, 5 }, { 9, 6 } };

            if (mm == 0)
            {
                return;
            }

            if (kk < 7 || jj < 4 || jj > 6)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                No[UShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void G_SSet(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 1, 7 }, { 1, 8 }, { 1, 9 }, { 2, 7 }, { 2, 8 }, { 2, 9 }, { 3, 7 }, { 3, 8 }, { 3, 9 } };

            if (mm == 0)
            {
                return;
            }

            if (kk > 3 || jj < 7)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                No[UShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        private void H_SSet(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 4, 7 }, { 4, 8 }, { 4, 9 }, { 5, 7 }, { 5, 8 }, { 5, 9 }, { 6, 7 }, { 6, 8 }, { 6, 9 } };

            if (mm == 0)
            {
                return;
            }

            if (kk < 4 || kk > 6 || jj < 7)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                No[UShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void I_SSet(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 7, 7 }, { 7, 8 }, { 7, 9 }, { 8, 7 }, { 8, 8 }, { 8, 9 }, { 9, 7 }, { 9, 8 }, { 9, 9 } };

            if (mm == 0)
            {
                return;
            }

            if (kk < 7 || jj < 7)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                No[UShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        private void USet_No()
        {
            textBox11.Text = No[UShin, 0, 1, 1].ToString();
            textBox12.Text = No[UShin, 0, 1, 2].ToString();
            textBox13.Text = No[UShin, 0, 1, 3].ToString();
            textBox14.Text = No[UShin, 0, 1, 4].ToString();
            textBox15.Text = No[UShin, 0, 1, 5].ToString();
            textBox16.Text = No[UShin, 0, 1, 6].ToString();
            textBox17.Text = No[UShin, 0, 1, 7].ToString();
            textBox18.Text = No[UShin, 0, 1, 8].ToString();
            textBox19.Text = No[UShin, 0, 1, 9].ToString();

            textBox21.Text = No[UShin, 0, 2, 1].ToString();
            textBox22.Text = No[UShin, 0, 2, 2].ToString();
            textBox23.Text = No[UShin, 0, 2, 3].ToString();
            textBox24.Text = No[UShin, 0, 2, 4].ToString();
            textBox25.Text = No[UShin, 0, 2, 5].ToString();
            textBox26.Text = No[UShin, 0, 2, 6].ToString();
            textBox27.Text = No[UShin, 0, 2, 7].ToString();
            textBox28.Text = No[UShin, 0, 2, 8].ToString();
            textBox29.Text = No[UShin, 0, 2, 9].ToString();

            textBox31.Text = No[UShin, 0, 3, 1].ToString();
            textBox32.Text = No[UShin, 0, 3, 2].ToString();
            textBox33.Text = No[UShin, 0, 3, 3].ToString();
            textBox34.Text = No[UShin, 0, 3, 4].ToString();
            textBox35.Text = No[UShin, 0, 3, 5].ToString();
            textBox36.Text = No[UShin, 0, 3, 6].ToString();
            textBox37.Text = No[UShin, 0, 3, 7].ToString();
            textBox38.Text = No[UShin, 0, 3, 8].ToString();
            textBox39.Text = No[UShin, 0, 3, 9].ToString();

            textBox41.Text = No[UShin, 0, 4, 1].ToString();
            textBox42.Text = No[UShin, 0, 4, 2].ToString();
            textBox43.Text = No[UShin, 0, 4, 3].ToString();
            textBox44.Text = No[UShin, 0, 4, 4].ToString();
            textBox45.Text = No[UShin, 0, 4, 5].ToString();
            textBox46.Text = No[UShin, 0, 4, 6].ToString();
            textBox47.Text = No[UShin, 0, 4, 7].ToString();
            textBox48.Text = No[UShin, 0, 4, 8].ToString();
            textBox49.Text = No[UShin, 0, 4, 9].ToString();

            textBox51.Text = No[UShin, 0, 5, 1].ToString();
            textBox52.Text = No[UShin, 0, 5, 2].ToString();
            textBox53.Text = No[UShin, 0, 5, 3].ToString();
            textBox54.Text = No[UShin, 0, 5, 4].ToString();
            textBox55.Text = No[UShin, 0, 5, 5].ToString();
            textBox56.Text = No[UShin, 0, 5, 6].ToString();
            textBox57.Text = No[UShin, 0, 5, 7].ToString();
            textBox58.Text = No[UShin, 0, 5, 8].ToString();
            textBox59.Text = No[UShin, 0, 5, 9].ToString();

            textBox61.Text = No[UShin, 0, 6, 1].ToString();
            textBox62.Text = No[UShin, 0, 6, 2].ToString();
            textBox63.Text = No[UShin, 0, 6, 3].ToString();
            textBox64.Text = No[UShin, 0, 6, 4].ToString();
            textBox65.Text = No[UShin, 0, 6, 5].ToString();
            textBox66.Text = No[UShin, 0, 6, 6].ToString();
            textBox67.Text = No[UShin, 0, 6, 7].ToString();
            textBox68.Text = No[UShin, 0, 6, 8].ToString();
            textBox69.Text = No[UShin, 0, 6, 9].ToString();

            textBox71.Text = No[UShin, 0, 7, 1].ToString();
            textBox72.Text = No[UShin, 0, 7, 2].ToString();
            textBox73.Text = No[UShin, 0, 7, 3].ToString();
            textBox74.Text = No[UShin, 0, 7, 4].ToString();
            textBox75.Text = No[UShin, 0, 7, 5].ToString();
            textBox76.Text = No[UShin, 0, 7, 6].ToString();
            textBox77.Text = No[UShin, 0, 7, 7].ToString();
            textBox78.Text = No[UShin, 0, 7, 8].ToString();
            textBox79.Text = No[UShin, 0, 7, 9].ToString();

            textBox81.Text = No[UShin, 0, 8, 1].ToString();
            textBox82.Text = No[UShin, 0, 8, 2].ToString();
            textBox83.Text = No[UShin, 0, 8, 3].ToString();
            textBox84.Text = No[UShin, 0, 8, 4].ToString();
            textBox85.Text = No[UShin, 0, 8, 5].ToString();
            textBox86.Text = No[UShin, 0, 8, 6].ToString();
            textBox87.Text = No[UShin, 0, 8, 7].ToString();
            textBox88.Text = No[UShin, 0, 8, 8].ToString();
            textBox89.Text = No[UShin, 0, 8, 9].ToString();

            textBox91.Text = No[UShin, 0, 9, 1].ToString();
            textBox92.Text = No[UShin, 0, 9, 2].ToString();
            textBox93.Text = No[UShin, 0, 9, 3].ToString();
            textBox94.Text = No[UShin, 0, 9, 4].ToString();
            textBox95.Text = No[UShin, 0, 9, 5].ToString();
            textBox96.Text = No[UShin, 0, 9, 6].ToString();
            textBox97.Text = No[UShin, 0, 9, 7].ToString();
            textBox98.Text = No[UShin, 0, 9, 8].ToString();
            textBox99.Text = No[UShin, 0, 9, 9].ToString();

        }

        private int Undo_kennsaku()
        {
            int Rc = 0;

            for (int i = 1; i > SShin; i--)
            {
                if (Undo[i] == 0)
                {
                    Shin = i;
                    Rc = i;
                    break;
                }
            }

            return Rc;
        }

        private int UCunt_Zelo()
        {
            int Rc = 0;

            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    if (No[UShin, 0, i, j] == 0)
                    {
                        Rc++;
                    }
                }
            }

            return Rc;
        }

        private void CopyMap(int t1, int t2)
        {
            for(int j1 = 0; j1 <= 9; j1++)
            {
                for(int j2 = 1; j2 <= 9; j2++)
                {
                    for(int j3 = 1; j3 <= 9; j3++)
                    {
                        No[t1, j1, j2, j3] = No[t2, j1, j2, j3];
                    }

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Set_No();
        }
    }

    static class Con
    {
        public const int CalcMax = 99990; // 配列上限
    }


}
