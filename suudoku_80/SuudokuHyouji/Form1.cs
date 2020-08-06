/*
===============================================================================
数独ソリッドプログラム
解決力80％（予測）
タイムアタック及び解法手順確認用
オープンソース
基礎コーディング　by N.Tanaka 2020.04.19
===============================================================================
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

//プロジェクト　数独表示
namespace SuudokuHyouji
{
    //メインフォームソース
    public partial class Form1 : Form
    {
        int[,,] No= new int[Con.CalcMax, 10, 10];   //メイン配列
        int[,,] Num = new int[10, 10, 10];          //各位置に書ける数字用配列
        Boolean FlgSet;                             //数字を新たにセット出来たか？
        Boolean Mujyun;                             //矛盾発生しているか？
        bool LoopFlg = true;                        //メインループ中？
        int Shin = 0;                               //手順位置
        int[,] Ren = new int[2,3];                  //同一操作チェック用配列
        int[] Undo = new int[Con.CalcMax];          //操作を戻す用配列
        int[,] UBack = new int[Con.CalcMax,2];      //操作を戻す位置用配列
        int[,] NBack = new int[10, 2];              //太い線で囲まれた3×3の設定最初位置用配列
        int SShin = 0;                              //メイン配列書き込み位置
        int UShin;                                  //借置き位置
        int Back = 0;                               //現在の戻り位置
        int XBack = 0;                              //現在の位置の戻り数字
        int t = 0;                                  //時間計算用
#if DEBUG
        string log;
#endif

        public Form1()
        {
            InitializeComponent();
            BGamen();
        }

        //開始ボタン押下時の処理
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;             //開始時の現在時刻
            string result = dt.ToString();          //時刻の文字列
            int Rc;                                 //関数の戻り値

            //初期情報セット
            button1.Enabled = false;
            button2.Enabled = false;
            label1.Text += result;
            t = System.Environment.TickCount;

            get_No();
            Gamen();
            UBack[Shin, 0] = Back++;
            UBack[Shin, 1] = XBack++;

            //メインループ
            while (LoopFlg == true)
            {
                FlgSet = false;
                Mujyun = false;

                //各マスの設定できる数字の初期化
                for (int jj = 1; jj <= 9; jj++)
                {
                    for (int kk = 1; kk <= 9; kk++)
                    {
                        for (int ii = 0; ii <=9; ii++)
                        {
                            Num[ii, jj, kk] = 0;
                        }
                    }
                }

                //各マスの設定できる数字の算出
                for (int jj = 1; jj <= 9; jj++)
                {
                    for (int kk = 1; kk <= 9; kk++)
                    {
                        Tate_Set(No[Shin, jj, kk], kk);
                        Yoko_Set(No[Shin, jj, kk], jj);
                        A_Set(No[Shin, jj, kk], jj, kk);
                        B_Set(No[Shin, jj, kk], jj, kk);
                        C_Set(No[Shin, jj, kk], jj, kk);
                        D_Set(No[Shin, jj, kk], jj, kk);
                        E_Set(No[Shin, jj, kk], jj, kk);
                        F_Set(No[Shin, jj, kk], jj, kk);
                        G_Set(No[Shin, jj, kk], jj, kk);
                        H_Set(No[Shin, jj, kk], jj, kk);
                        I_Set(No[Shin, jj, kk], jj, kk);
                    }
                }

                // メッセージ・キューにあるWindowsメッセージをすべて処理する
                Application.DoEvents();

                //ただ１つの設定数字の確定
                for (int jj = 1; jj <= 9; jj++)
                {
                    for (int kk = 1; kk <= 9; kk++)
                    {
                        One_Kennsaku(jj, kk);
                        if (FlgSet == true) break;
                    }
                    if (FlgSet == true) break;
                }

                //解けた！！チェック
                if (Seikai() == 729) break;

                if (FlgSet == false)
                {
#if DEBUG
                    log = String.Concat("kuuhaku_Kennsaku\r\n");
                    LogSave(log);
#endif
                    //空白をチェックして埋める
                    kuuhaku_Kennsaku();
                    {
                        // メッセージ・キューにあるWindowsメッセージをすべて処理する
                        Application.DoEvents();
                    }

                    //解けた！！チェック
                    if (Seikai() == 729) break;

                }

                if (FlgSet == false)
                {
                    Undo[Shin] = 1;
#if DEBUG
                    log = String.Concat("Undo_kennsaku Shin =", Shin.ToString(), "\r\n");
                    LogSave(log);
#endif
                    //手順を戻す
                    if (Undo_kennsaku() == 0)
                    {
                        break;
                    }

                    //矛盾チェック
                    if (Mujyun == true)
                    {
                        Rc = Seikai();
                        if (Rc > 0) Rc = 0;
                        Go_Start(Rc);
                        if (FlgSet == false) break;
                        Mujyun = false;
                    }
                }

                Set_No();
                if (checkBox1.Checked == true) Thread.Sleep(200);

            }


            //解けなかった場合の処理
            if (Seikai() != 729)
            {
                 label2.Text = "降参";
            }

            Set_No();
            t = System.Environment.TickCount - t;
            label2.Text += (t.ToString() + "m秒");
            button2.Enabled = true;

        }

        //画面データ→配列データ
        private void get_No()
        {
             No[Shin, 1, 1] = int.Parse(textBox11.Text);
             No[Shin, 1, 2] = int.Parse(textBox12.Text);
             No[Shin, 1, 3] = int.Parse(textBox13.Text);
             No[Shin, 1, 4] = int.Parse(textBox14.Text);
             No[Shin, 1, 5] = int.Parse(textBox15.Text);
             No[Shin, 1, 6] = int.Parse(textBox16.Text);
             No[Shin, 1, 7] = int.Parse(textBox17.Text);
             No[Shin, 1, 8] = int.Parse(textBox18.Text);
             No[Shin, 1, 9] = int.Parse(textBox19.Text);

             No[Shin, 2, 1] = int.Parse(textBox21.Text);
             No[Shin, 2, 2] = int.Parse(textBox22.Text);
             No[Shin, 2, 3] = int.Parse(textBox23.Text);
             No[Shin, 2, 4] = int.Parse(textBox24.Text);
             No[Shin, 2, 5] = int.Parse(textBox25.Text);
             No[Shin, 2, 6] = int.Parse(textBox26.Text);
             No[Shin, 2, 7] = int.Parse(textBox27.Text);
             No[Shin, 2, 8] = int.Parse(textBox28.Text);
             No[Shin, 2, 9] = int.Parse(textBox29.Text);

             No[Shin, 3, 1] = int.Parse(textBox31.Text);
             No[Shin, 3, 2] = int.Parse(textBox32.Text);
             No[Shin, 3, 3] = int.Parse(textBox33.Text);
             No[Shin, 3, 4] = int.Parse(textBox34.Text);
             No[Shin, 3, 5] = int.Parse(textBox35.Text);
             No[Shin, 3, 6] = int.Parse(textBox36.Text);
             No[Shin, 3, 7] = int.Parse(textBox37.Text);
             No[Shin, 3, 8] = int.Parse(textBox38.Text);
             No[Shin, 3, 9] = int.Parse(textBox39.Text);

             No[Shin, 4, 1] = int.Parse(textBox41.Text);
             No[Shin, 4, 2] = int.Parse(textBox42.Text);
             No[Shin, 4, 3] = int.Parse(textBox43.Text);
             No[Shin, 4, 4] = int.Parse(textBox44.Text);
             No[Shin, 4, 5] = int.Parse(textBox45.Text);
             No[Shin, 4, 6] = int.Parse(textBox46.Text);
             No[Shin, 4, 7] = int.Parse(textBox47.Text);
             No[Shin, 4, 8] = int.Parse(textBox48.Text);
             No[Shin, 4, 9] = int.Parse(textBox49.Text);

             No[Shin, 5, 1] = int.Parse(textBox51.Text);
             No[Shin, 5, 2] = int.Parse(textBox52.Text);
             No[Shin, 5, 3] = int.Parse(textBox53.Text);
             No[Shin, 5, 4] = int.Parse(textBox54.Text);
             No[Shin, 5, 5] = int.Parse(textBox55.Text);
             No[Shin, 5, 6] = int.Parse(textBox56.Text);
             No[Shin, 5, 7] = int.Parse(textBox57.Text);
             No[Shin, 5, 8] = int.Parse(textBox58.Text);
             No[Shin, 5, 9] = int.Parse(textBox59.Text);

             No[Shin, 6, 1] = int.Parse(textBox61.Text);
             No[Shin, 6, 2] = int.Parse(textBox62.Text);
             No[Shin, 6, 3] = int.Parse(textBox63.Text);
             No[Shin, 6, 4] = int.Parse(textBox64.Text);
             No[Shin, 6, 5] = int.Parse(textBox65.Text);
             No[Shin, 6, 6] = int.Parse(textBox66.Text);
             No[Shin, 6, 7] = int.Parse(textBox67.Text);
             No[Shin, 6, 8] = int.Parse(textBox68.Text);
             No[Shin, 6, 9] = int.Parse(textBox69.Text);

             No[Shin, 7, 1] = int.Parse(textBox71.Text);
             No[Shin, 7, 2] = int.Parse(textBox72.Text);
             No[Shin, 7, 3] = int.Parse(textBox73.Text);
             No[Shin, 7, 4] = int.Parse(textBox74.Text);
             No[Shin, 7, 5] = int.Parse(textBox75.Text);
             No[Shin, 7, 6] = int.Parse(textBox76.Text);
             No[Shin, 7, 7] = int.Parse(textBox77.Text);
             No[Shin, 7, 8] = int.Parse(textBox78.Text);
             No[Shin, 7, 9] = int.Parse(textBox79.Text);

             No[Shin, 8, 1] = int.Parse(textBox81.Text);
             No[Shin, 8, 2] = int.Parse(textBox82.Text);
             No[Shin, 8, 3] = int.Parse(textBox83.Text);
             No[Shin, 8, 4] = int.Parse(textBox84.Text);
             No[Shin, 8, 5] = int.Parse(textBox85.Text);
             No[Shin, 8, 6] = int.Parse(textBox86.Text);
             No[Shin, 8, 7] = int.Parse(textBox87.Text);
             No[Shin, 8, 8] = int.Parse(textBox88.Text);
             No[Shin, 8, 9] = int.Parse(textBox89.Text);

             No[Shin, 9, 1] = int.Parse(textBox91.Text);
             No[Shin, 9, 2] = int.Parse(textBox92.Text);
             No[Shin, 9, 3] = int.Parse(textBox93.Text);
             No[Shin, 9, 4] = int.Parse(textBox94.Text);
             No[Shin, 9, 5] = int.Parse(textBox95.Text);
             No[Shin, 9, 6] = int.Parse(textBox96.Text);
             No[Shin, 9, 7] = int.Parse(textBox97.Text);
             No[Shin, 9, 8] = int.Parse(textBox98.Text);
             No[Shin, 9, 9] = int.Parse(textBox99.Text);

        }

        //配列データ→画面データ
        private void Set_No()
        {
            textBox11.Text =  No[Shin, 1, 1].ToString();
            textBox12.Text =  No[Shin, 1, 2].ToString();
            textBox13.Text =  No[Shin, 1, 3].ToString();
            textBox14.Text =  No[Shin, 1, 4].ToString();
            textBox15.Text =  No[Shin, 1, 5].ToString();
            textBox16.Text =  No[Shin, 1, 6].ToString();
            textBox17.Text =  No[Shin, 1, 7].ToString();
            textBox18.Text =  No[Shin, 1, 8].ToString();
            textBox19.Text =  No[Shin, 1, 9].ToString();

            textBox21.Text =  No[Shin, 2, 1].ToString();
            textBox22.Text =  No[Shin, 2, 2].ToString();
            textBox23.Text =  No[Shin, 2, 3].ToString();
            textBox24.Text =  No[Shin, 2, 4].ToString();
            textBox25.Text =  No[Shin, 2, 5].ToString();
            textBox26.Text =  No[Shin, 2, 6].ToString();
            textBox27.Text =  No[Shin, 2, 7].ToString();
            textBox28.Text =  No[Shin, 2, 8].ToString();
            textBox29.Text =  No[Shin, 2, 9].ToString();

            textBox31.Text =  No[Shin, 3, 1].ToString();
            textBox32.Text =  No[Shin, 3, 2].ToString();
            textBox33.Text =  No[Shin, 3, 3].ToString();
            textBox34.Text =  No[Shin, 3, 4].ToString();
            textBox35.Text =  No[Shin, 3, 5].ToString();
            textBox36.Text =  No[Shin, 3, 6].ToString();
            textBox37.Text =  No[Shin, 3, 7].ToString();
            textBox38.Text =  No[Shin, 3, 8].ToString();
            textBox39.Text =  No[Shin, 3, 9].ToString();

            textBox41.Text =  No[Shin, 4, 1].ToString();
            textBox42.Text =  No[Shin, 4, 2].ToString();
            textBox43.Text =  No[Shin, 4, 3].ToString();
            textBox44.Text =  No[Shin, 4, 4].ToString();
            textBox45.Text =  No[Shin, 4, 5].ToString();
            textBox46.Text =  No[Shin, 4, 6].ToString();
            textBox47.Text =  No[Shin, 4, 7].ToString();
            textBox48.Text =  No[Shin, 4, 8].ToString();
            textBox49.Text =  No[Shin, 4, 9].ToString();

            textBox51.Text =  No[Shin, 5, 1].ToString();
            textBox52.Text =  No[Shin, 5, 2].ToString();
            textBox53.Text =  No[Shin, 5, 3].ToString();
            textBox54.Text =  No[Shin, 5, 4].ToString();
            textBox55.Text =  No[Shin, 5, 5].ToString();
            textBox56.Text =  No[Shin, 5, 6].ToString();
            textBox57.Text =  No[Shin, 5, 7].ToString();
            textBox58.Text =  No[Shin, 5, 8].ToString();
            textBox59.Text =  No[Shin, 5, 9].ToString();

            textBox61.Text =  No[Shin, 6, 1].ToString();
            textBox62.Text =  No[Shin, 6, 2].ToString();
            textBox63.Text =  No[Shin, 6, 3].ToString();
            textBox64.Text =  No[Shin, 6, 4].ToString();
            textBox65.Text =  No[Shin, 6, 5].ToString();
            textBox66.Text =  No[Shin, 6, 6].ToString();
            textBox67.Text =  No[Shin, 6, 7].ToString();
            textBox68.Text =  No[Shin, 6, 8].ToString();
            textBox69.Text =  No[Shin, 6, 9].ToString();

            textBox71.Text =  No[Shin, 7, 1].ToString();
            textBox72.Text =  No[Shin, 7, 2].ToString();
            textBox73.Text =  No[Shin, 7, 3].ToString();
            textBox74.Text =  No[Shin, 7, 4].ToString();
            textBox75.Text =  No[Shin, 7, 5].ToString();
            textBox76.Text =  No[Shin, 7, 6].ToString();
            textBox77.Text =  No[Shin, 7, 7].ToString();
            textBox78.Text =  No[Shin, 7, 8].ToString();
            textBox79.Text =  No[Shin, 7, 9].ToString();

            textBox81.Text =  No[Shin, 8, 1].ToString();
            textBox82.Text =  No[Shin, 8, 2].ToString();
            textBox83.Text =  No[Shin, 8, 3].ToString();
            textBox84.Text =  No[Shin, 8, 4].ToString();
            textBox85.Text =  No[Shin, 8, 5].ToString();
            textBox86.Text =  No[Shin, 8, 6].ToString();
            textBox87.Text =  No[Shin, 8, 7].ToString();
            textBox88.Text =  No[Shin, 8, 8].ToString();
            textBox89.Text =  No[Shin, 8, 9].ToString();

            textBox91.Text =  No[Shin, 9, 1].ToString();
            textBox92.Text =  No[Shin, 9, 2].ToString();
            textBox93.Text =  No[Shin, 9, 3].ToString();
            textBox94.Text =  No[Shin, 9, 4].ToString();
            textBox95.Text =  No[Shin, 9, 5].ToString();
            textBox96.Text =  No[Shin, 9, 6].ToString();
            textBox97.Text =  No[Shin, 9, 7].ToString();
            textBox98.Text =  No[Shin, 9, 8].ToString();
            textBox99.Text =  No[Shin, 9, 9].ToString();

        }

        //最初入力数字を赤くする
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
            if( textBox.Text != "0")
            {
                textBox.ForeColor = Color.Red;
            }
        }

        //最初入力数字を青くする
        private void BGamen()
        {
            Set_ColorB(textBox11);
            Set_ColorB(textBox12);
            Set_ColorB(textBox13);
            Set_ColorB(textBox14);
            Set_ColorB(textBox15);
            Set_ColorB(textBox16);
            Set_ColorB(textBox17);
            Set_ColorB(textBox18);
            Set_ColorB(textBox19);

            Set_ColorB(textBox21);
            Set_ColorB(textBox22);
            Set_ColorB(textBox23);
            Set_ColorB(textBox24);
            Set_ColorB(textBox25);
            Set_ColorB(textBox26);
            Set_ColorB(textBox27);
            Set_ColorB(textBox28);
            Set_ColorB(textBox29);

            Set_ColorB(textBox31);
            Set_ColorB(textBox32);
            Set_ColorB(textBox33);
            Set_ColorB(textBox34);
            Set_ColorB(textBox35);
            Set_ColorB(textBox36);
            Set_ColorB(textBox37);
            Set_ColorB(textBox38);
            Set_ColorB(textBox39);

            Set_ColorB(textBox41);
            Set_ColorB(textBox42);
            Set_ColorB(textBox43);
            Set_ColorB(textBox44);
            Set_ColorB(textBox45);
            Set_ColorB(textBox46);
            Set_ColorB(textBox47);
            Set_ColorB(textBox48);
            Set_ColorB(textBox49);

            Set_ColorB(textBox51);
            Set_ColorB(textBox52);
            Set_ColorB(textBox53);
            Set_ColorB(textBox54);
            Set_ColorB(textBox55);
            Set_ColorB(textBox56);
            Set_ColorB(textBox57);
            Set_ColorB(textBox58);
            Set_ColorB(textBox59);

            Set_ColorB(textBox61);
            Set_ColorB(textBox62);
            Set_ColorB(textBox63);
            Set_ColorB(textBox64);
            Set_ColorB(textBox65);
            Set_ColorB(textBox66);
            Set_ColorB(textBox67);
            Set_ColorB(textBox68);
            Set_ColorB(textBox69);

            Set_ColorB(textBox71);
            Set_ColorB(textBox72);
            Set_ColorB(textBox73);
            Set_ColorB(textBox74);
            Set_ColorB(textBox75);
            Set_ColorB(textBox76);
            Set_ColorB(textBox77);
            Set_ColorB(textBox78);
            Set_ColorB(textBox79);

            Set_ColorB(textBox81);
            Set_ColorB(textBox82);
            Set_ColorB(textBox83);
            Set_ColorB(textBox84);
            Set_ColorB(textBox85);
            Set_ColorB(textBox86);
            Set_ColorB(textBox87);
            Set_ColorB(textBox88);
            Set_ColorB(textBox89);

            Set_ColorB(textBox91);
            Set_ColorB(textBox92);
            Set_ColorB(textBox93);
            Set_ColorB(textBox94);
            Set_ColorB(textBox95);
            Set_ColorB(textBox96);
            Set_ColorB(textBox97);
            Set_ColorB(textBox98);
            Set_ColorB(textBox99);

        }

        //青くするのサブルーチン
        private void Set_ColorB(TextBox textBox)
        {
            textBox.ForeColor = Color.Blue;
        }

        //縦使用数値設定
        private void Tate_Set(int mm, int j)
        {
            if(mm == 0)
            {
                return;
            }

            for ( int i = 1; i <= 9; i++ )
            {
                 Num[mm, i, j] = 1;
            }

            return;
        }

        //横使用数値設定
        private void Yoko_Set(int mm, int j)
        {
            if (mm == 0)
            {
                return;
            }

            for (int i = 1; i <= 9; i++)
            {
                Num[mm, j, i] = 1;
            }

            return;

        }

        private void A_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 1, 1 }, { 1, 2 }, { 1, 3 }, 
                                            { 2, 1 }, { 2, 2 }, { 2, 3 }, 
                                            { 3, 1 }, { 3, 2 }, { 3, 3 } };

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
                Num[mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        private void B_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 1, 4 }, { 1, 5 }, { 1, 6 },
                                            { 2, 4 }, { 2, 5 }, { 2, 6 },
                                            { 3, 4 }, { 3, 5 }, { 3, 6 } };

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
                Num[mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        private void C_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 1, 7 }, { 1, 8 }, { 1, 9 },
                                            { 2, 7 }, { 2, 8 }, { 2, 9 },
                                            { 3, 7 }, { 3, 8 }, { 3, 9 } };

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
                Num[mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        private void D_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 4, 1 }, { 4, 2 }, { 4, 3 }, 
                                            { 5, 1 }, { 5, 2 }, { 5, 3 }, 
                                            { 6, 1 }, { 6, 2 }, { 6, 3 } };

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
                Num[mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        private void E_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 4, 4 }, { 4, 5 }, { 4, 6 },
                                            { 5, 4 }, { 5, 5 }, { 5, 6 },
                                            { 6, 4 }, { 6, 5 }, { 6, 6 } };

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
                Num[mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        private void F_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 4, 7 }, { 4, 8 }, { 4, 9 },
                                            { 5, 7 }, { 5, 8 }, { 5, 9 },
                                            { 6, 7 }, { 6, 8 }, { 6, 9 } };

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
                Num[mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        private void G_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 7, 1 }, { 7, 2 }, { 7, 3 }, 
                                            { 8, 1 }, { 8, 2 }, { 8, 3 }, 
                                            { 9, 1 }, { 9, 2 }, { 9, 3 } };

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
                Num[mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void H_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 7, 4 }, { 7, 5 }, { 7, 6 }, 
                                            { 8, 4 }, { 8, 5 }, { 8, 6 }, 
                                            { 9, 4 }, { 9, 5 }, { 9, 6 } };

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
                Num[mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        private void I_Set(int mm, int kk, int jj)
        {
            int[,] gloup = new int[9, 2] { { 7, 7 }, { 7, 8 }, { 7, 9 }, 
                                            { 8, 7 }, { 8, 8 }, { 8, 9 }, 
                                            { 9, 7 }, { 9, 8 }, { 9, 9 } };

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
                Num[mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }

        //確定数値１個の決定
        private void One_Kennsaku(int jj, int kk)
        {
            int Cnt = 0;
            int work = 0;

            if ( No[Shin, jj, kk] != 0)
            {
                return;
            }

            for (int i = 1; i <= 9; i++)
            {
                if ( Num[i, jj, kk] == 0)
                {
                    Cnt++;
                    work = i;
                }
            }

            if (Cnt == 1)
            {
                No[Shin, jj, kk] = work;
                FlgSet = true;
#if DEBUG
                log = String.Concat("One_Kennsaku jj = ", jj.ToString());
                LogSave(log);
                log = String.Concat(" kk = ", kk.ToString());
                LogSave(log);
                log = String.Concat(" work = ", work.ToString(), "\r\n");
                LogSave(log);
#endif
            }

                return;
        }

        //空白検索
        private int kuuhaku_Kennsaku()
        {
            int rc = 0;
            int Cnt;
            int SetCnt = 0;
            Boolean UFlag = false;
            Boolean USet = false;
            int[] work = new int [10];

            for (int jj = 1; jj <= 9; jj++)
            {
                for (int kk = 1; kk <= 9; kk++)
                {
                    if (No[Shin, jj, kk] != 0)
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
                        if (Num[i, jj, kk] == 0)
                        {
                            work[Cnt++] = i;
                        }
                    }

                    if (Cnt == 0)
                    {
                        UFlag = true;
                        break;
                    }

                    if (Ren[0,0] == 0)
                    {
                        Ren[0, 0] = 1;
                        Ren[0, 1] = jj;
                        Ren[0, 2] = kk;
                    }
                    else
                    {
                        if (Ren[1,0] == 1)
                        {
                            Ren[0, 1] = Ren[1, 1];
                            Ren[0, 2] = Ren[1, 2];
                        }
                        Ren[1, 0] = 1;
                        Ren[1, 1] = jj;
                        Ren[1, 2] = kk;
                        if (Ren[0, 1] == Ren[1, 1] && Ren[0, 2] == Ren[1, 2])
                        {
                            UFlag = true;
                            break;
                        }
                    }

                    for (int rr = 0; rr < Cnt; rr++)
                    {
                        if (SShin + 1 >= Con.CalcMax)
                        {
                            FlgSet = false;
                            return rc;
                        }

                        CopyMap(SShin + 1, Shin);
                        No[SShin + 1, jj, kk] = work[rr];
                        UBack[SShin + 1, 0] = Back;
                        UBack[SShin + 1, 1] = XBack;
                        SShin++;
#if DEBUG
                        log = String.Concat("kuuhaku_Kennsaku loop SShin = ", SShin.ToString());
                        LogSave(log);
                        log = String.Concat(" jj = ", jj.ToString());
                        LogSave(log);
                        log = String.Concat(" kk = ", kk.ToString());
                        LogSave(log);
                        log = String.Concat(" work[rr] = ", work[rr].ToString());
                        LogSave(log);
                        log = String.Concat(" UBack[SShin+1, 0] Back= ", Back.ToString());
                        LogSave(log);
                        log = String.Concat(" UBack[SShin+1, 1] XBack= ", XBack.ToString(), "\r\n");
                        LogSave(log);
#endif
                        USet = true;
                    }

                    if ( USet == true)
                    {
                        {
                            int[,] gloup = new int[9, 2] { { 1, 1 }, { 1, 2 }, { 1, 3 },
                                            { 2, 1 }, { 2, 2 }, { 2, 3 },
                                            { 3, 1 }, { 3, 2 }, { 3, 3 } };
                            bool SSFlg = false;
                            for (int i = 0; i <= 8; i++)
                            {
                                if (jj == gloup[i, 0] && kk == gloup[i, 1]) SSFlg = true;
                            }
                            if (Shin > 0 && SSFlg == true && NBack[1, 0] == 0)
                            {
                                NBack[1, 0] = 1;
                                NBack[1, 1] = SShin;
                            }
                        }

                        {
                            int[,] gloup = new int[9, 2] { { 1, 4 }, { 1, 5 }, { 1, 6 },
                                            { 2, 4 }, { 2, 5 }, { 2, 6 },
                                            { 3, 4 }, { 3, 5 }, { 3, 6 } };
                            bool SSFlg = false;
                            for (int i = 0; i <= 8; i++)
                            {
                                if (jj == gloup[i, 0] && kk == gloup[i, 1]) SSFlg = true;
                            }
                            if (Shin > 0 && SSFlg == true && NBack[2, 0] == 0)
                            {
                                NBack[2, 0] = 1;
                                NBack[2, 1] = SShin;
                            }
                        }

                        {
                            int[,] gloup = new int[9, 2] { { 1, 7 }, { 1, 8 }, { 1, 9 },
                                            { 2, 7 }, { 2, 8 }, { 2, 9 },
                                            { 3, 7 }, { 3, 8 }, { 3, 9 } };
                            bool SSFlg = false;
                            for (int i = 0; i <= 8; i++)
                            {
                                if (jj == gloup[i, 0] && kk == gloup[i, 1]) SSFlg = true;
                            }
                            if (Shin > 0 && SSFlg == true && NBack[3, 0] == 0)
                            {
                                NBack[3, 0] = 1;
                                NBack[3, 1] = SShin;
                            }
                        }

                        {
                            int[,] gloup = new int[9, 2] { { 4, 1 }, { 4, 2 }, { 4, 3 },
                                            { 5, 1 }, { 5, 2 }, { 5, 3 },
                                            { 6, 1 }, { 6, 2 }, { 6, 3 } };
                            bool SSFlg = false;
                            for (int i = 0; i <= 8; i++)
                            {
                                if (jj == gloup[i, 0] && kk == gloup[i, 1]) SSFlg = true;
                            }
                            if (Shin > 0 && SSFlg == true && NBack[4, 0] == 0)
                            {
                                NBack[4, 0] = 1;
                                NBack[4, 1] = SShin;
                            }
                        }

                        {
                            int[,] gloup = new int[9, 2] { { 4, 4 }, { 4, 5 }, { 4, 6 },
                                            { 5, 4 }, { 5, 5 }, { 5, 6 },
                                            { 6, 4 }, { 6, 5 }, { 6, 6 } };
                            bool SSFlg = false;
                            for (int i = 0; i <= 8; i++)
                            {
                                if (jj == gloup[i, 0] && kk == gloup[i, 1]) SSFlg = true;
                            }
                            if (Shin > 0 && SSFlg == true && NBack[5, 0] == 0)
                            {
                                NBack[5, 0] = 1;
                                NBack[5, 1] = SShin;
                            }
                        }

                        {
                            int[,] gloup = new int[9, 2] { { 4, 7 }, { 4, 8 }, { 4, 9 },
                                            { 5, 7 }, { 5, 8 }, { 5, 9 },
                                            { 6, 7 }, { 6, 8 }, { 6, 9 } };
                            bool SSFlg = false;
                            for (int i = 0; i <= 8; i++)
                            {
                                if (jj == gloup[i, 0] && kk == gloup[i, 1]) SSFlg = true;
                            }
                            if (Shin > 0 && SSFlg == true && NBack[6, 0] == 0)
                            {
                                NBack[6, 0] = 1;
                                NBack[6, 1] = SShin;
                            }
                        }

                        {
                            int[,] gloup = new int[9, 2] { { 7, 1 }, { 7, 2 }, { 7, 3 },
                                            { 8, 1 }, { 8, 2 }, { 8, 3 },
                                            { 9, 1 }, { 9, 2 }, { 9, 3 } };
                            bool SSFlg = false;
                            for (int i = 0; i <= 8; i++)
                            {
                                if (jj == gloup[i, 0] && kk == gloup[i, 1]) SSFlg = true;
                            }
                            if (Shin > 0 && SSFlg == true && NBack[7, 0] == 0)
                            {
                                NBack[7, 0] = 1;
                                NBack[7, 1] = SShin;
                            }
                        }

                        {
                            int[,] gloup = new int[9, 2] { { 7, 4 }, { 7, 5 }, { 7, 6 },
                                            { 8, 4 }, { 8, 5 }, { 8, 6 },
                                            { 9, 4 }, { 9, 5 }, { 9, 6 } };
                            bool SSFlg = false;
                            for (int i = 0; i <= 8; i++)
                            {
                                if (jj == gloup[i, 0] && kk == gloup[i, 1]) SSFlg = true;
                            }
                            if (Shin > 0 && SSFlg == true && NBack[8, 0] == 0)
                            {
                                NBack[8, 0] = 1;
                                NBack[8, 1] = SShin;
                            }
                        }

                        {
                            int[,] gloup = new int[9, 2] { { 7, 7 }, { 7, 8 }, { 7, 9 },
                                            { 8, 7 }, { 8, 8 }, { 8, 9 },
                                            { 9, 7 }, { 9, 8 }, { 9, 9 } };
                            bool SSFlg = false;
                            for (int i = 0; i <= 8; i++)
                            {
                                if (jj == gloup[i, 0] && kk == gloup[i, 1]) SSFlg = true;
                            }
                            if (Shin > 0 && SSFlg == true && NBack[9, 0] == 0)
                            {
                                NBack[9, 0] = 1;
                                NBack[9, 1] = SShin;
                            }
                        }

                        Back = SShin;
                        XBack++;
                        break;
                    }
                }

                if (USet == true || UFlag == true)
                {
                    break;
                }

            }

            if ( UFlag == true)
            {
                FlgSet = false;
                Undo[Shin] = 1;
            } 
            else
            {
                FlgSet = true;
            }

            if (USet == false && UFlag == false)
            {
                Mujyun = true;
            }

            Shin = SShin;

            return rc;
        }

        //正解チェック
        private int Seikai()
        {
            int Rc = 0;

            UShin = Shin;

            for (int jj = 1; jj <= 9; jj++)
            {
                for (int kk = 1; kk <= 9; kk++)
                {
                    for (int nn = 1; nn <=9; nn++)
                    {
                        int[] ss = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                        ss[0] = A_UNum(nn, jj, kk);
                        ss[1] = B_UNum(nn, jj, kk);
                        ss[2] = C_UNum(nn, jj, kk);
                        ss[3] = D_UNum(nn, jj, kk);
                        ss[4] = E_UNum(nn, jj, kk);
                        ss[5] = F_UNum(nn, jj, kk);
                        ss[6] = G_UNum(nn, jj, kk);
                        ss[7] = H_UNum(nn, jj, kk);
                        ss[8] = I_UNum(nn, jj, kk);

                        for (int mm = 0; mm <= 8; mm++)
                        {
                            if (ss[mm] == 1)
                            {
                                Rc++;
                            }
                            else if (ss[mm] > 0)
                            {
                                Rc = (mm + 1) * -100;
#if DEBUG
                                log = String.Concat("Seikai loop mm = ", mm.ToString());
                                LogSave(log);
                                log = String.Concat(" ss[mm] = ", ss[mm].ToString());
                                LogSave(log);
                                log = String.Concat(" Shin = ", Shin.ToString());
                                LogSave(log);
                                log = String.Concat(" UShin = ", UShin.ToString(), "\r\n");
                                LogSave(log);
#endif
                                Go_Start(Rc);
                                return Rc;
                            }
                        }

                    }

                }

            }

            return Rc;
        }


        private int Tate_UNum(int mm, int j)
        {
            int rc = 0;

            if (mm == 0)
            {
                return rc;
            }

            for (int i = 1; i <= 9; i++)
            {
                if (No[UShin, i, j] == mm)
                {
                    rc++;
                }
            }

            return rc;
        }

        private int Yoko_UNum(int mm, int j)
        {
            int rc = 0;

            if (mm == 0)
            {
                return rc;
            }

            for (int i = 1; i <= 9; i++)
            {
                if (No[UShin, j, i] == mm)
                {
                    rc++;
                }
            }

            return rc;

        }

        private int A_UNum(int mm, int kk, int jj)
        {
            int rc = 0;
            int[,] gloup = new int[9, 2] { { 1, 1 }, { 1, 2 }, { 1, 3 }, 
                                            { 2, 1 }, { 2, 2 }, { 2, 3 }, 
                                            { 3, 1 }, { 3, 2 }, { 3, 3 } };

            if (mm == 0)
            {
                return rc;
            }

            if (kk > 3 || jj > 3)
            {
                return rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (No[UShin, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    rc++;
                }
            }

            return rc;
        }

        private int B_UNum(int mm, int kk, int jj)
        {
            int rc = 0;
            int[,] gloup = new int[9, 2] { { 1, 4 }, { 1, 5 }, { 1, 6 },
                                            { 2, 4 }, { 2, 5 }, { 2, 6 },
                                            { 3, 4 }, { 3, 5 }, { 3, 6 } };

            if (mm == 0)
            {
                return rc;
            }

            if (kk > 3 || jj < 4 || jj > 6)
            {
                return rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (No[UShin, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    rc++;
                }
            }

            return rc;
        }

        private int C_UNum(int mm, int kk, int jj)
        {
            int rc = 0;
            int[,] gloup = new int[9, 2] { { 1, 7 }, { 1, 8 }, { 1, 9 },
                                            { 2, 7 }, { 2, 8 }, { 2, 9 },
                                            { 3, 7 }, { 3, 8 }, { 3, 9 } };

            if (mm == 0)
            {
                return rc;
            }

            if (kk > 3 || jj < 7)
            {
                return rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (No[UShin, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    rc++;
                }
            }

            return rc;
        }

        private int D_UNum(int mm, int kk, int jj)
        {
            int rc = 0;
            int[,] gloup = new int[9, 2] { { 4, 1 }, { 4, 2 }, { 4, 3 }, 
                                            { 5, 1 }, { 5, 2 }, { 5, 3 }, 
                                            { 6, 1 }, { 6, 2 }, { 6, 3 } };

            if (mm == 0)
            {
                return rc;
            }

            if (kk < 4 || kk > 6 || jj > 3)
            {
                return rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (No[UShin, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    rc++;
                }
            }

            return rc;
        }

        private int E_UNum(int mm, int kk, int jj)
        {
            int rc = 0;
            int[,] gloup = new int[9, 2] { { 4, 4 }, { 4, 5 }, { 4, 6 },
                                            { 5, 4 }, { 5, 5 }, { 5, 6 },
                                            { 6, 4 }, { 6, 5 }, { 6, 6 } };

            if (mm == 0)
            {
                return rc;
            }

            if (kk < 4 || kk > 6 || jj < 4 || jj > 6)
            {
                return rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (No[UShin, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    rc++;
                }
            }

            return rc;
        }

        private int F_UNum(int mm, int kk, int jj)
        {
            int rc = 0;
            int[,] gloup = new int[9, 2] { { 4, 7 }, { 4, 8 }, { 4, 9 },
                                            { 5, 7 }, { 5, 8 }, { 5, 9 },
                                            { 6, 7 }, { 6, 8 }, { 6, 9 } };

            if (mm == 0)
            {
                return rc;
            }

            if (kk < 4 || kk > 6 || jj < 7)
            {
                return rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (No[UShin, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    rc++;
                }
            }

            return rc;
        }

        private int G_UNum(int mm, int kk, int jj)
        {
            int rc = 0;
            int[,] gloup = new int[9, 2] { { 7, 1 }, { 7, 2 }, { 7, 3 },
                                            { 8, 1 }, { 8, 2 }, { 8, 3 }, 
                                            { 9, 1 }, { 9, 2 }, { 9, 3 } };

            if (mm == 0)
            {
                return rc;
            }

            if (kk < 7 || jj > 3)
            {
                return rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (No[UShin, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    rc++;
                }
            }

            return rc;
        }

        private int H_UNum(int mm, int kk, int jj)
        {
            int rc = 0;
            int[,] gloup = new int[9, 2] { { 7, 4 }, { 7, 5 }, { 7, 6 }, 
                                            { 8, 4 }, { 8, 5 }, { 8, 6 }, 
                                            { 9, 4 }, { 9, 5 }, { 9, 6 } };

            if (mm == 0)
            {
                return rc;
            }

            if (kk < 7 || jj < 4 || jj > 6)
            {
                return rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (No[UShin, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    rc++;
                }
            }

            return rc;
        }

        private int I_UNum(int mm, int kk, int jj)
        {
            int rc = 0;
            int[,] gloup = new int[9, 2] { { 7, 7 }, { 7, 8 }, { 7, 9 }, 
                                            { 8, 7 }, { 8, 8 }, { 8, 9 }, 
                                            { 9, 7 }, { 9, 8 }, { 9, 9 } };

            if (mm == 0)
            {
                return rc;
            }

            if (kk < 7 || jj < 7)
            {
                return rc;
            }

            for (int i = 0; i <= 8; i++)
            {
                if (No[UShin, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    rc++;
                }
            }

            return rc;
        }

        //思考画面を戻す
        private int Undo_kennsaku()
        {
            int Rc = 0;

            for (int i = Shin; i >= 1; i--)
            {
                if (Undo[i] == 0)
                {
                    Rc = i;
                    break;
                }
            }

            if ( Rc != 0)
            {
                Undo[Rc] = 1;
                if (SShin + 1 >= Con.CalcMax)
                {
                    FlgSet = false;
                    return 0;
                }

                CopyMap(SShin+1, Rc);
                Shin = SShin+1;
                Back = Shin;
                UBack[Shin, 0] = Shin;
                UBack[Shin, 1] = XBack++;
                SShin++;
            }
            return Rc;
        }

        //画面コピー
        private void CopyMap(int t1, int t2)
        {
            for (int j2 = 1; j2 <= 9; j2++)
            {
                for (int j3 = 1; j3 <= 9; j3++)
                {
                    No[t1, j2, j3] = No[t2, j2, j3];
                }

            }
        }

        //最初の分岐まで戻す
        private void Go_Start(int TT)
        {
            int Rc = 0;
            int WW = 0;
            FlgSet = true;

#if DEBUG
            log = String.Concat("Go_Start TT = ", TT.ToString(), "\r\n");
            LogSave(log);
#endif
            switch (TT)
            {
                case 0:
                    for(int i = 1; i <= 9; i++)
                    {
                        if (NBack[i, 0] == 1)
                        {
                            WW = UBack[NBack[i, 1], 1];
#if DEBUG
                            log = String.Concat("Go_Start i = ", i.ToString());
                            LogSave(log);
                            log = String.Concat(" NBack[i, 1] = ", NBack[i, 1].ToString(), "\r\n");
                            LogSave(log);
#endif
                            break;
                        }
                    }
                    break;
                case -100:
                    WW = UBack[NBack[1, 1], 1];
                    break;
                case -200:
                    WW = UBack[NBack[2, 1], 1];
                    break;
                case -300:
                    WW = UBack[NBack[3, 1], 1];
                    break;
                case -400:
                    WW = UBack[NBack[4, 1], 1];
                    break;
                case -500:
                    WW = UBack[NBack[5, 1], 1];
                    break;
                case -600:
                    WW = UBack[NBack[6, 1], 1];
                    break;
                case -700:
                    WW = UBack[NBack[7, 1], 1];
                    break;
                case -800:
                    WW = UBack[NBack[8, 1], 1];
                    break;
                case -900:
                    WW = UBack[NBack[9, 1], 1];
                    break;
                default:
                    FlgSet = false;
                    return;
            }

#if DEBUG
            log = String.Concat("Go_Start Shin = ", Shin.ToString());
            LogSave(log);
            log = String.Concat(" WW = ", WW.ToString(), "\r\n");
            LogSave(log);
#endif
            if (WW == 0)
            {
                LoopFlg = false;
                return;
            }
            for (int i = Shin; i >= 1; i--)
            {
                if (Undo[i] == 0 && UBack[i,1] == WW)
                {
                    Shin = UBack[i, 0];
                    Undo[Shin] = 1;
                    Rc = i;
                    break;
                }
            }

            if ( Rc != 0)
            {
                for (int i = Shin; i >= 1; i--)
                {
                    if (Undo[i] == 0 && UBack[i, 1] == WW)
                    {
                        Shin = i;
                        Back = i;
                        Rc = i;
                        Undo[Rc] = 1;
                        if (SShin + 1 >= Con.CalcMax)
                        {
                            FlgSet = false;
                            return;
                        }

                        if (UBack[NBack[1, 1], 0] == 1 && UBack[i, 1] == UBack[NBack[1, 1], 1])
                        {
                            for (int k = 0; k <= 9; k++)
                            {
                                NBack[k, 0] = 0;
                                NBack[k, 1] = 0;
                            }

                        }
                        CopyMap(SShin + 1, Rc);
                        Shin = SShin + 1;
                        SShin++;
                        UBack[Shin, 0] = Shin;
                        UBack[Shin, 1] = XBack++;
                        break;
                    }
                    else
                    {
                        if (UBack[i, 1] != WW)
                        {
                            FlgSet = false;
                            for (int j = 1; j <= 9; j++)
                            {
                                if (Undo[i] == 0 && NBack[j, 0] == 1)
                                {
                                    Undo[i] = 1;
                                    WW = UBack[NBack[j, 1], 1];
                                    FlgSet = true;
                                    if (SShin + 1 >= Con.CalcMax)
                                    {
                                        FlgSet = false;
                                        return;
                                    }

                                    CopyMap(SShin + 1, i);
                                    Shin = SShin + 1;
                                    SShin++;
                                    UBack[Shin, 0] = Shin;
                                    UBack[Shin, 1] = XBack++;
#if DEBUG
                                    log = String.Concat("Go_Start Shin = ", Shin.ToString());
                                    LogSave(log);
                                    log = String.Concat(" j = ", j.ToString());
                                    LogSave(log);
                                    log = String.Concat(" NBack[j, 1] = ", NBack[j, 1].ToString(), "\r\n");
                                    LogSave(log);
#endif
                                    return;
                                }
                            }
                        } 
                        else
                        {
                            Rc = 0;
                            LoopFlg = false;
                            return;
                        }
                    }
                }

            }

            if (Rc == 0)
            {
                FlgSet = false;
                return; 
            }

        }

        private void LogSave(string text)
        {
            string filePath = @"Debag.log";

            StreamWriter sw = new StreamWriter(filePath, true, Encoding.GetEncoding("shift_jis"));
            sw.Write(text);
            sw.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Set_No();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Shin = 0;                               //手順位置
            SShin = 0;                              //メイン配列書き込み位置
            Back = 0;                               //現在の戻り位置
            XBack = 0;                              //現在の位置の戻り数字
            t = 0;                                  //時間計算用
            LoopFlg = true;

            for(int i = 0; i < 10; i++)
            {
                NBack[i, 0] = 0;
                NBack[i, 1] = 0;
            }

            for(int i = 0; i <=9; i++)
            {
                for(int j = 0; j <= 9; j++)
                {
                    No[0, i, j] = 0; 
                }
            }

            label1.Text = "開始";
            label2.Text = "終了";
            Set_No();
            BGamen();

            button1.Enabled = true;

        }
    }

    static class Con
    {
        public const int CalcMax = 990; // 配列上限
    }


}
