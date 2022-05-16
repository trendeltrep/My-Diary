using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;


namespace DiaryProjext
{
    public partial class FormMain : Form
    {
        public List<Button> Buttons = new List<Button>();
        public FormMain()
        {
            InitializeComponent();
            BackColor =Color.RoyalBlue;
            Form1 toCheckFile = new Form1();
            Database.GetData();
            createEvents();
            buttonCreate.BackColor = Color.Yellow;
            buttonCreate.Click += new EventHandler(buttonCreate_Click);            
        }
                
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.FormClosing += F1_FormClosing;
            f1.ShowDialog();           
        }

        private void F1_FormClosing(object sender, FormClosingEventArgs e)
        {
            createEvents();
        }
        public void createEvents()
        {
            Controls.Clear();
            Buttons.Clear();
            buttonCreate.Location = new Point(550, 12);
            Controls.Add(buttonCreate);
            Database.SortDatabaseEventsByTime();

            int today = 0;
            int tomorrow= 0;
            int thisWeek= 0;
            int nextWeek= 0;
            int overThanWeek= 0;
            int finished= 0;

            int newHelpINT = 0;

            for (int i = 0; i < Database.expired.Count; i++)
            {
                Label boolName = new Label()
                {
                    Text = "Просрочені",
                    Location = new Point(12, 10),
                    AutoSize = true,
                };
                Controls.Add(boolName);

                Button button = new Button()
                {
                    Name = $"{i}",
                    Size = new Size(500, 55),
                    Location = new Point(10, (i) * 60 + 30),
                    BackColor = Color.LightGray,
                    Tag = Name,
                };
                button.Click += updateEvents;

                Label name = new Label()
                {
                    Text = $"{Database.expired[i].name}",
                    AutoSize = true,
                    Location = new Point(40, 35 + i * 60),
                    BackColor = Color.LightGray,
                };
                Label place = new Label()
                {
                    Text = $"{Database.expired[i].place}",
                    AutoSize = true,
                    Location = new Point(40, 50 + i * 60),
                    BackColor = Color.LightGray,
                };
                Label time = new Label()
                {
                    Text = $"{Database.expired[i].timeStart.ToUniversalTime().ToString("r")}, Тривалість: {Database.expired[i].howLong.ToString("HH:mm:ss")} ",
                    AutoSize = true,
                    Location = new Point(40, 65 + i * 60),
                    BackColor = Color.LightGray,
                };
                CheckBox checkBox = new CheckBox()
                {
                    Name = $"{i }",
                    Location = new Point(23, i * 60 + 50),
                    AutoSize = true,
                    BackColor = Color.LightGray,
                    Tag = Name,
                    Checked = Database.expired[i].check
                };

                checkBox.CheckStateChanged += checkBox_CheckStateChanged;

                Controls.Add(name);
                Controls.Add(place);
                Controls.Add(time);
                Controls.Add(checkBox);
                Controls.Add(button);
                Buttons.Add(button);
            }
            
            if (Database.expired.Count == 0)
            {
                today = -20;
            }           
            for (int i = 0; i < Database.today.Count; i++)
            {
                int helpInt = Database.expired.Count;
                Label boolName2 = new Label()
                {
                    Text = "Сьогодні",
                    Location = new Point(12, 30 + 60 * (helpInt) + today),//35 + 60
                    AutoSize = true,
                };
                Controls.Add(boolName2);

                Button button = new Button()
                {
                    Name = $"{i + helpInt}",
                    Size = new Size(500, 55),
                    Location = new Point(10, (i + helpInt) * 60 + 50 + today) ,
                    BackColor = Color.LightGray,
                    Tag = Name,
                };
                button.Click += updateEvents;

                Label name = new Label()
                {
                    Text = $"{Database.today[i].name}",
                    AutoSize = true,
                    Location = new Point(40, (i + 1 + helpInt) * 60 - 5 + today),
                    BackColor = Color.LightGray,

                };
                Label place = new Label()
                {
                    Text = $"{Database.today[i].place}",
                    AutoSize = true,
                    Location = new Point(40, 10 + (i + 1 + helpInt) * 60 + today),
                    BackColor = Color.LightGray,
                };
                Label time = new Label()
                {
                    Text = $"{Database.today[i].timeStart.ToUniversalTime().ToString("r")}, Тривалість: {Database.today[i].howLong.ToString("HH:mm:ss")} ",
                    AutoSize = true,
                    Location = new Point(40, 25 + (i + 1 + helpInt) * 60 + today),
                    BackColor = Color.LightGray,
                };
                CheckBox checkBox = new CheckBox()
                {
                    Name = $"{i + helpInt}",
                    Location = new Point(23, (i + 1 + helpInt) * 60 + 10 + today),
                    AutoSize = true,
                    BackColor = Color.LightGray,
                    Tag = Name,
                    Checked = Database.today[i].check

                };
                checkBox.CheckStateChanged += checkBox_CheckStateChanged;

                Controls.Add(name);
                Controls.Add(place);
                Controls.Add(time);
                Controls.Add(checkBox);
                Controls.Add(button);
                Buttons.Add(button);
            }


            if (Database.expired.Count == 0 || Database.today.Count == 0)
            {
                tomorrow = -20;
            }
            if (Database.expired.Count ==0&& Database.today.Count == 0)
            {
                tomorrow = -40;
            }        
            for (int i = 0; i < Database.tomorrow.Count; i++)
            {
                int helpInt = Database.expired.Count +Database.today.Count;
                Label boolName2 = new Label()
                {
                    Text = "Завтра",
                    Location = new Point(12, 30+20 + 60 * (helpInt) +tomorrow),//35 + 60
                    AutoSize = true,
                };
                Controls.Add(boolName2);

                Button button = new Button()
                {
                    Name = $"{i + helpInt}",
                    Size = new Size(500, 55),
                    Location = new Point(10, (i + helpInt) * 60 + 20+50 + tomorrow),
                    BackColor = Color.LightGray,
                    Tag = Name,
                };
                button.Click += updateEvents;

                Label name = new Label()
                {
                    Text = $"{Database.tomorrow[i].name}",
                    AutoSize = true,
                    Location = new Point(40, (i + 1 + helpInt) * 60+15 + tomorrow),
                    BackColor = Color.LightGray,

                };
                Label place = new Label()
                {
                    Text = $"{Database.tomorrow[i].place}",
                    AutoSize = true,
                    Location = new Point(40, 10 +20 + (i + 1 + helpInt) * 60 + tomorrow),
                    BackColor = Color.LightGray,
                };
                Label time = new Label()
                {
                    Text = $"{Database.tomorrow[i].timeStart.ToUniversalTime().ToString("r")}, Тривалість: {Database.tomorrow[i].howLong.ToString("HH:mm:ss")} ",
                    AutoSize = true,
                    Location = new Point(40, 25 + 20 + (i + 1 + helpInt) * 60 + tomorrow),
                    BackColor = Color.LightGray,
                };
                CheckBox checkBox = new CheckBox()
                {
                    Name = $"{i + helpInt}",
                    Location = new Point(23, (i + 1 + helpInt) * 60 + 10 + 20 + tomorrow),
                    AutoSize = true,
                    BackColor = Color.LightGray,
                    Tag = Name,
                    Checked = Database.tomorrow[i].check

                };
                checkBox.CheckStateChanged += checkBox_CheckStateChanged;

                Controls.Add(name);
                Controls.Add(place);
                Controls.Add(time);
                Controls.Add(checkBox);
                Controls.Add(button);
                Buttons.Add(button);
            }

            
            if (Database.expired.Count == 0) { newHelpINT++; }
            if (Database.today.Count == 0) { newHelpINT++; }
            if (Database.tomorrow.Count == 0) { newHelpINT++; }
            
            if (newHelpINT == 1) { thisWeek = -20; }
            if (newHelpINT == 2) { thisWeek = -40; }
            if (newHelpINT == 3) { thisWeek = -60; }
            for (int i = 0; i < Database.thisWeek.Count; i++)
            {
                int helpInt = Database.expired.Count + Database.today.Count + Database.tomorrow.Count;
                Label boolName2 = new Label()
                {
                    Text = "Цієї неділі",
                    Location = new Point(12, 30 + 40 + 60 * (helpInt)+thisWeek),//35 + 60
                    AutoSize = true,
                };
                Controls.Add(boolName2);

                Button button = new Button()
                {
                    Name = $"{i + helpInt}",
                    Size = new Size(500, 55),
                    Location = new Point(10, (i + helpInt) * 60 + 40 + 50 + thisWeek),
                    BackColor = Color.LightGray,
                    Tag = Name,
                };
                button.Click += updateEvents;

                Label name = new Label()
                {
                    Text = $"{Database.thisWeek[i].name}",
                    AutoSize = true,
                    Location = new Point(40, (i + 1 + helpInt) * 60 + 15+20 + thisWeek),
                    BackColor = Color.LightGray,

                };
                Label place = new Label()
                {
                    Text = $"{Database.thisWeek[i].place}",
                    AutoSize = true,
                    Location = new Point(40, 10 + 40 + (i + 1 + helpInt) * 60 + thisWeek),
                    BackColor = Color.LightGray,
                };
                Label time = new Label()
                {
                    Text = $"{Database.thisWeek[i].timeStart.ToUniversalTime().ToString("r")}, Тривалість: {Database.thisWeek[i].howLong.ToString("HH:mm:ss")} ",
                    AutoSize = true,
                    Location = new Point(40, 25 + 40 + (i + 1 + helpInt) * 60 + thisWeek),
                    BackColor = Color.LightGray,
                };
                CheckBox checkBox = new CheckBox()
                {
                    Name = $"{i + helpInt}",
                    Location = new Point(23, (i + 1 + helpInt) * 60 + 10 + 40 + thisWeek),
                    AutoSize = true,
                    BackColor = Color.LightGray,
                    Tag = Name,
                    Checked = Database.thisWeek[i].check

                };
                checkBox.CheckStateChanged += checkBox_CheckStateChanged;

                Controls.Add(name);
                Controls.Add(place);
                Controls.Add(time);
                Controls.Add(checkBox);
                Controls.Add(button);
                Buttons.Add(button);
            }

            
            if(Database.thisWeek.Count == 0) { newHelpINT++; }
            
            if (newHelpINT == 1) { nextWeek= -20; }
            if (newHelpINT == 2) { nextWeek = -40; }
            if (newHelpINT == 3) { nextWeek= -60; }
            if (newHelpINT == 4) { nextWeek= -80; }
            for (int i = 0; i < Database.nextWeek.Count; i++)
            {
                int helpInt = Database.expired.Count + Database.today.Count + Database.tomorrow.Count+ Database.thisWeek.Count;
                Label boolName2 = new Label()
                {
                    Text = "Наступної неділі",
                    Location = new Point(12, 30 + 60 + 60 * (helpInt)+nextWeek),//35 + 60
                    AutoSize = true,
                };
                Controls.Add(boolName2);

                Button button = new Button()
                {
                    Name = $"{i + helpInt}",
                    Size = new Size(500, 55),
                    Location = new Point(10, (i + helpInt) * 60 + 60 + 50 + nextWeek),
                    BackColor = Color.LightGray,
                    Tag = Name,
                };
                button.Click += updateEvents;

                Label name = new Label()
                {
                    Text = $"{Database.nextWeek[i].name}",
                    AutoSize = true,
                    Location = new Point(40, (i + 1 + helpInt) * 60 + 15 + 40 + nextWeek),
                    BackColor = Color.LightGray,

                };
                Label place = new Label()
                {
                    Text = $"{Database.nextWeek[i].place}",
                    AutoSize = true,
                    Location = new Point(40, 10 + 60 + (i + 1 + helpInt) * 60 + nextWeek),
                    BackColor = Color.LightGray,
                };
                Label time = new Label()
                {
                    Text = $"{Database.nextWeek[i].timeStart.ToUniversalTime().ToString("r")}, Тривалість: {Database.nextWeek[i].howLong.ToString("HH:mm:ss")} ",
                    AutoSize = true,
                    Location = new Point(40, 25 + 60 + (i + 1 + helpInt) * 60 + nextWeek),
                    BackColor = Color.LightGray,
                };
                CheckBox checkBox = new CheckBox()
                {
                    Name = $"{i + helpInt}",
                    Location = new Point(23, (i + 1 + helpInt) * 60 + 10 + 60 + nextWeek),
                    AutoSize = true,
                    BackColor = Color.LightGray,
                    Tag = Name,
                    Checked = Database.nextWeek[i].check

                };
                checkBox.CheckStateChanged += checkBox_CheckStateChanged;

                Controls.Add(name);
                Controls.Add(place);
                Controls.Add(time);
                Controls.Add(checkBox);
                Controls.Add(button);
                Buttons.Add(button);
            }

            
            if(Database.nextWeek.Count == 0) { newHelpINT++; }
            
            if (newHelpINT == 1) { overThanWeek = -20; }
            if (newHelpINT == 2) { overThanWeek = -40; }
            if (newHelpINT == 3) { overThanWeek = -60; }
            if (newHelpINT == 4) { overThanWeek = -80; }
            if (newHelpINT == 5) { overThanWeek = -100; }
            for (int i = 0; i < Database.overThanWeek.Count; i++)
            {
                int helpInt = Database.expired.Count + Database.today.Count + Database.tomorrow.Count + Database.thisWeek.Count+ Database.nextWeek.Count;
                Label boolName2 = new Label()
                {
                    Text = "Пізніше наступної неділі",
                    Location = new Point(12, 30 + 80 + 60 * (helpInt)+overThanWeek),//35 + 60
                    AutoSize = true,
                };
                Controls.Add(boolName2);

                Button button = new Button()
                {
                    Name = $"{i + helpInt}",
                    Size = new Size(500, 55),
                    Location = new Point(10, (i + helpInt) * 60 + 80 + 50 + overThanWeek),
                    BackColor = Color.LightGray,
                    Tag = Name,
                };
                button.Click += updateEvents;

                Label name = new Label()
                {
                    Text = $"{Database.overThanWeek[i].name}",
                    AutoSize = true,
                    Location = new Point(40, (i + 1 + helpInt) * 60 + 15 + 60 + overThanWeek),
                    BackColor = Color.LightGray,

                };
                Label place = new Label()
                {
                    Text = $"{Database.overThanWeek[i].place}",
                    AutoSize = true,
                    Location = new Point(40, 10 + 80 + (i + 1 + helpInt) * 60 + overThanWeek),
                    BackColor = Color.LightGray,
                };
                Label time = new Label()
                {
                    Text = $"{Database.overThanWeek[i].timeStart.ToUniversalTime().ToString("r")}, Тривалість: {Database.overThanWeek[i].howLong.ToString("HH:mm:ss")} ",
                    AutoSize = true,
                    Location = new Point(40, 25 + 80 + (i + 1 + helpInt) * 60 + overThanWeek),
                    BackColor = Color.LightGray,
                };
                CheckBox checkBox = new CheckBox()
                {
                    Name = $"{i + helpInt}",
                    Location = new Point(23, (i + 1 + helpInt) * 60 + 10 + 80 + overThanWeek),
                    AutoSize = true,
                    BackColor = Color.LightGray,
                    Tag = Name,
                    Checked = Database.overThanWeek[i].check

                };
                checkBox.CheckStateChanged += checkBox_CheckStateChanged;

                Controls.Add(name);
                Controls.Add(place);
                Controls.Add(time);
                Controls.Add(checkBox);
                Controls.Add(button);
                Buttons.Add(button);
            }

           
            if(Database.overThanWeek.Count == 0) { newHelpINT++; }
           
            if (newHelpINT == 1) { finished = -20; }
            if (newHelpINT == 2) { finished = -40; }
            if (newHelpINT == 3) { finished = -60; }
            if (newHelpINT == 4) { finished = -80; }
            if (newHelpINT == 5) { finished = -100; }
            if (newHelpINT == 6) { finished = -120; }
            for (int i = 0; i < Database.finished.Count; i++)
            {
                int helpInt = Database.expired.Count + Database.today.Count + Database.tomorrow.Count + Database.thisWeek.Count + Database.nextWeek.Count + Database.overThanWeek.Count;
                Label boolName2 = new Label()
                {
                    Text = "Закінчені",
                    Location = new Point(12, 30 + 100 + 60 * (helpInt)+finished),//35 + 60
                    AutoSize = true,
                };
                Controls.Add(boolName2);

                Button button = new Button()
                {
                    Name = $"{i + helpInt}",
                    Size = new Size(500, 55),
                    Location = new Point(10, (i + helpInt) * 60 + 100 + 50 + finished),
                    BackColor = Color.LightGray,
                    Tag = Name,
                };
                button.Click += updateEvents;

                Label name = new Label()
                {
                    Text = $"{Database.finished[i].name}",
                    AutoSize = true,
                    Location = new Point(40, (i + 1 + helpInt) * 60 + 15 + 80 + finished),
                    BackColor = Color.LightGray,

                };
                Label place = new Label()
                {
                    Text = $"{Database.finished[i].place}",
                    AutoSize = true,
                    Location = new Point(40, 10 + 100 + (i + 1 + helpInt) * 60 + finished),
                    BackColor = Color.LightGray,
                };
                Label time = new Label()
                {
                    Text = $"{Database.finished[i].timeStart.ToUniversalTime().ToString("r")}, Тривалість: {Database.finished[i].howLong.ToString("HH:mm:ss")} ",
                    AutoSize = true,
                    Location = new Point(40, 25 + 100 + (i + 1 + helpInt) * 60 + finished),
                    BackColor = Color.LightGray,
                };
                CheckBox checkBox = new CheckBox()
                {
                    Name = $"{i+helpInt}",
                    Location = new Point(23, (i + 1 + helpInt) * 60 + 10 + 100 + finished),
                    AutoSize = true,
                    BackColor = Color.LightGray,
                    Tag = Name,
                    Checked = Database.finished[i].check

                };
                checkBox.CheckStateChanged += checkBox_CheckStateChanged;

                Controls.Add(name);
                Controls.Add(place);
                Controls.Add(time);
                Controls.Add(checkBox);
                Controls.Add(button);
                Buttons.Add(button);
            }                        
        }
        private void updateEvents(object sender, EventArgs e)
        {               
            Form1 f1 = new Form1();
            f1.FormClosing += F1_FormClosing;
            f1.button1.Click -= f1.button1_Click;
            f1.button1.Click += f1.button1_click_for_FormMain;

            f1.buttonDelete.Visible = true;
                        
            Button button = sender as Button;
            int index = -1;
            
            for(int i = 0; i < Buttons.Count; i++)
            {
                if (button.Equals(Buttons[i]))
                {
                    index = i;
                    f1.buttonDelete.Name =$"{index}";
                    f1.button1.Name = $"{index}";

                    f1.textBox1.Text = Database.events[index].name;
                    f1.textBox2.Text = Database.events[index].place;
                    f1.dateTimePickerChooseDate.Value = Database.events[index].timeStart;
                    f1.dateTimePickerHowLong.Value = Database.events[index].howLong;
                    f1.checkBox.Checked = Database.events[index].check;
                    f1.ShowDialog();
                    
                    break;
                }
                
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Database.LoadToFileData();            
        }

        private void checkBox_CheckStateChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int index = Int32.Parse(checkBox.Name);
            Database.events[index].check = checkBox.Checked;            
            createEvents();  
        }
    } 
}
