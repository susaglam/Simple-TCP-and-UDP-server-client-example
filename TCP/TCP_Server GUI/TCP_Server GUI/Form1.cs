using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_Server_GUI
{
    public partial class Form1 : Form
    {
        Server server;

        public Form1()
        {
            InitializeComponent();
            consoleBox.Text = "Server is not running.\n";
        }


        private async void start1_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "START")
            {
                int port = Convert.ToInt32(txtPort.Text);
                if (port < 7001 || port > 7020)
                {
                    consoleBox.Text += "Please select port 7001 to 7020.";
                    return;
                }
                server = Server.getInstance(port);
                consoleBox.Text = "The server is running. Waiting for client connection.\n";
                btnStart.Text = "STOP";
                await Task.Run(() =>
                {
                    if (server != null)
                    {
                        server.acceptClient();
                    }
                });

                consoleBox.Text = "Client " + server.getClientEndPoint().Address + " is connected to the port " + server.getClientEndPoint().Port;

                while (true)
                {
                    string temp = "";
                    await Task.Run(() =>
                    {
                        temp = server.receiveMessage();
                    });
                    consoleBox.Text += "\n" + temp;

                }
            }
            else
            {
                btnStart.Text = "START";
            }


        }

    }
}
