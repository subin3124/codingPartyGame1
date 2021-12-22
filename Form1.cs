using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
namespace 두더지_1차본
{
    public partial class Form1 : Form
    {
        int xRandom = 500;
        int yRandom = 600;
        string user;
        int score = 0;
        int time = 50;
        Random random = new Random();
        public Form1(String user)
        {
            InitializeComponent();
            this.user = user;

        }
        //두더지를 잡았을때
        private void pictureBox1_Click(object sender, EventArgs e)
        {

            int randx = random.Next(100, xRandom);
            int randy = random.Next(100, yRandom);
            pictureBox1.Location = new Point(randx,randy);
            randx = random.Next(100, xRandom);
            randy = random.Next(100, yRandom);
            pictureBox3.Location = new Point(randx, randy);
            score++;
            label1.Text = score.ToString();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            int randx = random.Next(100, xRandom);
            int randy = random.Next(100, yRandom);
            pictureBox3.Location = new Point(randx, randy);
            randx = random.Next(100, xRandom);
            randy = random.Next(100, yRandom);
            pictureBox1.Location = new Point(randx, randy);
            score--;
            label1.Text = score.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(random.Next(100, xRandom), random.Next(100, yRandom));
            pictureBox3.Location = new Point(random.Next(100, xRandom), random.Next(100, yRandom));
            timer1.Enabled = true;
            timer1.Start();
            pictureBox1.Visible = true;
            pictureBox3.Visible = true;
            button1.Visible = false;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time <= 0)
            {

                timer1.Stop();
                textBox1.Text = "게임 끝";
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
                pictureBox3.Visible= false;
                //서버 연결
                RestClient restClient = new RestClient("http://localhost:8080/Score/Game1");
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                GameJson json = new GameJson();
                json.user = user;
                json.score = score;
                request.AddJsonBody(json);
               IRestResponse res = restClient.Execute<GameJson>(request);
                Console.WriteLine(res.Content + "\n" + res.StatusCode);
            }
            else
            {
                time--;
                textBox1.Text = time.ToString();
            }
            
    
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            
           
            this.BackColor = Color.SaddleBrown;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
    class GameJson
    {
        public string user;
        public int score;

    }
}
