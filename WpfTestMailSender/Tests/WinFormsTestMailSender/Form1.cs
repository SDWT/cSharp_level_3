using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Mail;
using System.Security;

namespace WinFormsTestMailSender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 
            ToolTip tltSender = new ToolTip();
            tltSender.SetToolTip(tbxSender, "Отправитель");

            ToolTip tltPassword = new ToolTip();
            tltPassword.SetToolTip(passwordBox, "Пароль");
        }

        private void BtnSendEmail_Click(object sender, EventArgs e)
        {
            // Emails list 
            List<string> listStrMails = new List<string>();
            listStrMails.Add(tbxSender.Text);

            string strSender = tbxSender.Text;
            SecureString strPassword = passwordBox.Text;

            foreach (var mail in listStrMails)
            {
                using (var mm = new MailMessage(strSender, mail))
                {
                    mm.Subject = "Hello from C#";    // Email title / Тема
                    mm.Body = "Hello world! hahaha"; // Email body / Текст
                    mm.IsBodyHtml = false;           // false - body hasnt html

                    using (var sc = new SmtpClient("smtp.mail.ru", 25))
                    {
                        sc.EnableSsl = true;
                        sc.Credentials = new NetworkCredential(strSender, strPassword);
                        try
                        {
                            sc.Send(mm);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при отправке {ex.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } // smtp
                } // mm
            }
            MessageBox.Show("Работа завершена.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
