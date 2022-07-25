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
        
        private bool _binaryUsed = false;
        private bool _unaryUsed = false;
        private bool _dotUsed = false;
        private int _trackBrackets = 0;
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
            bool isBracket = (value == UIResources.BRACKETLEFT || value == UIResources.BRACKETRIGHT); 
            bool isRightBracket = (value == UIResources.BRACKETRIGHT);
            bool isLeftBracket = (value == UIResources.BRACKETLEFT);
            if ((isNumeric || value == UIResources.DOT) && !_unaryUsed )
            {
                if (_textBox.Text.Length != 0 && _textBox.Text[_textBox.Text.Length - 1].ToString() == UIResources.BRACKETRIGHT) return;
                if (_dotUsed && value == UIResources.DOT) return;
                if(value == UIResources.DOT) _dotUsed = true;
                _binaryUsed = false;
                this._textBox.Text = this._textBox.Text == UIResources.ZEROSTRING ? value : _textBox.Text + value;
            }
            else if( isBracket )
            {
                if(isLeftBracket && _textBox.Text == UIResources.ZEROSTRING)
                {
                    _textBox.Text = value; 
                    _trackBrackets++;
                    return;
                }
                
                /* If vallue = UIResources.BRACKETRIGHT then it add it to expression */
                if (isRightBracket )
                {
                    if (_trackBrackets <= 0) return;
                    _trackBrackets--;
                    this._textBox.Text = this._textBox.Text == UIResources.ZEROSTRING ? value : _textBox.Text + value;
                    return;
                }

                /* checks whether LeftBracket is not getting input after an operand  OR a Dot ( . ) */
                else if (!((_textBox.Text.Length != 0 && _textBox.Text[_textBox.Text.Length - 1].ToString() == UIResources.BRACKETLEFT) || _binaryUsed)
                        || _textBox.Text.Length != 0 && _textBox.Text[_textBox.Text.Length - 1].ToString() == UIResources.DOT) return;
                else
                {
                    _trackBrackets++;
                    _binaryUsed = false;
                    _unaryUsed = false;
                    this._textBox.Text = this._textBox.Text == UIResources.ZEROSTRING ? value : _textBox.Text + value;
                }
               
            }
            else if (!_binaryUsed && !isNumeric && value!=UIResources.DOT && !isBracket )
            {
                /* If ( is on the left then don't add operator */
                if (_textBox.Text.Length != 0 && _textBox.Text[_textBox.Text.Length - 1].ToString() == UIResources.BRACKETLEFT) return;
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
            _trackBrackets = 0;
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
            this.Controls.Add(this._textBox);
            this.Name = UIResources.FORMNAME;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
