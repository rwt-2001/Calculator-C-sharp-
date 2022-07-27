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
      
        private Label _labelBox;
        private SplitContainer _splitContainer;
        private TableLayoutPanel _tableLayoutPanel;


        /*Variables*/
        private Dictionary<string, ButtonInfo> _buttonsInfo;
        private Dictionary<string, Button> _buttons;
        private Dictionary<string, ButtonInfo> _specialButtonsInfo;
        private MemoryData _memoryData;
        private bool _binaryUsed = false;
        private bool _unaryUsed = false;
        private bool _dotUsed = false;
        private int _trackBrackets = 0;
        private string[] _unaryOperators;
        private ExpressionEvaluator _evaluator = new ExpressionEvaluator();
        
        /* Gather the button data */
        private void GetButtonInfo()
        {
            
            string content = File.ReadAllText(UIResources.BUTTONCONFIGFILEPATH);
            _buttonsInfo = JsonConvert.DeserializeObject<Dictionary<string, ButtonInfo>>(content);

            content = File.ReadAllText(UIResources.SPECIALBUTTONCONFIGFILEPATH);
            _specialButtonsInfo = JsonConvert.DeserializeObject<Dictionary<string, ButtonInfo>>(content);


            content = File.ReadAllText(UIResources.UNARYOPERATORS);
            _unaryOperators = JsonConvert.DeserializeObject<string[]>(content);
            

        }

        /*Adds button to the dictionary _numericButtons an to tableLayoutPanel*/
        private void AddButtonToTable()
        {
            /* Loop adds button for operators and operands */
            foreach(string buttonKey in _buttonsInfo.Keys)
            {
                ButtonInfo buttonInfo = _buttonsInfo[buttonKey];
                Button newButton = new Button();
                newButton.Name = buttonInfo.Name;
                newButton.Dock = DockStyle.Fill;
                newButton.Text = buttonInfo.Text;
                newButton.BackColor = Color.FromArgb(167,190,174);
                newButton.Font = new Font(UIResources.TEXTFONTSTYLE, 16);
                newButton.Click += AddToExpression;             
                _buttons[buttonInfo.Name] = newButton;
                this._tableLayoutPanel.Controls.Add(newButton,buttonInfo.LocationX,buttonInfo.LocationY);
            }

            /* Loop adds special button like reset memory buttons */
            foreach(string buttonKey in _specialButtonsInfo.Keys)
            {
                ButtonInfo buttonInfo = _specialButtonsInfo[buttonKey];
                Button newButton = new Button();
                newButton.Name = buttonInfo.Name;
                newButton.Dock = DockStyle.Fill;
                newButton.Text = buttonInfo.Text;
                newButton.BackColor = Color.FromArgb(167, 190, 174);
                newButton.Font = new Font(UIResources.TEXTFONTSTYLE, 16);
                _buttons[buttonInfo.Name] = newButton;
                this._tableLayoutPanel.Controls.Add(newButton, buttonInfo.LocationX, buttonInfo.LocationY);
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
                if (_labelBox.Text.Length != 0 && _labelBox.Text[_labelBox.Text.Length - 1].ToString() == UIResources.BRACKETRIGHT) return;
                if (_dotUsed && value == UIResources.DOT) return;
                if(value == UIResources.DOT) _dotUsed = true;
                _binaryUsed = false;
                this._labelBox.Text = this._labelBox.Text == UIResources.ZEROSTRING ? value : _labelBox.Text + value;
            }
            else if( isBracket )
            {
                if(isLeftBracket && _labelBox.Text == UIResources.ZEROSTRING)
                {
                    _labelBox.Text = value; 
                    _trackBrackets++;
                    return;
                }
                
                /* If vallue = UIResources.BRACKETRIGHT then it add it to expression */
                if (isRightBracket )
                {
                    if (_trackBrackets <= 0) return;
                    _trackBrackets--;
                    this._labelBox.Text = this._labelBox.Text == UIResources.ZEROSTRING ? value : _labelBox.Text + value;
                    return;
                }

                /* checks whether LeftBracket is not getting input after an operand  OR a Dot ( . ) */
                else if (!((_labelBox.Text.Length != 0 && _labelBox.Text[_labelBox.Text.Length - 1].ToString() == UIResources.BRACKETLEFT) || _binaryUsed)
                        || _labelBox.Text.Length != 0 && _labelBox.Text[_labelBox.Text.Length - 1].ToString() == UIResources.DOT) return;
                else
                {
                    _trackBrackets++;
                    _binaryUsed = false;
                    _unaryUsed = false;
                    this._labelBox.Text = this._labelBox.Text == UIResources.ZEROSTRING ? value : _labelBox.Text + value;
                }
               
            }
            else if ( !isNumeric && value!=UIResources.DOT )

            {
                if (_binaryUsed)
                {
                    string preValue = this._labelBox.Text;
                    this._labelBox.Text = preValue.Substring(0, preValue.Length - 2) + value + UIResources.SPACE;
                    return;
                }
                /* If ( is on the left then don't add operator */
                if (_labelBox.Text.Length != 0 && _labelBox.Text[_labelBox.Text.Length - 1].ToString() == UIResources.BRACKETLEFT) return;
                bool isUnary = _unaryOperators.Contains(value);
                this._labelBox.Text += isUnary ? value +  UIResources.SPACE : UIResources.SPACE + value + UIResources.SPACE;
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
                result = _evaluator.Evaluate(this._labelBox.Text);
                this._labelBox.Text = result.ToString();
                ReInititalizeValues();
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
                Reset(sender,e);
            }
        }
        private void Reset(object sender, EventArgs e)
        {
            _labelBox.Text = UIResources.ZEROSTRING;
            ReInititalizeValues();
        }

   
        private void ReInititalizeValues()
        {
            _binaryUsed = false;
            _unaryUsed = false;
            _dotUsed = false;
            _trackBrackets = 0;
        }
        /* Copy and paste functionality */
        private void MemorySave(object sender, EventArgs e)
        {
            string data = this._labelBox.Text;
            this._memoryData.SetMemory(data, _binaryUsed, _unaryUsed, _dotUsed, _trackBrackets);
        }
        private void MemoryRead(object sender, EventArgs e )
        {
            if (this._memoryData.data == string.Empty) return;
            this._binaryUsed = _memoryData._binaryUsed;
            this._unaryUsed = _memoryData._unaryUsed;
            this._dotUsed = _memoryData._dotUsed;
            this._trackBrackets =this. _memoryData._trackBrackets;
            this._labelBox.Text = this._labelBox.Text==UIResources.ZEROSTRING ? this._memoryData.data : this._labelBox.Text + this._memoryData.data;

        }
        public Form1()
        {
            _buttonsInfo = new Dictionary<string, ButtonInfo>();
            _buttons = new Dictionary<string, Button>();
            _specialButtonsInfo =  new Dictionary<string, ButtonInfo>();
            _memoryData = new MemoryData();
            GetButtonInfo();
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            
            this._tableLayoutPanel = new TableLayoutPanel();
            this._labelBox = new Label();
            this._splitContainer = new SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // _labelBox
            // 
            this._labelBox.Dock = DockStyle.Fill;
            this._labelBox.Font = new Font(UIResources.TEXTFONTSTYLE, 25);
            this._labelBox.Location = new Point(0, 0);
            this._labelBox.Name = "_labelBox";
            this._labelBox.Size = new Size(380, 100);
            this._labelBox.TabIndex = 25;
            this._labelBox.Text = "0";
            this._labelBox.TextAlign = ContentAlignment.BottomRight;

            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = DockStyle.Fill;
            this._splitContainer.IsSplitterFixed = true;
            this._splitContainer.Location = new Point(0, 0);
            this._splitContainer.Name = "_splitContainer";
            this._splitContainer.Orientation = Orientation.Horizontal;

            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._labelBox);

            //
            // _tableLayoutPanel
            // 
            this._tableLayoutPanel.ColumnCount = 5;
            this._tableLayoutPanel.Location = new Point(0, 0);
            this._tableLayoutPanel.Name = "_tableLayoutPanel";
            this._tableLayoutPanel.Dock = DockStyle.Fill;
            this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            this._tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            this._tableLayoutPanel.RowCount = 6;
            this._tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            this._tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            this._tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            this._tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            this._tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            this._tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            this._tableLayoutPanel.TabIndex = 26;

            /* Add button to table */
            AddButtonToTable();
            _buttons[UIResources.SHOWRESULT].Click += GetResult;
            _buttons[UIResources.BUTTONRESET].Click += Reset;
            _buttons[UIResources.MEMORYREAD].Click += MemoryRead;
            _buttons[UIResources.MEMORYSAVE].Click += MemorySave;
            // _splitContainer.Panel2

            this._splitContainer.Panel2.Controls.Add(this._tableLayoutPanel);
            this._splitContainer.Size = new Size(380, 450);
            this._splitContainer.SplitterDistance = 100;
            this._splitContainer.SplitterWidth = 1;
            this._splitContainer.TabIndex = 27;
             
             //Form1
             
            this.ClientSize = new Size(380, 450);
            this.Controls.Add(this._splitContainer);
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Name = "Form1";
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            
        }
    }
}
