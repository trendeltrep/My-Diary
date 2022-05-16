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


namespace DiaryProjext
{

    public partial class Form1 : Form
    {       
        public Form1()
        {
            InitializeComponent();
            BackColor = Color.Yellow;
            CheckFile();          
            SetMyCustomFormat();            
        }

        private void SetMyCustomFormat()
        {
            dateTimePickerChooseDate.Format = DateTimePickerFormat.Custom;
            dateTimePickerChooseDate.CustomFormat = "dd.MM.yyyy HH:mm:ss";

            dateTimePickerHowLong.Format = DateTimePickerFormat.Custom;
            dateTimePickerHowLong.CustomFormat = "HH:mm:ss";
        }

        protected void CheckFile()
        {
            if (!File.Exists(@"DIARYbase.txt"))
            {
                StreamWriter sw = new StreamWriter(@"DIARYbase.txt");
                sw.Close();
            }
        }

        private bool CheckNameAndWhat()
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private string CheckForDatesIntersection(DateTime date)
        {
            foreach(Event e in Database.events)
            {
                string helpDate = e.howLong.ToString("HH:mm:ss");
                
                var time = TimeSpan.Parse(helpDate);
                var result = e.timeStart.Add(time);

                if (date>= e.timeStart && date <=result) { return e.name; }
            }
            return string.Empty;
        }

        private string CheckForDatesIntersectionUpdates(DateTime date,int helpint)
        {
            for (int i = 0; i < Database.events.Count; i++)
            {
                if (i != helpint)
                {
                    string helpDate = Database.events[i].howLong.ToString("HH:mm:ss");
                    var time = TimeSpan.Parse(helpDate);
                    var result = Database.events[i].timeStart.Add(time);

                    if (date >= Database.events[i].timeStart && date <= result)
                    {
                        return Database.events[i].name;
                    }
                }
            }
            return string.Empty;
        }

        public void button1_click_for_FormMain(object sender,EventArgs e)
        {
            if (CheckNameAndWhat() == true)
            {
                
                int index = Int32.Parse(button1.Name);
                var helpstring = CheckForDatesIntersectionUpdates(dateTimePickerChooseDate.Value,index);
                if (helpstring == string.Empty)
                {
                    Database.events.RemoveAt(index);
                    Event newEvent = new Event(textBox1.Text, textBox2.Text, dateTimePickerChooseDate.Value, dateTimePickerHowLong.Value, checkBox.Checked);
                    Database.events.Add(newEvent);
                    Close();
                }
                else
                {
                    MessageBox.Show(caption: "Помилка", text: $"Дата проведення конфліктуєт з {helpstring}.\nПотрібно змінити дату проведення");
                }
            }
            else
            {
                MessageBox.Show(caption: "Помилка", text: "Заповніть назву події та місце її проведення");
            }
        }

        public  void button1_Click(object sender, EventArgs e)
        {
            if (CheckNameAndWhat() == true)
            {
                var helpstring = CheckForDatesIntersection(dateTimePickerChooseDate.Value);
                if (helpstring == string.Empty)
                {
                    Event newEvent = new Event(textBox1.Text, textBox2.Text, dateTimePickerChooseDate.Value, dateTimePickerHowLong.Value, checkBox.Checked);
                    Database.events.Add(newEvent);
                    Close();
                }
                else
                {
                    MessageBox.Show(caption:"Помилка", text:$"Дата проведення конфліктуєт з {helpstring}.\nПотрібно змінити дату проведення");
                }
            }
            else
            {
                MessageBox.Show(caption: "Помилка", text: "Заповніть назву події та місце її проведення");
            }
        }
        public void buttonDelete_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int index = Int32.Parse(button.Name);
            DialogResult dialogResult = MessageBox.Show("Ви впевненні що бажаєте видалити цю подію?", "Видалення", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Database.events.RemoveAt(index);
                Close();
            }
        }
    }
}
