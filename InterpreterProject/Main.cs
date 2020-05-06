﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterpreterProject
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInput.Text = "";
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Lexer lexer = new Lexer(txtInput.Text);
            Parser parser = new Parser(lexer, txtOutput);
            parser.P();
        }
    }
}
