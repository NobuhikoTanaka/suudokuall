
        private void Tate_SSet(int mm, int j)
        {
            if(mm == 0)
            {
                return;
            }

            for( int i = 1; i <= 9; i++ )
            {
                 No[SUShin, mm, i, j] = 1;
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
                 No[SUShin, mm, j, i] = 1;
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

            if(kk > 3 || jj > 3 )
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                 No[SUShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
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
                 No[SUShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
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

            if (kk < 7  || jj > 3)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                 No[SUShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
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

            if (kk > 3 || jj < 4 ||@jj > 6)
            {
                return;
            }

            for (int i = 0; i <= 8; i++)
            {
                 No[SUShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
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
                 No[SUShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
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
                 No[SUShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
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
                 No[SUShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
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
                 No[SUShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
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
                 No[SUShin, mm, gloup[i, 0], gloup[i, 1]] = 1;
            }

            return;
        }
        
        private void USet_No()
        {
            textBox11.Text =  No[UShin, 0, 1, 1].ToString();
            textBox12.Text =  No[UShin, 0, 1, 2].ToString();
            textBox13.Text =  No[UShin, 0, 1, 3].ToString();
            textBox14.Text =  No[UShin, 0, 1, 4].ToString();
            textBox15.Text =  No[UShin, 0, 1, 5].ToString();
            textBox16.Text =  No[UShin, 0, 1, 6].ToString();
            textBox17.Text =  No[UShin, 0, 1, 7].ToString();
            textBox18.Text =  No[UShin, 0, 1, 8].ToString();
            textBox19.Text =  No[UShin, 0, 1, 9].ToString();

            textBox21.Text =  No[UShin, 0, 2, 1].ToString();
            textBox22.Text =  No[UShin, 0, 2, 2].ToString();
            textBox23.Text =  No[UShin, 0, 2, 3].ToString();
            textBox24.Text =  No[UShin, 0, 2, 4].ToString();
            textBox25.Text =  No[UShin, 0, 2, 5].ToString();
            textBox26.Text =  No[UShin, 0, 2, 6].ToString();
            textBox27.Text =  No[UShin, 0, 2, 7].ToString();
            textBox28.Text =  No[UShin, 0, 2, 8].ToString();
            textBox29.Text =  No[UShin, 0, 2, 9].ToString();

            textBox31.Text =  No[UShin, 0, 3, 1].ToString();
            textBox32.Text =  No[UShin, 0, 3, 2].ToString();
            textBox33.Text =  No[UShin, 0, 3, 3].ToString();
            textBox34.Text =  No[UShin, 0, 3, 4].ToString();
            textBox35.Text =  No[UShin, 0, 3, 5].ToString();
            textBox36.Text =  No[UShin, 0, 3, 6].ToString();
            textBox37.Text =  No[UShin, 0, 3, 7].ToString();
            textBox38.Text =  No[UShin, 0, 3, 8].ToString();
            textBox39.Text =  No[UShin, 0, 3, 9].ToString();

            textBox41.Text =  No[UShin, 0, 4, 1].ToString();
            textBox42.Text =  No[UShin, 0, 4, 2].ToString();
            textBox43.Text =  No[UShin, 0, 4, 3].ToString();
            textBox44.Text =  No[UShin, 0, 4, 4].ToString();
            textBox45.Text =  No[UShin, 0, 4, 5].ToString();
            textBox46.Text =  No[UShin, 0, 4, 6].ToString();
            textBox47.Text =  No[UShin, 0, 4, 7].ToString();
            textBox48.Text =  No[UShin, 0, 4, 8].ToString();
            textBox49.Text =  No[UShin, 0, 4, 9].ToString();

            textBox51.Text =  No[UShin, 0, 5, 1].ToString();
            textBox52.Text =  No[UShin, 0, 5, 2].ToString();
            textBox53.Text =  No[UShin, 0, 5, 3].ToString();
            textBox54.Text =  No[UShin, 0, 5, 4].ToString();
            textBox55.Text =  No[UShin, 0, 5, 5].ToString();
            textBox56.Text =  No[UShin, 0, 5, 6].ToString();
            textBox57.Text =  No[UShin, 0, 5, 7].ToString();
            textBox58.Text =  No[UShin, 0, 5, 8].ToString();
            textBox59.Text =  No[UShin, 0, 5, 9].ToString();

            textBox61.Text =  No[UShin, 0, 6, 1].ToString();
            textBox62.Text =  No[UShin, 0, 6, 2].ToString();
            textBox63.Text =  No[UShin, 0, 6, 3].ToString();
            textBox64.Text =  No[UShin, 0, 6, 4].ToString();
            textBox65.Text =  No[UShin, 0, 6, 5].ToString();
            textBox66.Text =  No[UShin, 0, 6, 6].ToString();
            textBox67.Text =  No[UShin, 0, 6, 7].ToString();
            textBox68.Text =  No[UShin, 0, 6, 8].ToString();
            textBox69.Text =  No[UShin, 0, 6, 9].ToString();

            textBox71.Text =  No[UShin, 0, 7, 1].ToString();
            textBox72.Text =  No[UShin, 0, 7, 2].ToString();
            textBox73.Text =  No[UShin, 0, 7, 3].ToString();
            textBox74.Text =  No[UShin, 0, 7, 4].ToString();
            textBox75.Text =  No[UShin, 0, 7, 5].ToString();
            textBox76.Text =  No[UShin, 0, 7, 6].ToString();
            textBox77.Text =  No[UShin, 0, 7, 7].ToString();
            textBox78.Text =  No[UShin, 0, 7, 8].ToString();
            textBox79.Text =  No[UShin, 0, 7, 9].ToString();

            textBox81.Text =  No[UShin, 0, 8, 1].ToString();
            textBox82.Text =  No[UShin, 0, 8, 2].ToString();
            textBox83.Text =  No[UShin, 0, 8, 3].ToString();
            textBox84.Text =  No[UShin, 0, 8, 4].ToString();
            textBox85.Text =  No[UShin, 0, 8, 5].ToString();
            textBox86.Text =  No[UShin, 0, 8, 6].ToString();
            textBox87.Text =  No[UShin, 0, 8, 7].ToString();
            textBox88.Text =  No[UShin, 0, 8, 8].ToString();
            textBox89.Text =  No[UShin, 0, 8, 9].ToString();

            textBox91.Text =  No[UShin, 0, 9, 1].ToString();
            textBox92.Text =  No[UShin, 0, 9, 2].ToString();
            textBox93.Text =  No[UShin, 0, 9, 3].ToString();
            textBox94.Text =  No[UShin, 0, 9, 4].ToString();
            textBox95.Text =  No[UShin, 0, 9, 5].ToString();
            textBox96.Text =  No[UShin, 0, 9, 6].ToString();
            textBox97.Text =  No[UShin, 0, 9, 7].ToString();
            textBox98.Text =  No[UShin, 0, 9, 8].ToString();
            textBox99.Text =  No[UShin, 0, 9, 9].ToString();

        }



