using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DitecLibrarySystem
{
    public partial class FrmIssueBooks : Form
    {
        public FrmIssueBooks()
        {
            InitializeComponent();
        }


        private void dtpBarrow_ValueChanged(object sender, EventArgs e)
        {
            dtpReturn.Value = dtpBarrow.Value.AddDays(7);
        }

           private void btnIssueBooks_Click(object sender, EventArgs e)
        {
            //bool result = DataLink.runCommand("INSERT INTO tbl_barrow_books (RefCode,BookID,MemberID,IssueDate,ReturnDate,Status) values ('"+lblRefCode.Text+"','"+txtISBN.Text+"','"+txtMemberID.Text+"','"+dtpBarrow.Value.Date.ToShortDateString()+"','"+dtpReturn.Value.Date.ToShortDateString()+"','issued');");
            //textBox1.Text="test" + dtpBarrow.Value.Date.ToShortDateString();
            bool result = DataLink.runCommand("INSERT INTO tbl_barrow_books (RefCode,BookID,MemberID,IssueDate,ReturnDate,Status) values ('" + lblRefCode.Text + "','" + txtISBN.Text + "','" + txtMemberID.Text + "','" + dtpBarrow.Value.Date.ToString("yyyy-MM-dd H:mm:ss") + "','" + dtpReturn.Value.Date.ToString("yyyy-MM-dd H:mm:ss") + "','issued');");
            
               DataLink.runCommand("UPDATE tbl_book SET    Status = 'issued to " + txtMemberID.Text + "'   WHERE BookID = " + txtISBN.Text + ";"); //update book status

            if (result)
            {
                MessageBox.Show("Book issued successfully !", "Ditec Library System ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }  
        }

        private void FrmIssueBooks_Load(object sender, EventArgs e)
        {
            dtpReturn.Value = dtpBarrow.Value.AddDays(7);
           
            
            getRefCode();
        }


        private void getRefCode()
        {
            try
            {
                 MySqlCommand resultscommand = null;
                MySqlDataAdapter adp = new MySqlDataAdapter();
                DataTable resultstable = new DataTable();
                resultscommand = new  MySqlCommand("SELECT * from tbl_barrow_books;", DataLink.libConnection);
                adp.SelectCommand = resultscommand;
                adp.Fill(resultstable);
                int _refCode = resultstable.Rows.Count + 1;
                lblRefCode.Text = (_refCode.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading tbl_barrow_books");

            }
        }

        private void btnRefCode_Click(object sender, EventArgs e)
        {
            getRefCode();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
