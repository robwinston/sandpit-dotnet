using System;
using System.Windows.Forms;
using CalcForms.Properties;
using Com.Rtwsq.Thom.Calculator;
using Com.Rtwsq.Thom.Calculator.Validation;

namespace CalcForms
{
    public partial class IntExpression : Form
    {
        public IntExpression()
        {
            InitializeComponent();
            ArithmeticExpression.SetExpressionType(ExpressionType.IntegerBasic);
            txtWhatsAllowed.Text = ArithmeticExpression.AllowedOps;
        }

        private void OnEvaluate(object sender, EventArgs e)
        {
            string expression = txtExpression.Text;

            if (expression == String.Empty)
            {
                btnReset.PerformClick();
            }
            else
            {
                ArithmeticExpression arithmeticExpression = new ArithmeticExpression(expression);

                if (arithmeticExpression.HasErrors)
                {
                    txtResult.Text = Resources.IntExpression_OnEvaluate_Error_;
                    txtMessages.Text = arithmeticExpression.ErrorMessages;
                }
                else
                {
                    txtResult.Text = arithmeticExpression.Result;
                }
            }
        }

        private void OnExpressionChange(object sender, EventArgs e)
        {
            txtResult.ResetText();
            txtMessages.ResetText();
            btnEvaluate.Enabled = txtExpression.Text.Length > 0;
        }

        private void OnReset(object sender, EventArgs e)
        {
            txtResult.ResetText();
            txtMessages.ResetText();
            txtExpression.ResetText();
        }

        private void OnExpressionOpts(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton) sender;
            
            if (rb.Checked)
            {
                if (rb.Name == "rbSimple")
                {
                    ArithmeticExpression.SetExpressionType(ExpressionType.IntegerBasic);
                    txtWhatsAllowed.Text = ArithmeticExpression.AllowedOps;
                }
                else if (rb.Name == "rbExtended")
                {
                    ArithmeticExpression.SetExpressionType(ExpressionType.IntegerExtended);
                    txtWhatsAllowed.Text = ArithmeticExpression.AllowedOps;
                }
                else
                {
                    txtWhatsAllowed.Text = @"? (" + rb.Name + @")";
                }

                txtMessages.ResetText();
                txtResult.ResetText();
                
            }
            
        }
    }
}
