using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BoardF;

namespace WindowGameF
{
    public partial class FormGameF : Form
    {
        const int size = 4;
        Game game;

        public FormGameF()
        {
            InitializeComponent();
            game = new Game(size);
            HideButtons();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void b00_Click(object sender, EventArgs e)
        {
            if (game.Solved()) return;
                Button button = (Button)sender;
            int x = int.Parse(button.Name.Substring(1, 1));
            int y = int.Parse(button.Name.Substring(2, 1));
            game.PressAt(x, y);
            ShowButtons();

            if (game.Solved())
                labelMoves.Text = "Finish!!! Результат: " + game.Moves;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            game.Start(1000 + DateTime.Now.DayOfYear);
            ShowButtons();
        }

        void HideButtons()
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    ShowDigitAt(0, x, y);
            labelMoves.Text = "Результат";
        }
        void ShowButtons()
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    ShowDigitAt(game.GetDigitAt(x, y), x, y);

            labelMoves.Text = "Ходов: "+ game.Moves.ToString();
        }

        void ShowDigitAt(int digit,int x, int y)
        {
            Button button = (Button) Controls["b" + x + y];
            button.Text = digit.ToString();
            
            button.Visible = digit > 0;
        }
    }
}
