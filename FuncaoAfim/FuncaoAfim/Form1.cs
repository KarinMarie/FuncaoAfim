using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuncaoAfim
{
    public partial class Form1 : Form
    {
        FuncaoAfim funcao;
        DataBaseManager banco;

        public Form1()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US"); // Evitar um problema que eu tava tendo com o número decimal passando vírgulas para a consulta SQL.
            InitializeComponent();
            funcao = new FuncaoAfim(numA.Value, numB.Value, chartFuncao);
            banco = new DataBaseManager("FuncaoAfim");
            chartFuncao = funcao.GerarGrafico();
            lbFuncao.Text = funcao.FuncaoTexto();
            CarregarHistorico();
        }

        private void btnGerarGrafico_Click(object sender, EventArgs e)
        {
            try
            {
                funcao = new FuncaoAfim(numA.Value, numB.Value, chartFuncao);
                chartFuncao = funcao.GerarGrafico();
                lbFuncao.Text = funcao.FuncaoTexto();

                banco.AtualizarBanco($"INSERT INTO Funcoes VALUES({numA.Value}, {numB.Value}, '{lbFuncao.Text}')");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarHistorico()
        {
            listFuncoes.Items.Clear();

            DataTable tabela = banco.ConsultarBanco("SELECT Funcao FROM Funcoes ORDER BY ID DESC");

            if(tabela != null)
            {
                for(int i = 0; i < tabela.Rows.Count; i++)
                {
                    string linha = tabela.Rows[i]["Funcao"].ToString();
                    listFuncoes.Items.Add(linha);
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarHistorico();
        }

        private void btnLimparHistorico_Click(object sender, EventArgs e)
        {
            banco.ConsultarBanco("TRUNCATE TABLE Funcoes");
            listFuncoes.Items.Clear();
        }

        private void listFuncoes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tabela = banco.ConsultarBanco($"SELECT * FROM Funcoes WHERE Funcao = '{listFuncoes.SelectedItem}'");

            if (tabela != null)
            {
                if (listFuncoes.SelectedIndex != -1)
                {
                    decimal A = Convert.ToDecimal(tabela.Rows[0]["A"]);
                    decimal B = Convert.ToDecimal(tabela.Rows[0]["B"]);

                    funcao = new FuncaoAfim(A, B, chartFuncao);
                    chartFuncao = funcao.GerarGrafico();
                    lbFuncao.Text = funcao.FuncaoTexto();

                    numA.Value = A;
                    numB.Value = B;

                    tabControl1.SelectedIndex = 0;
                }
                else
                    MessageBox.Show("Por favor, selecione um item da lista.", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}

// https://coolors.co/d8e2dc-ffe5d9-ffcad4-f4acb7-9d8189
// https://icoconvert.com/