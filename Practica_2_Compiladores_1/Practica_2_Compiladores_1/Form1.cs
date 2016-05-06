using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Irony.Ast;
using Irony.Parsing;
using Practica_2_Compiladores_1.Analizadores;

namespace Practica_2_Compiladores_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ParseTreeNode raiz = Sintactico.analizar("programa hola [var a;]");
                if (raiz == null)
                {
                    MessageBox.Show("Error");
                }
                else
                {
                    MessageBox.Show("Exito");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("El error es: "+ex.Message);
            }
            
        }
    }
}
