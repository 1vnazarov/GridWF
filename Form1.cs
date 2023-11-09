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

namespace GridWF {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        private string readFile(string filePath) {
            try {
                // Проверяем существование файла
                if (File.Exists(filePath)) {
                    // Считываем содержимое файла
                    return File.ReadAllText(filePath);
                }
                else {
                    MessageBox.Show("Файл не найден.");
                    return null;
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Произошла ошибка при чтении файла: " + ex.Message);
                return null;
            }
        }

        private string fileWrite(string data, string filename) {
            string fullData = "";
            string filePath = AppDomain.CurrentDomain.BaseDirectory + filename;
            if (data != null) {
                fullData = data + "\n";
                if (File.Exists(filePath)) fullData = File.ReadAllText(filePath) + fullData;
            }
            try {
                File.WriteAllText(filePath, fullData);
                return filePath;
            }
            catch (Exception ex) {
                return ex.Message;
            }
        }
        private string[] names = { "Карпенко Г.С.", "Дудкин В.С.", "Силахина Т.В.", "Ильюшенков Л.В.", "Левит Л.В." };
        private string[] phones = { "+71234567890", "+745678901", "+70987654321", "+79964295057", "+78005553535" };
        private void Form1_Load(object sender, EventArgs e) {
            var tbl = new DataTable();
            tbl.Columns.Add("Фамилии");
            tbl.Columns.Add("Номера телефонов");
            for (int i = 0; i < names.Length; i++)
                tbl.Rows.Add(names[i], phones[i]);
            dataGridView1.DataSource = tbl;
            button1.Select(); // Снять выделение с текстбокса
            const string nl = "\r\n";
            textBox1.Text = "Таблица телефонов:" + nl + nl;
            for (int i = 0; i < names.Length; i++)
                textBox1.Text += string.Format("{0, -30} {1, -30}", names[i], phones[i]) + nl;
            fileWrite(null, "TableNames.txt");
            fileWrite(textBox1.Text, "TableNames.txt");
        }

        private void button1_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("Notepad", "TableNames.txt");
        }

        private void button2_Click(object sender, EventArgs e) {
            string html =
            @"<title>Таблица</title>
            <table border='1' align='center'>
                <caption>Таблица телефонов</caption>";
            for (int i = 0; i < names.Length; i++)
                html += string.Format("<tr><td>{0}</td><td>{1}</td></tr>", names[i], phones[i]);
            html += "</table>";
            fileWrite(null, "TableNames.html");
            fileWrite(html, "TableNames.html");
            System.Diagnostics.Process.Start("chrome.exe", fileWrite("", "TableNames.html"));
        }
    }
}
