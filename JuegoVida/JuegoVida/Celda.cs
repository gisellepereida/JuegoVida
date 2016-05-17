using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace JuegoVida
{
    class Celda
    {
        public enum Estado { Viva, Muerta };

        int x, y;
        public Estado estado_actual;
        public Estado estado_siguiente;

        static int lado = 20;

        public Celda(int x, int y, Random r)
        {

            double d = r.NextDouble();
            if (d < .80)
                estado_actual = Estado.Muerta;
            else
                estado_actual = Estado.Viva;

            this.x = x; this.y = y;
        }
        public void Dibuja(Form f)
        {

            Graphics g = f.CreateGraphics();
            g.DrawRectangle(new Pen(Color.Black, 1), x, y, lado, lado);
            if (estado_actual == Estado.Viva)
                g.FillRectangle(new SolidBrush(Color.DarkRed), x, y, lado, lado);


        }
        public void update()
        {
            estado_actual = estado_siguiente;
        }
    }
        class Tablero
        {
            List<List<Celda>> tablero;
            int tamaño;
            Random r = new Random();
            public Tablero(int tamaño)
            {
                tablero = new List<List<Celda>>();
                this.tamaño = tamaño;
                for (int i = 0; i < tamaño; i++)
                {
                    List<Celda> temp = new List<Celda>();
                    for (int j = 0; j < tamaño; j++)
                    {
                        temp.Add(new Celda(20 + i * 20, 20 + j * 20, r));
                    }
                    tablero.Add(temp);

                }
            }
            public void Dibuja(Form f)
            {
                for (int i = 0; i < tamaño; i++)
                    for (int j = 0; j < tamaño; j++)
                    {
                        tablero[i][j].Dibuja(f);
                    }
            }

            public void next()
            {
                for (int i = 0; i < tamaño; i++)
                    for (int j = 0; j < tamaño; j++)
                    {
                        int vecinas = cuantas_vecinas_vivas(i, j);

                        if (vecinas < 2)
                            tablero[i][j].estado_siguiente = Celda.Estado.Muerta;

                        if (tablero[i][j].estado_actual == Celda.Estado.Viva)
                        {
                            if (vecinas == 2 || vecinas == 3)
                            {

                            }
                            else if (vecinas > 3)
                            {
                                tablero[i][j].estado_siguiente = Celda.Estado.Muerta;
                            }
                        }

                        else
                        {
                            if (vecinas == 3)
                            {
                                tablero[i][j].estado_siguiente = Celda.Estado.Viva;
                            }


                        }
                    }
            }

            public void update()
            {
                for (int i = 0; i < tamaño; i++)
                    for (int j = 0; j < tamaño; j++)
                        tablero[i][j].update();
            }

            int cuantas_vecinas_vivas(int i, int j)
            {
                int vivas = 0;

                //Noroeste
                if (i > 0 && j > 0 && tablero[i - 1][j - 1].estado_actual == Celda.Estado.Viva)
                    vivas++;

                //Norte
                if (i > 0 && tablero[i - 1][j].estado_actual == Celda.Estado.Viva)
                    vivas++;

                //Noreste
                if (i > 0 && j < tamaño - 1 && tablero[i - 1][j + 1].estado_actual == Celda.Estado.Viva)
                    vivas++;

                //Oeste
                if (j > 0 && tablero[i][j - 1].estado_actual == Celda.Estado.Viva)
                    vivas++;

                //Este
                if (j < tamaño - 1 && tablero[i][j + 1].estado_actual == Celda.Estado.Viva)
                    vivas++;

                //Suroeste
                if (i < tamaño - 1 && j > 0 && tablero[i + 1][j - 1].estado_actual == Celda.Estado.Viva)
                    vivas++;

                //Sur
                if (i < tamaño - 1 && tablero[i + 1][j].estado_actual == Celda.Estado.Viva)
                    vivas++;

                //Sureste
                if (i < tamaño - 1 && j < tamaño - 1 && tablero[i + 1][j + 1].estado_actual == Celda.Estado.Viva)
                    vivas++;

                return vivas;


            }
        }
    }
    

