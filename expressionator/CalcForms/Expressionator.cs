using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CalcForms.Properties;
using Com.Rtwsq.Thom.Calculator;
using Com.Rtwsq.Thom.Calculator.Config;
using Com.Rtwsq.Thom.Calculator.Validation;

namespace CalcForms
{
    public partial class Expressionator : Form
    {
        public Expressionator()
        {
            InitializeComponent();
            ArithmeticExpression.SetExpressionType(ExpressionType.IntegerBasic);
            txtOpsAllowed.Text = ArithmeticExpression.AllowedOps;
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

        private readonly Dictionary<string, ExpressionType> _typeOptions = new Dictionary<string, ExpressionType>()
        {
            {"rbSimple", ExpressionType.IntegerBasic},
            {"rbExtended", ExpressionType.IntegerExtended},
            {"rbSimpleDecimal", ExpressionType.DecimalBasic},
            {"rbExtendedDecimal", ExpressionType.DecimalExtended}
        };

        private void OnExpressionOpts(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton) sender;
            
            if (rb.Checked)
            {
                if (_typeOptions.ContainsKey(rb.Name))
                {
                    ArithmeticExpression.SetExpressionType(_typeOptions[rb.Name]);
                }
                else
                {
                    txtOpsAllowed.Text = @"? (" + rb.Name + @")";
                }

                txtNumsAllowed.Text = ArithmeticExpression.AllowedNumberTypes;
                txtOpsAllowed.Text = ArithmeticExpression.AllowedOps;
                txtMessages.ResetText();
                txtResult.ResetText();
                
            }
            
        }
    }
}
