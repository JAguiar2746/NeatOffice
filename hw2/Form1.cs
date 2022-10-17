using System.Windows.Forms.Design;
using static System.Net.Mime.MediaTypeNames;

namespace hw2
{
    public partial class Form1 : Form
    {
        Double result = 0;
        String operation = "";
        bool enterValue = false;
        String firstnum, secondnum;
        bool num_clicked = false;
        String filePath = string.Empty;
        private GraphAlgorithms g;

        public Form1()
        {
            InitializeComponent();
            g = new GraphAlgorithms(pBar, pLabel, statusStrip2);
        }

        private void openTXT_click(object sender, EventArgs e)
        {
            OpenFileDialog openText = new OpenFileDialog();
            openText.Filter = "txt files (*.txt) | *.txt";
            
            if(openText.ShowDialog() == DialogResult.OK)
            {
                String filename = openText.FileName.ToString();
                g.ReadGraphFromTXTFile(filename);
                listBox1.Items.Insert(0, openText.FileName);
            }
        }

        private void openCSV_Click(object sender, EventArgs e)
        {
            OpenFileDialog openText = new OpenFileDialog();
            openText.Filter = "csv files(*.csv)| *.csv";

            if (openText.ShowDialog() == DialogResult.OK)
            {               
                String filename = openText.FileName.ToString();
                g.ReadGraphFromCSVFile(filename);
                listBox1.Items.Insert(0,openText.FileName);
            }
        }
        private void openMultiple_Click(object sender, EventArgs e)
        {
            OpenFileDialog openText = new OpenFileDialog();
            openText.Filter = "All Supported Files (*.txt,*.csv)| *.csv;*.txt|Text files (*.txt)|*.txt|CSV Files (*.csv)|*.csv";
            openText.Multiselect = true;

            if (openText.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in openText.FileNames)
                {
                    
                    if (file.EndsWith(".txt"))
                    {
                        g.ReadGraphFromTXTFile(file);
                    }
                    else if(file.EndsWith(".csv"))
                    {
                        g.ReadGraphFromCSVFile(file);
                    }
                    listBox1.Items.Insert(0, file);
                }

            }
        }

        private void deleteSelected_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void deleteAll_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void MST_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                string filename = listBox1.SelectedItem.ToString();
                g.GetMST(filename);
                listBox2.Items.Add("MST:" + filename);                
            }else{
                MessageBox.Show("No Item Selected", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void Dijkstra_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                string filename = listBox1.SelectedItem.ToString();
                g.Dijkstra(filename);
                listBox2.Items.Add("Shortest Paths:" + filename);
            }
            else
            {
                MessageBox.Show("No Item Selected", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void save_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                string file = listBox2.SelectedItem.ToString();
                SaveFileDialog save = new SaveFileDialog();
                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (file.StartsWith("MST:"))
                    {
                        file = file.Substring(4);
                        g.WriteMSTSolutionTo(save.FileName, file);
                        listBox2.Items.Remove(listBox2.SelectedItem);
                    }
                    else if (file.StartsWith("Shortest Paths:"))
                    {
                        file = file.Substring(15);
                        g.WriteSSSPSolutionTo(save.FileName, file);
                        listBox2.Items.Remove(listBox2.SelectedItem);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Item Selected", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void numButtons(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            num_clicked = true;

            if ((displayText.Text == "0") || (enterValue))
                displayText.Text = "";
            enterValue = false;

            if (b.Text == ".")
            {
                if (!displayText.Text.Contains("."))
                    displayText.Text = displayText.Text + b.Text;
            }
            else
            {
                displayText.Text = displayText.Text + b.Text;
            }
        }

        private void opButtons(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (result != 0)
            {
                equals_btn.PerformClick();
                enterValue = true;
                operation = b.Text;
                showLabel.Text = System.Convert.ToString(result) + " " + operation;
            }
            else
            {
                operation = b.Text;
                result = Double.Parse(displayText.Text);
                displayText.Text = "";
                showLabel.Text = System.Convert.ToString(result) + " " + operation;
            }
            firstnum = showLabel.Text;
        }


        private void c_btn_Click(object sender, EventArgs e)
        {
            displayText.Text = "0";
            result = 0;
            showLabel.Text = "";
            firstnum = "";
            secondnum = "";
        }

        private void ce_btn_Click(object sender, EventArgs e)
        {
            displayText.Text = "0";
            secondnum = "0";
        }

        private void equals_btn_Click(object sender, EventArgs e)
        {
            if (num_clicked)
            {
                secondnum = displayText.Text;
                showLabel.Text = "";
                switch (operation)
                {
                    case "+":
                        displayText.Text = (result + Double.Parse(displayText.Text)).ToString();
                        break;
                    case "-":
                        displayText.Text = (result - Double.Parse(displayText.Text)).ToString();
                        break;
                    case "*":
                        displayText.Text = (result * Double.Parse(displayText.Text)).ToString();
                        break;
                    case "/":
                        displayText.Text = (result / Double.Parse(displayText.Text)).ToString();
                        break;
                    case "%":
                        displayText.Text = (result / 100).ToString();
                        break;
                    case "^":
                        double i = Double.Parse(displayText.Text);
                        double q;
                        q = result;
                        displayText.Text = Math.Pow(q, i).ToString();
                        break;
                    default:
                        break;
                }
                result = Double.Parse(displayText.Text);
                operation = "";

                
                displayHistory_rtb.AppendText(firstnum + " " + secondnum + " = " + "\n");
                displayHistory_rtb.AppendText("\t" + displayText.Text + "\n");
                

            }
        }       

        private void square_btn_Click(object sender, EventArgs e)
        {
            Double num;
            showLabel.Text = System.Convert.ToString("(" + (displayText.Text) + ")^2" + " = " + "\n");
            num = Convert.ToDouble(displayText.Text) * Convert.ToDouble(displayText.Text);
            displayText.Text = System.Convert.ToString(num);
            displayHistory_rtb.AppendText((showLabel.Text));
            displayHistory_rtb.AppendText("\t" + num + "\n");
            
        }

        private void sqrt_btn_Click(object sender, EventArgs e)
        {
            double sq = Double.Parse(displayText.Text);
            showLabel.Text = System.Convert.ToString("sqrt" + "(" + (displayText.Text) + ")" + " = " + "\n");
            sq = Math.Sqrt(sq);
            displayText.Text = System.Convert.ToString(sq);
            displayHistory_rtb.AppendText((showLabel.Text));
            displayHistory_rtb.AppendText("\t" + sq + "\n");
            
        }

        private void exponent_btn_Click(object sender, EventArgs e)
        {
            ToolStripButton b = (ToolStripButton)sender;

            if (result != 0)
            {
                equals_btn.PerformClick();
                enterValue = true;
                operation = b.Text;
                showLabel.Text = System.Convert.ToString(result) + " " + operation;
            }
            else
            {
                operation = b.Text;
                result = Double.Parse(displayText.Text);
                displayText.Text = "";
                showLabel.Text = System.Convert.ToString(result) + " " + operation;
            }

            firstnum = showLabel.Text;

        }

        private void LOG_btn_Click(object sender, EventArgs e)
        {
            double log = Double.Parse(displayText.Text);
            showLabel.Text = System.Convert.ToString("log" + "(" + (displayText.Text) + ")" + " = " + "\n");
            log = Math.Log(log);
            displayText.Text = System.Convert.ToString(log);
            displayHistory_rtb.AppendText((showLabel.Text));
            displayHistory_rtb.AppendText("\t" + log + "\n");
            
        }

        private void sin_btn_Click(object sender, EventArgs e)
        {
            double sin = Double.Parse(displayText.Text);
            showLabel.Text = System.Convert.ToString("sin" + "(" + (displayText.Text) + ")" + " = " + "\n");
            sin = Math.Sin(sin);
            displayText.Text = System.Convert.ToString(sin);
            displayHistory_rtb.AppendText((showLabel.Text));
            displayHistory_rtb.AppendText("\t" + sin + "\n");
            
        }

        private void cos_btn_Click(object sender, EventArgs e)
        {
            double cos = Double.Parse(displayText.Text);
            showLabel.Text = System.Convert.ToString("cos" + "(" + (displayText.Text) + ")" + " = " + "\n");
            cos = Math.Cos(cos);
            displayText.Text = System.Convert.ToString(cos);
            displayHistory_rtb.AppendText((showLabel.Text));
            displayHistory_rtb.AppendText("\t" + cos + "\n");
            
        }

        private void tan_btn_Click(object sender, EventArgs e)
        {
            double tan = Double.Parse(displayText.Text);
            showLabel.Text = System.Convert.ToString("tan" + "(" + (displayText.Text) + ")" + " = " + "\n");
            tan = Math.Tan(tan);
            displayText.Text = System.Convert.ToString(tan);
            displayHistory_rtb.AppendText((showLabel.Text));
            displayHistory_rtb.AppendText("\t" + tan + "\n");
            
        }

        private void sign_btn_Click(object sender, EventArgs e)
        {
            Double num;
            showLabel.Text = System.Convert.ToString((displayText.Text) + "\n");
            num = Convert.ToDouble(displayText.Text) * -1;
            displayText.Text = System.Convert.ToString(num);
            displayHistory_rtb.AppendText((showLabel.Text));
            displayHistory_rtb.AppendText("\t" + num + "\n");
            
        }

        private void saveHistory_btn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = saveFile.FileName;
            }
            System.IO.File.WriteAllText(filePath, displayHistory_rtb.Text.Replace("\n", Environment.NewLine));
        }

        private void backspace_btn_Click(object sender, EventArgs e)
        {
            if (displayText.Text.Length > 0)
            {
                displayText.Text = displayText.Text.Remove(displayText.Text.Length - 1, 1);
            }

            if (displayText.Text == "")
            {
                displayText.Text = "0";
            }
        }

        private void inverse_btn_Click(object sender, EventArgs e)
        {
            Double num;
            showLabel.Text = System.Convert.ToString("1 / "+(displayText.Text) + "\n");
            num = 1/Convert.ToDouble(displayText.Text);
            displayText.Text = System.Convert.ToString(num);
            displayHistory_rtb.AppendText((showLabel.Text));
            displayHistory_rtb.AppendText("\t" + num + "\n");
        }

        private void calculatorColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDiag = new ColorDialog();
            if (colorDiag.ShowDialog() == DialogResult.OK)
            {
                splitContainer2.Panel1.BackColor = colorDiag.Color;
            }
        }

        private void dayCounterColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDiag = new ColorDialog();
            if (colorDiag.ShowDialog() == DialogResult.OK)
            {
                splitContainer2.Panel2.BackColor = colorDiag.Color;
            }
        }

        private void graphColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDiag = new ColorDialog();
            if (colorDiag.ShowDialog() == DialogResult.OK)
            {
                splitContainer1.Panel2.BackColor = colorDiag.Color;
            }
        }

        private void calcFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDiag = new FontDialog();
            if(fontDiag.ShowDialog() == DialogResult.OK)
            {
                displayText.Font = fontDiag.Font;
                showLabel.Font = fontDiag.Font;
            }
        }

        private void about_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Brought to you by:\nJose Aguiar & Ryan Neubert", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void calcHist_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if(open.ShowDialog() == DialogResult.OK)
            {
                showLabel.Text = File.ReadAllText(open.FileName);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            decimal count = numericUpDown1.Value;
            if (count > 0)
                toDate.Value = fromDate.Value.AddDays((double)count);
            else if (count < 0)
                fromDate.Value = toDate.Value.AddDays((double)-count);
            else
                toDate.Value = fromDate.Value;
        }

        private void date_valueChanged(object sender, EventArgs e)
        {
            DateTime to = toDate.Value;
            DateTime from = fromDate.Value;            
            numericUpDown1.Value = to.Subtract(from).Days;
        }

        private void history_clear_btn_Click(object sender, EventArgs e)
        {
            displayHistory_rtb.Clear();            
        }
    }
}