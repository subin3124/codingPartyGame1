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
    public partial class Form2 : Form
    {
        User user;
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RestClient client = new RestClient("http://localhost:8080/User/Add");
            RestRequest restRequest = new RestRequest(Method.POST);
            user = new User();
            user.id = textBox1.Text;
            user.name = textBox2.Text;
            restRequest.AddJsonBody(user);
            restRequest.AddHeader("Content-Type", "application/json");
            client.Execute<User>(restRequest);
            Form1 form1 = new Form1(user.id);
            form1.Show();
            this.Visible = false;
        }
    }
    class User
    {
        public string id;
        public string name;
    }
}
