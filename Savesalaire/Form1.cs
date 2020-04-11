using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
namespace Savesalaire
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            BANKEntities ctx = new BANKEntities();
            List<SalaryAccount> s = ctx.SalaryAccount.ToList<SalaryAccount>();
            List<SavingAccount> s1 = ctx.SavingAccount.ToList<SavingAccount>();

            solde1.Text = s[0].Balance.ToString();
            solde2.Text = s1[0].Balance.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int a = int.Parse(textBox1.Text);

            using (var ctx = new BANKEntities())
            {
                List<SalaryAccount> s = ctx.SalaryAccount.ToList<SalaryAccount>();
                List<SavingAccount> s1 = ctx.SavingAccount.ToList<SavingAccount>();
                using (DbContextTransaction tr = ctx.Database.BeginTransaction())
                {
                    int? x=s[0].Balance - a;
                    try
                    {
                        if (x >= 0)
                        {

                            s[0].Balance -= a;
                            s1[0].Balance += a;

                            ctx.SaveChanges();

                            MessageBox.Show("Transaction success");

                            tr.Commit();
                            textBox1.Clear();
                        }
                        else
                        {
                            MessageBox.Show("tu peux pas faire cette operation a cause vous avez pas d solde qui contient le montant entrer");
                            
                        }

                        
                    }
                    catch(Exception ex)

                    {
                        MessageBox.Show("Transaction Fail "+ex.Message);
                        tr.Rollback();

                    }

                    refresh();
                   
                
                  

                }
            }


        }
    void refresh()
        {
            solde1.Text = "";
            solde2.Text = "";

            solde1.Text = s[0].Balance.ToString();
            solde2.Text = s1[0].Balance.ToString();
        }
    }
}
