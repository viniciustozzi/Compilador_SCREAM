using Compiler.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador_SCREAM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Compiler compiler = new Compiler();
            bool compiled = compiler.Compile(txtCode.Text);

            lstTokens.Items.Clear();
            // Exibe os tokens..
            foreach (Token token in compiler.Tokens)
            {
                lstTokens.Items.Add(token.Type.Description + "(" + token.Value + ")");
            }
            // Exibe a árvore..
            tvDerivation.Nodes.Clear();

            //AddNode(compiler.DerivationTree.BaseNode, null);
            // Exibe mensagem de falha ou sucesso..

            if (compiled)
            {
                MessageBox.Show("Build Succeeded", "Build Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Build Failed", "Build Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
