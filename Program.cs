using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace Private_Launcher
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    class MainForm : Form
    {
        private Label label1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label2;
        private Button button1;
        private PictureBox pictureBox1;
        private Button deleteSecureIdButton;
        private Button toggleSecureIdButton;
        private ComboBox serverIpComboBox;
        private Label serverIpLabel;
        private Button deleteServerIpButton;
        private Label disclaimerLabel;

        private const string SERVER_IP_FILE = "serverip.txt";
        private const string SECURE_ID_FILE = "secureid.txt";
        private const string FOLDER_PATH_FILE = "folderpath.txt";
        private const string LOGO_URL = "https://worldclassservers.net/filebox/h1emulogo.png";

        public MainForm()
        {
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Color backgroundColor = Color.FromArgb(28, 27, 34);

            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 200;
            headerPanel.BackColor = backgroundColor;

            disclaimerLabel = new Label();
            disclaimerLabel.Text = "H1Emu is not affiliated with H1Z1.com, H1Z1: Just Survive or Daybreak Game Company LLC, H1Z1 and the H1Z1 logo are trademarks of the Daybreak Game Company LLC";
            disclaimerLabel.Location = new Point(10, 450);
            disclaimerLabel.AutoSize = false;
            disclaimerLabel.Size = new Size(780, 50);
            disclaimerLabel.TextAlign = ContentAlignment.MiddleCenter;
            disclaimerLabel.ForeColor = Color.White;
            Controls.Add(disclaimerLabel);



            // Create and set up the logo picture box
            pictureBox1 = new PictureBox();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Load(LOGO_URL);
            pictureBox1.Dock = DockStyle.Fill; // Set the Dock property to Fill
            headerPanel.Controls.Add(pictureBox1);

            label1 = new Label();
            label1.Text = "Select Your H1z1 Game Folder:";
            label1.ForeColor = Color.White;
            label1.Location = new Point(240, 220); // Changed Y position to 220
            label1.AutoSize = true;

            textBox1 = new TextBox();
            textBox1.Location = new Point(240, 243); // Changed Y position to 243
            textBox1.Size = new Size(241, 20);
            textBox1.ReadOnly = true;

            Button browseButton = new Button();
            browseButton.Text = "Browse";
            browseButton.Location = new Point(500, 242); // Changed Y position to 242
            browseButton.Size = new Size(75, 23);
            browseButton.Click += new EventHandler(browseButton_Click);
            browseButton.BackColor = Color.WhiteSmoke;


            label2 = new Label();
            label2.Text = "Enter a Secure ID:";
            label2.Location = new Point(240, 271); // Changed Y position to 271
            label2.AutoSize = true;
            label2.ForeColor = Color.White;

            toggleSecureIdButton = new Button();
            toggleSecureIdButton.Text = "Show Secure ID";
            toggleSecureIdButton.ForeColor = Color.White;
            toggleSecureIdButton.Location = new Point(500, 270);
            toggleSecureIdButton.Size = new Size(100, 23);
            toggleSecureIdButton.Click += new EventHandler(toggleSecureIdButton_Click);
            toggleSecureIdButton.BackColor = Color.DarkGreen;
            Controls.Add(toggleSecureIdButton);


            textBox2 = new TextBox();
            textBox2.Location = new Point(340, 271); // Changed Y position to 271
            textBox2.Size = new Size(140, 10);
            textBox2.UseSystemPasswordChar = true; // Mask the text by default




            button1 = new Button();
            button1.Text = "Launch";
            button1.Location = new Point(650, 400); // Changed Y position to 295
            button1.Size = new Size(110, 50);
            button1.Click += new EventHandler(button1_Click);
            button1.Enabled = false;
            button1.BackColor = Color.DarkGreen;


            // Load Secure ID from file
            if (File.Exists(SECURE_ID_FILE))
            {
                textBox2.Text = File.ReadAllText(SECURE_ID_FILE);
                textBox2.Enabled = false;

                deleteSecureIdButton = new Button();
                deleteSecureIdButton.Text = "Delete Secure ID";
                deleteSecureIdButton.Location = new Point(340, 305);
                deleteSecureIdButton.Size = new Size(110, 50);
                deleteSecureIdButton.Click += new EventHandler(deleteSecureIdButton_Click);
                deleteSecureIdButton.BackColor = Color.DarkRed;
                Controls.Add(deleteSecureIdButton);
                button1.Enabled = true;
            }

            // Load folder path from file
            if (File.Exists(FOLDER_PATH_FILE))
            {
                textBox1.Text = File.ReadAllText(FOLDER_PATH_FILE);
                button1.Enabled = true;
            }

            serverIpLabel = new Label();
            serverIpLabel.Text = "Enter Server IP:Port:";
            serverIpLabel.Location = new Point(220, 380);
            serverIpLabel.AutoSize = true;
            serverIpLabel.ForeColor = Color.White;
            Controls.Add(serverIpLabel);

            serverIpComboBox = new ComboBox();
            serverIpComboBox.Location = new Point(340, 380);
            serverIpComboBox.Size = new Size(140, 10);
            serverIpComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            serverIpComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            serverIpComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            Controls.Add(serverIpComboBox);

            deleteServerIpButton = new Button();
            deleteServerIpButton.Text = "Delete Server IP";
            deleteServerIpButton.Location = new Point(340, 405);
            deleteServerIpButton.Size = new Size(110, 23);
            deleteServerIpButton.Click += new EventHandler(deleteServerIpButton_Click);
            deleteServerIpButton.BackColor = Color.DarkRed;
            Controls.Add(deleteServerIpButton);


            // Load server IPs from file
            if (File.Exists(SERVER_IP_FILE))
            {
                var ips = File.ReadAllLines(SERVER_IP_FILE);
                serverIpComboBox.Items.AddRange(ips);
                if (ips.Length > 0)
                {
                    serverIpComboBox.SelectedIndex = 0;
                }
            }



            ClientSize = new Size(800, 500);
            Controls.Add(browseButton);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(headerPanel);
            Text = "Launch H1Z1";
            BackColor = backgroundColor;

        }

        private void deleteServerIpButton_Click(object sender, EventArgs e)
        {
            if (serverIpComboBox.SelectedIndex != -1)
            {
                if (MessageBox.Show("Are you sure you want to delete the selected Server IP?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string serverIP = serverIpComboBox.Text;
                    serverIpComboBox.Items.RemoveAt(serverIpComboBox.SelectedIndex);

                    // Update server IP file
                    List<string> ips = File.ReadAllLines(SERVER_IP_FILE).ToList();
                    ips.Remove(serverIP);
                    File.WriteAllLines(SERVER_IP_FILE, ips);
                }
            }
            else
            {
                MessageBox.Show("No Server IP is selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toggleSecureIdButton_Click(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar)
            {
                textBox2.UseSystemPasswordChar = false;
                toggleSecureIdButton.Text = "Hide Secure ID";
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                toggleSecureIdButton.Text = "Show Secure ID";
            }
        }
        private void deleteSecureIdButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the Secure ID file this Action cannot be Reversed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (File.Exists(SECURE_ID_FILE))
                {
                    File.Delete(SECURE_ID_FILE);
                    textBox2.Text = "";
                    textBox2.Enabled = true;
                    Controls.Remove(deleteSecureIdButton);
                    button1.Enabled = false;
                }
            }
        }
        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.Description = "Select the folder containing the H1Z1.exe file.";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
                button1.Enabled = true;

                // Save folder path to file
                File.WriteAllText(FOLDER_PATH_FILE, textBox1.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string folderPath = textBox1.Text;
            string sessionID = textBox2.Text;
            string serverIP = serverIpComboBox.Text;

            Console.WriteLine("Selected folder path: " + folderPath);

            if (Directory.Exists(folderPath))
            {
                // Save Secure ID to file
                File.WriteAllText(SECURE_ID_FILE, sessionID);

                // Save Server IP to file if it's not already in the list
                if (!serverIpComboBox.Items.Contains(serverIP))
                {
                    File.AppendAllText(SERVER_IP_FILE, serverIP + Environment.NewLine);
                    serverIpComboBox.Items.Add(serverIP);
                }

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $"/C \"cd /d {folderPath} && H1Z1.exe inifile=ClientConfig.ini providerNamespace=soe sessionid={sessionID} CasSessionId=1234 InternationalizationLocale=en_US LaunchPadUfp={{fingerprint}} LaunchPadSessionId=0 STEAM_ENABLED=0 Server={serverIP}\"";
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = true;

                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show("Invalid folder selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
