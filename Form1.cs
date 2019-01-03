using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        Point Cpos;
        Boolean shotFlg; //true :発射処理
        Boolean hitFlg; //true  :当たり
        long score; //スコア

        public Form1()
        {
            InitializeComponent();
        }

        //Playerの上移動
        private void movePlayer()
        {
            if (hitFlg)
            {
                return; //プレイヤーの動きをとめる
            }

            Player.Top -= 12;
            if ( Player.Top < (0 - Player.Height) )
            {
                score += 10; //画面上部まで来たのでスコア加算
                scoreLabel.Text = score.ToString();
                Sukima.Width -= 5; //すき間を狭くする
                initPlayer(); //Playerの初期化
            }

            long pH = Player.Height;
            long pW = Player.Width;
            long sH = Sukima.Height;
            long sW = Sukima.Width;

            if ((Player.Top < Sukima.Top + sH) && (Player.Top + pH > Sukima.Top))
              {
                if ((Player.Left < Sukima.Left) || (Player.Left + pW > Sukima.Left + sW))
                    {
                        hitFlg = true;
                        button1.Enabled = true; //開始ボタンを有効にする
                        Gameover.Show(); //ゲームオーバーを表示する
                    }
             }
        }

        //すき間の横移動
        private void moveSukima()
        {
            if (hitFlg)
            {
                return;
            }

            Sukima.Left += 4;
            if (Sukima.Left > Width)
            {
                Sukima.Left = -Sukima.Width;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cpos = PointToClient(Cursor.Position);
            moveSukima(); //すき間の横移動

            if (shotFlg)
            {
                movePlayer(); //Playerの上移動
                return; //timer1_Tickから抜ける
            }

            if (Cpos.X < 0)
            {
                Cpos.X = 0;
            }
            if (Cpos.X >  Width - Player.Width)
            {
                Cpos.X = Width - Player.Width;
            }
           
            Player.Left = Cpos.X;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initGame();
        }

        //Gameの初期化
        private void initGame()
        {
            hitFlg = false; //false:はずれ
            Gameover.Hide(); //ゲームオーバーを隠す
            button1.Enabled = false; //開始ボタンを無効にする
            score = 0;  //スコアのクリア
            scoreLabel.Text = "0";
            Sukima.Width = 80; //すき間の幅
            initPlayer();
        }

        //Playerの初期化
        private void initPlayer()
        {
            Player.Top = Height - (Player.Height * 2);
            Player.Left = Cpos.X;
            shotFlg = false; //false:発射していない
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            shotFlg = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initGame();
        }
    }
}
