using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace Bai01
{
    public partial class Form1 : Form
    {
        SmtpClient mailclient = new SmtpClient("smtp.gmail.com", 587);
        MailMessage message = null;
        string filePath = "";
        public Form1()
        {
            mailclient.EnableSsl = true;
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtSender.Text == string.Empty) || (txtPass.Text == string.Empty) || (txtTo.Text == string.Empty) || (txtBody.Text == string.Empty))
                    MessageBox.Show("Information is not empty!");
                else
                {
                    mailclient.Credentials = new NetworkCredential(txtSender.Text, txtPass.Text);
                    message = new MailMessage(txtSender.Text, txtTo.Text);
                    message.Subject = txtSubject.Text;
                    message.Body = txtBody.Text;
                    if (filePath == "")
                        return;
                    else
                    {
                        Attachment attach = new Attachment(filePath);
                        message.Attachments.Add(attach);
                    }
                    mailclient.Send(message);
                    MessageBox.Show("Mail was sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = @"C:\";
            open.Title = "Browse a file";
            open.Filter = "All files(*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
                filePath = open.FileName;
            txtBody.Text += "Attachment: " + filePath;

        }
    }
}
