
        private void Tate_UNum(int mm, int j)
        {
            if (mm == 0)
            {
                return;
            }

            for (int i = 1; i <= 9; i++)
            {
                if( No[UShin, 0, i, j] == mm)
                {
                    UNum++;
                }
            }

            return;
        }

        private void Yoko_UNum(int mm, int j)
        {
            if (mm == 0)
            {
                return;
            }

            for (int i = 1; i <= 9; i++)
            {
                if ( No[UShin, 0, j, i] == mm)
                {
                    UNum++;
                }
            }

            return;

        }

        private void A_UNum(int mm, int kk, int jj)
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
                if( No[UShin, 0, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    UNum++;
                }
            }


            return;
        }

        private void B_UNum(int mm, int kk, int jj)
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
                if( No[UShin, 0, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    UNum++;
                }
            }

            return;
        }
        private void C_UNum(int mm, int kk, int jj)
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
                if( No[UShin, 0, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    UNum++;
                }
            }

            return;
        }
        private void D_UNum(int mm, int kk, int jj)
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
                if( No[UShin, 0, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    UNum++;
                }
            }

            return;
        }

        private void E_UNum(int mm, int kk, int jj)
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
                if( No[UShin, 0, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    UNum++;
                }
            }

            return;
        }
        private void F_UNum(int mm, int kk, int jj)
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
                if( No[UShin, 0, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    UNum++;
                }
            }

            return;
        }
        private void G_UNum(int mm, int kk, int jj)
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
                if( No[UShin, 0, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    UNum++;
                }
            }

            return;
        }

        private void H_UNum(int mm, int kk, int jj)
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
                if( No[UShin, 0, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    UNum++;
                }
            }

            return;
        }
        private void I_UNum(int mm, int kk, int jj)
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
                if( No[UShin, 0, gloup[i, 0], gloup[i, 1]] == mm)
                {
                    UNum++;
                }
            }

            return;
        }
