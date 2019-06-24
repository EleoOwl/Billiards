using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Billi
{
    public partial class Previev : Form
    {
        public Previev()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Previev_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        private void Previev_Load(object sender, EventArgs e)
        {

        }
    }
}
