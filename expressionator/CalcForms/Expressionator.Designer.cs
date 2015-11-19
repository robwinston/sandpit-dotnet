namespace CalcForms
{
    partial class Expressionator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ExpressionTip = new System.Windows.Forms.ToolTip(this.components);
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.btnEvaluate = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.grpExpOptions = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNumsAllowed = new System.Windows.Forms.TextBox();
            this.rbSimpleDecimal = new System.Windows.Forms.RadioButton();
            this.txtOpsAllowed = new System.Windows.Forms.TextBox();
            this.rbExtended = new System.Windows.Forms.RadioButton();
            this.rbSimple = new System.Windows.Forms.RadioButton();
            this.rbExtendedDecimal = new System.Windows.Forms.RadioButton();
            this.grpExpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtExpression
            // 
            this.txtExpression.Location = new System.Drawing.Point(75, 62);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(239, 20);
            this.txtExpression.TabIndex = 0;
            this.ExpressionTip.SetToolTip(this.txtExpression, "Enter an integer arithmetic expression using add (+), subtract (-), multiply(*), " +
        "negate(-)\r\nGroup expressions with parentheses as needed (nesting is permitted).");
            this.txtExpression.TextChanged += new System.EventHandler(this.OnExpressionChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Expression";
            // 
            // txtMessages
            // 
            this.txtMessages.Location = new System.Drawing.Point(75, 231);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ReadOnly = true;
            this.txtMessages.Size = new System.Drawing.Size(353, 122);
            this.txtMessages.TabIndex = 5;
            this.ExpressionTip.SetToolTip(this.txtMessages, "Displays error messages if Evaluate fails");
            // 
            // btnEvaluate
            // 
            this.btnEvaluate.AutoSize = true;
            this.btnEvaluate.Enabled = false;
            this.btnEvaluate.Location = new System.Drawing.Point(75, 98);
            this.btnEvaluate.Name = "btnEvaluate";
            this.btnEvaluate.Size = new System.Drawing.Size(75, 23);
            this.btnEvaluate.TabIndex = 2;
            this.btnEvaluate.Text = "Evaluate";
            this.btnEvaluate.UseVisualStyleBackColor = true;
            this.btnEvaluate.Click += new System.EventHandler(this.OnEvaluate);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(75, 166);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(100, 20);
            this.txtResult.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Result";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Messages";
            // 
            // btnReset
            // 
            this.btnReset.AutoSize = true;
            this.btnReset.Enabled = false;
            this.btnReset.Location = new System.Drawing.Point(166, 98);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.OnReset);
            // 
            // grpExpOptions
            // 
            this.grpExpOptions.Controls.Add(this.rbExtendedDecimal);
            this.grpExpOptions.Controls.Add(this.label5);
            this.grpExpOptions.Controls.Add(this.label4);
            this.grpExpOptions.Controls.Add(this.txtNumsAllowed);
            this.grpExpOptions.Controls.Add(this.rbSimpleDecimal);
            this.grpExpOptions.Controls.Add(this.txtOpsAllowed);
            this.grpExpOptions.Controls.Add(this.rbExtended);
            this.grpExpOptions.Controls.Add(this.rbSimple);
            this.grpExpOptions.Location = new System.Drawing.Point(534, 33);
            this.grpExpOptions.Name = "grpExpOptions";
            this.grpExpOptions.Size = new System.Drawing.Size(200, 251);
            this.grpExpOptions.TabIndex = 8;
            this.grpExpOptions.TabStop = false;
            this.grpExpOptions.Text = "Supported Expressions";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Operators";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Number Type";
            // 
            // txtNumsAllowed
            // 
            this.txtNumsAllowed.Location = new System.Drawing.Point(6, 163);
            this.txtNumsAllowed.Name = "txtNumsAllowed";
            this.txtNumsAllowed.ReadOnly = true;
            this.txtNumsAllowed.Size = new System.Drawing.Size(175, 20);
            this.txtNumsAllowed.TabIndex = 9;
            // 
            // rbSimpleDecimal
            // 
            this.rbSimpleDecimal.AutoSize = true;
            this.rbSimpleDecimal.Location = new System.Drawing.Point(7, 71);
            this.rbSimpleDecimal.Name = "rbSimpleDecimal";
            this.rbSimpleDecimal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rbSimpleDecimal.Size = new System.Drawing.Size(97, 17);
            this.rbSimpleDecimal.TabIndex = 3;
            this.rbSimpleDecimal.Text = "Decimal Simple";
            this.rbSimpleDecimal.UseVisualStyleBackColor = true;
            this.rbSimpleDecimal.CheckedChanged += new System.EventHandler(this.OnExpressionOpts);
            // 
            // txtOpsAllowed
            // 
            this.txtOpsAllowed.Location = new System.Drawing.Point(6, 210);
            this.txtOpsAllowed.Name = "txtOpsAllowed";
            this.txtOpsAllowed.ReadOnly = true;
            this.txtOpsAllowed.Size = new System.Drawing.Size(175, 20);
            this.txtOpsAllowed.TabIndex = 2;
            // 
            // rbExtended
            // 
            this.rbExtended.AutoSize = true;
            this.rbExtended.Location = new System.Drawing.Point(7, 44);
            this.rbExtended.Name = "rbExtended";
            this.rbExtended.Size = new System.Drawing.Size(106, 17);
            this.rbExtended.TabIndex = 1;
            this.rbExtended.Text = "Integer Extended";
            this.rbExtended.UseVisualStyleBackColor = true;
            this.rbExtended.CheckedChanged += new System.EventHandler(this.OnExpressionOpts);
            // 
            // rbSimple
            // 
            this.rbSimple.AutoSize = true;
            this.rbSimple.Checked = true;
            this.rbSimple.Location = new System.Drawing.Point(7, 20);
            this.rbSimple.Name = "rbSimple";
            this.rbSimple.Size = new System.Drawing.Size(92, 17);
            this.rbSimple.TabIndex = 0;
            this.rbSimple.TabStop = true;
            this.rbSimple.Text = "Integer Simple";
            this.rbSimple.UseVisualStyleBackColor = true;
            this.rbSimple.CheckedChanged += new System.EventHandler(this.OnExpressionOpts);
            // 
            // rbExtendedDecimal
            // 
            this.rbExtendedDecimal.AutoSize = true;
            this.rbExtendedDecimal.Location = new System.Drawing.Point(7, 100);
            this.rbExtendedDecimal.Name = "rbExtendedDecimal";
            this.rbExtendedDecimal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rbExtendedDecimal.Size = new System.Drawing.Size(111, 17);
            this.rbExtendedDecimal.TabIndex = 12;
            this.rbExtendedDecimal.Text = "Decimal Extended";
            this.rbExtendedDecimal.UseVisualStyleBackColor = true;
            this.rbExtendedDecimal.CheckedChanged += new System.EventHandler(this.OnExpressionOpts);
            // 
            // Expressionator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 395);
            this.Controls.Add(this.grpExpOptions);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnEvaluate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtExpression);
            this.Name = "Expressionator";
            this.Text = "Simple Expression Calculator";
            this.grpExpOptions.ResumeLayout(false);
            this.grpExpOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip ExpressionTip;
        private System.Windows.Forms.Button btnEvaluate;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox grpExpOptions;
        private System.Windows.Forms.TextBox txtOpsAllowed;
        private System.Windows.Forms.RadioButton rbExtended;
        private System.Windows.Forms.RadioButton rbSimple;
        private System.Windows.Forms.RadioButton rbSimpleDecimal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNumsAllowed;
        private System.Windows.Forms.RadioButton rbExtendedDecimal;
    }
}

