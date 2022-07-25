using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Math.Operations;
namespace Calculator_UI
{
    public partial class Form1 : Form
    {

        private TextBox _textBox;
        private Dictionary<string, ButtonInfo> _buttonsInfo = new Dictionary<string, ButtonInfo>();
        private Dictionary<string, Button> _buttons = new Dictionary<string, Button>();
        private Button _testButton;
        private bool _binaryUsed = false;
        private bool _unaryUsed = false;
        private bool _dotUsed = false;
        private string[] _unaryOperators;
        private ExpressionEvaluator _evaluator = new ExpressionEvaluator();

        private void GetButtonInfo()
        {
            string content = File.ReadAllText(UIResources.BUTTONCONFIGFILEPATH);
            _buttonsInfo = JsonConvert.DeserializeObject<Dictionary<string, ButtonInfo>>(content);
            content = File.ReadAllText(UIResources.UNARYOPERATORS);
            _unaryOperators = JsonConvert.DeserializeObject<string[]>(content);

        }

        /*Adds button to the dictionary _numericButtons*/
        private void CreateButtonInstance()
        {
            foreach(string buttonKey in _buttonsInfo.Keys)
            {
                ButtonInfo buttonInfo = _buttonsInfo[buttonKey];
                Button newButton = new Button();
                newButton.Name = buttonInfo.Name;
                newButton.Size = new Size(buttonInfo.Width, buttonInfo.Height);
                newButton.Location = new Point(buttonInfo.LocationX, buttonInfo.LocationY);
                newButton.Text = buttonInfo.Text;
                newButton.BackColor = Color.FromArgb(167,190,174);
                newButton.Font = new Font(UIResources.TEXTFONTSTYLE, 16);
                newButton.Click += AddToExpression;
                _buttons[buttonInfo.Name] = newButton; 
            }
            


        }
        /* Add buttons to layout */
        private void AddButtonToLayout()
        {
            foreach (string buttonKey in _buttons.Keys)
            {
                this.Controls.Add(_buttons[buttonKey]);
            }
        }
        private void AddToExpression(object sender, EventArgs e)
        {
            string value = (sender as Button).Text;
            bool isNumeric = double.TryParse(value, out double numeric);
            if ((isNumeric || value == UIResources.DOT) && !_unaryUsed )
            {
                if (_dotUsed && value == UIResources.DOT) return;
                if(value == UIResources.DOT) _dotUsed = true;
                _binaryUsed = false;
                this._textBox.Text = this._textBox.Text == UIResources.ZEROSTRING ? value : _textBox.Text + value;
            }
            if(value == "(" || value == ")" && !_binaryUsed)
            {
                if (_textBox.Text.Length != 0 && _textBox.Text[_textBox.Text.Length - 1].ToString() == UIResources.DOT) return;
               
                _binaryUsed = false;
                _unaryUsed = false;
                this._textBox.Text = this._textBox.Text == UIResources.ZEROSTRING ? value : _textBox.Text + value;
            }
            else if (!_binaryUsed && !isNumeric && value!=UIResources.DOT)
            {
                bool isUnary = _unaryOperators.Contains(value);
                this._textBox.Text += isUnary ? value +  UIResources.SPACE : UIResources.SPACE + value + UIResources.SPACE;
                _binaryUsed = isUnary?false:true;
                _unaryUsed = isUnary?true:false;
                _dotUsed = false;
            }
            
        }
        private void GetResult(object sender, EventArgs e)
        {
            double result;
            try
            {
                result = _evaluator.Evaluate(this._textBox.Text);
                this._textBox.Text = result.ToString();
                _binaryUsed = false;
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void Reset(object sender, EventArgs e)
        {
            _textBox.Text = UIResources.ZEROSTRING;
            _binaryUsed = false;
            _unaryUsed = false;
            _dotUsed = false;
        }
        public Form1()
        {
            GetButtonInfo();
            CreateButtonInstance();
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this._textBox = new TextBox();
            this._testButton = new Button();
            this.SuspendLayout();
            
            // 
            // _textBox
            // 
            this._textBox.Font = new Font(UIResources.TEXTFONTSTYLE, 21F);
            this._textBox.Location = new Point(14, 15);
            this._textBox.Name = "_textBox";
            this._textBox.Size = new Size(350, 39);
            this._textBox.TabIndex = 12;
            this._textBox.Text = UIResources.ZEROSTRING;
            this._textBox.TextAlign = HorizontalAlignment.Right;

            // 
            // _testButton
            // 
            this._testButton.Location = new Point(164, 199);
            this._testButton.Name = "_testButton";
            this._testButton.Size = new Size(68, 44);
            this._testButton.TabIndex = 1;

            /* Remove the event handler AddToExpression from special buttons */
            _buttons[UIResources.SHOWRESULT].Click -= AddToExpression;
            _buttons[UIResources.BUTTONUNDO].Click -= AddToExpression;
            _buttons[UIResources.BUTTONRESET].Click -= AddToExpression;
            

            _buttons[UIResources.SHOWRESULT].Click += GetResult;
            _buttons[UIResources.BUTTONRESET].Click += Reset;
            // 
            // Form1
            // 
            AddButtonToLayout();
            this.BackColor = Color.FromArgb(231,232,209);
            this.ClientSize = new Size(380, 355);
            this.Controls.Add(this._testButton);
            this.Controls.Add(this._textBox);
            this.Name = UIResources.FORMNAME;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
