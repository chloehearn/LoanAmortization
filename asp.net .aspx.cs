using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Payment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            for (var i = 2.00; i <= 8; i += 0.25)
                ddlInterest.Items.Add(i.ToString());
        }
        
    }

    protected void btnPayment_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void btnFuture_Click(object sender, EventArgs e)
    {
        Response.Redirect("Future.aspx");
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
             int quantity;

        if (int.TryParse(txtLoan.Text, out quantity) == true)
        {
            var interest = ddlInterest.Text;
            var term = txtTerm.Text;
            var loan = txtLoan.Text;

            double intRateConv = double.Parse(interest);
            int termConv = int.Parse(term);
            double loanConv = double.Parse(loan);
            var monthlyRate = ((intRateConv / 100) / 12);
            var months = termConv * 12;

            var payment = ((loanConv * monthlyRate) / (1 - Math.Pow((1 + monthlyRate), -months)));

            lblOutput.Text = string.Format("{0:C}", payment);

            var periods = (termConv * 12);
            for (int rows = 1; rows < (periods + 1); rows++)
            {
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                cell1.Text = rows.ToString();
                row.Cells.Add(cell1);
                tblOutput.Rows.Add(row);
                TableCell cell2 = new TableCell();
                cell2.Text = string.Format("{0:C}", loanConv);
                row.Cells.Add(cell2);
                tblOutput.Rows.Add(row);
                TableCell cell3 = new TableCell();
                cell3.Text = string.Format("{0:C}", payment);
                row.Cells.Add(cell3);
                tblOutput.Rows.Add(row);
                TableCell cell4 = new TableCell();
                var newInterestRate = ((intRateConv / 100) / 12);
                var interestOutput = (loanConv * newInterestRate);
                cell4.Text = string.Format("{0:C}", interestOutput);
                row.Cells.Add(cell4);
                tblOutput.Rows.Add(row);
                TableCell cell5 = new TableCell();
                var principal = payment - (loanConv * newInterestRate);
                cell5.Text = string.Format("{0:C}", principal);
                row.Cells.Add(cell5);
                tblOutput.Rows.Add(row);

                var balance = loanConv - principal;
                if (balance < 0) balance = 0;

                TableCell cell6 = new TableCell();
                cell6.Text = string.Format("{0:C}", balance);
                row.Cells.Add(cell6);
                tblOutput.Rows.Add(row);

                loanConv = balance;

            }
     


        }
    }
    protected void txtTerm_TextChanged(object sender, EventArgs e)
    {

    }
}
