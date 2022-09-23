using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DemoXO
{
    public partial class Form1 : Form
    {
        MenuStrip menuStrip1 = new ();
        
        Button[,] button = new Button[3,3];

        int[,] number = new int[3, 3];

        bool NextClick;

        ToolStripMenuItem NEW1 = new("GAME");
        ToolStripMenuItem FIRST = new("NEW GAME");
        ToolStripMenuItem TWELW = new("CLEAR GAME");
        ToolStripMenuItem THIRD = new("CLOSE");

        ToolStripMenuItem aboutItem = new("ABOUT GAME");

        public Form1()
        {
            InitializeComponent();

            MinimumSize = new Size(370, 370);
            MaximumSize = new Size(370, 370);
            Text = "XO";
            NEW1.DropDownItems.Add(FIRST);
            NEW1.DropDownItems.Add(TWELW);
            NEW1.DropDownItems.Add(THIRD);           

            FIRST.Click += FIRST_Click;
            TWELW.Click += TWELW_Click;
            THIRD.Click += THIRD_Click;
            menuStrip1.Items.Add(NEW1);
            Controls.Add(menuStrip1);

            
            aboutItem.Click += AboutItem_Click;
            menuStrip1.Items.Add(aboutItem);
            
        }

        private void AboutItem_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("GAME XO 3x3");
        }

        private void THIRD_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void TWELW_Click(object? sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    this.button[i, j].Dispose();
                }           
        }

        private void FIRST_Click(object? sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {

                    button[i, j] = new();
                    this.button[i, j].Name = "button";
                    this.button[i, j].Size = new Size(100, 100);
                    this.button[i, j].Text = "[" + i.ToString() + " " + j.ToString() + "]";
                    this.button[i, j].BackColor = Color.Chocolate;
                    this.button[i, j].Location = new Point(25 + 100 * i, 25 + 100 * j);
                    this.button[i, j].Click += Button1_Click;
                    Controls.Add(this.button[i, j]);
                    number[i,j] = 0;
                }
            
            
        }


        private void Button1_Click(object? sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (sender == button[i, j] && NextClick==false)
                    {
                        GraphicsPath myPath = new();
                        Rectangle smallRectangle1 = button[i, j].ClientRectangle;
                        Rectangle smallRectangle2 = button[i, j].ClientRectangle;
                        Rectangle smallRectangle3 = button[i, j].ClientRectangle;
                        smallRectangle1.Inflate(-3, -40);
                        smallRectangle2.Inflate(-40, -3);
                        myPath.AddRectangle(smallRectangle1);
                        myPath.AddRectangle(smallRectangle2);
                        myPath.AddRectangle(smallRectangle3);

                        button[i, j].Region = new Region(myPath);
                        button[i, j].Enabled = false;
                        NextClick=true;
                        number[i, j] = 1;
                        break;
                    }
                    else if (sender == button[i, j] && NextClick == true) 
                    {
                        GraphicsPath myPath = new();
                        Rectangle smallRectangle = button[i, j].ClientRectangle;
                        smallRectangle.Inflate(-3, -3);
                        myPath.AddEllipse(smallRectangle);                       
                        button[i, j].Region = new Region(myPath);
                        button[i, j].Enabled = false;
                        NextClick = false;
                        number[i, j] = 2;;
                        break;
                    }
                  
                }
            Wins();
        }
        private void Wins()

        {
            int i = 0;
            int j = 0;

            if (((number[i, j] == number[i, j + 1]) && (number[i, j] == number[i, j + 2]) && (number[i, j] != 0)
                     || (number[i + 1, j] == number[i + 1, j + 1]) && (number[i + 1, j] == number[i + 1, j + 2]) && (number[i + 1, j] != 0)
                     || (number[i + 2, j] == number[i + 2, j + 1]) && (number[i + 2, j] == number[i + 2, j + 2]) && (number[i + 2, j] != 0)
                     || (number[i, j] == number[i + 1, j + 1]) && (number[i, j] == number[i + 2, j + 2]) && (number[i, j] != 0)
                     || (number[i + 2, j] == number[i + 1, j + 1]) && (number[i + 2, j] == number[i, j + 2]) && (number[i + 2, j] != 0)
                     || (number[i, j] == number[i + 1, j]) && (number[i, j] == number[i + 2, j]) && (number[i, j] != 0)
                     || (number[i, j + 1] == number[i + 1, j + 1]) && (number[i, j + 1] == number[i + 2, j + 1]) && (number[i, j + 1] != 0)
                     || (number[i, j + 2] == number[i + 1, j + 2]) && (number[i, j + 2] == number[i + 2, j + 2]) && (number[i, j + 2] != 0)))
            {
                if (MessageBox.Show("WIN") == DialogResult.OK)
                {
                    for (int i1 = 0; i1 < 3; i1++)
                        for (int j1 = 0; j1 < 3; j1++)
                        {
                            this.button[i1, j1].Dispose();
                        }
                }

            }
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}