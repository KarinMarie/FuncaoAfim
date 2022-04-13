using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace FuncaoAfim
{
    class FuncaoAfim
    {
        private decimal A;
        private decimal B;
        private Chart grafico;

        public FuncaoAfim(decimal A, decimal B, Chart grafico)
        {
            this.A = A;
            this.B = B;
            this.grafico = grafico;
        }

        private decimal CalcularPontoY()
        {
            return B;
        }

        private decimal CalcularPontoX()
        {
            return (0 - B) / A;
        }

        public Chart GerarGrafico()
        {
            
            // DEFINIR OS QUADRANTES DO GRÁFICO:
            grafico.ChartAreas[0].AxisX.Maximum = 10;
            grafico.ChartAreas[0].AxisX.Minimum = -10;
           
            grafico.ChartAreas[0].AxisY.Maximum = 10;
            grafico.ChartAreas[0].AxisY.Minimum = -10;
            // ------------------------------------------

            // DEFININDO OS EIXOS X E Y DO GRÁFICO:
            grafico.ChartAreas[0].AxisX.Crossing = 0;
            grafico.ChartAreas[0].AxisY.Crossing = 0;
            // ------------------------------------------

            // RETIRANDO AS GRADES DO GRÁFICO:
            grafico.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            grafico.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            grafico.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            grafico.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            // ------------------------------------------

            // LIMPANDO OS PONTOS ANTERIORES E
            // ADICIONANDO OS PONTOS QUE CORTAM OS EIXOS X E Y:
            grafico.Series[0].Points.Clear();
            grafico.Series[0].Points.AddXY(0, CalcularPontoY());
            grafico.Series[0].Points.AddXY(CalcularPontoX(), 0);
            // ------------------------------------------

            // ADICIONANDO OUTROS PONTOS PARA TORNAR A LINHA DO GRÁFICO MAIS LONGA:
            decimal Y;
            for (int X = -5; X <= 10; X++)
            {
                Y = A * X + B;
                grafico.Series[0].Points.AddXY(X, Y);
            }
            // ------------------------------------------

            grafico.Series[0].Color = Color.FromArgb(229, 56, 82); // Muda a cor da linha do gráfico
            return grafico;
        }

        public string FuncaoTexto()
        {
            if (B >= 0)
                return $"f(x) = {A}x + {B}";
            else
                return $"f(x) = {A}x - {B*-1}";
        }
    }
}
