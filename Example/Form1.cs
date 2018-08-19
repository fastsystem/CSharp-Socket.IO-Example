using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example
{
    public partial class Form1 : Form
    {
        Socket socket;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.socket = IO.Socket("http://localhost:3000");

            this.socket.On(Socket.EVENT_CONNECT, () =>
            {
                this.socket.Emit("last-messages", (ary) =>
                {
                    foreach (JObject jo in (ary as JArray))
                        this.RecvNewMessage(jo);
                });
            });

            this.socket.On("new-message", (jo) =>
            {
                this.RecvNewMessage(jo as JObject);
            });
        }


        private void RecvNewMessage(JObject jobject)
        {
            var message = jobject.ToObject<NewMessage>();

            this.Invoke((MethodInvoker)(() => {
                this.lstMessages.Items.Add(message.Date.ToString() + ":" + message.Text);
            }));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var message = new NewMessage()
            {
                Date = DateTime.Now,
                Text = this.txtMessage.Text
            };

            var jobject = JObject.FromObject(message);

            this.RecvNewMessage(jobject);
            this.socket.Emit("send-message", jobject);
        }
    }
}
