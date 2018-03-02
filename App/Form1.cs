using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ClassLibrary1;

namespace App
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Индекс на текущей строки с которой ведется работа
        /// </summary>
        int currindex { get; set; } //Автореализуемое свойство дающее доступ к индексу строки с котоым ведется работа
        DataTable dt = new DataTable();
        /// <summary>
        /// Имя ссылки на коллекцию объектов класса <see cref="Банкомат"/>
        /// </summary>
        List<Банкомат> atms;

        /// <summary>
        /// Возвращает коллекцию объектов класса <see cref="Банкомат"/>
        /// Свойство только для чтения.
        /// </summary>
        public List<Банкомат> Atms
        {
            get
            {
                return atms;
            }
        }

        /// <summary>
        /// Имя ссылку на коллекцию класса <see cref="Банкомат"/>
        /// Предназначен для сохранения данных перед фильтрацией, чтобы позволить пользователю сбросить изменения
        /// </summary>
        List<Банкомат> backupAtms;
        /// <summary>
        /// Флаг, показывающий являются ли данные уже отфильтрованными.
        /// </summary>
        bool isFiltered = false;
        /// <summary>
        /// Поле в котором хранится путь к файлу.
        /// </summary>
        static string path = "";
        /// <summary>
        /// Направление сортировки
        /// </summary>
        ListSortDirection direction = ListSortDirection.Ascending;

        public Form1()
        {

            InitializeComponent();
        }

        /// <summary>
        /// Событие в котром обрабатывается открытие файла
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked) numericTextBox1.Enabled = false;
            else numericTextBox1.Enabled = true;
            button1.Enabled = false;
            viewToolStripMenuItem.Enabled = false;
            button2.Enabled = false;
            сохранитьToolStripButton.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            SaveMenuToolStripItem.Enabled = false;
            сохранитькакToolStripButton.Enabled = false;
            создатьToolStripButton.Enabled = false;

            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    isFiltered = false;
                    atms = new List<Банкомат>();
                    dt.Rows.Clear();
                    dt.Columns.Clear();
                    dataGridView1.DataSource = dt;
                    path = openFileDialog1.FileName;
                    viewToolStripMenuItem.Enabled = true;
                    List<string> rows = File.ReadAllLines(path, Encoding.UTF8).ToList();

                    if (rows[0].Split(',').Length != 17) throw new Exception();
                    // Заполнение заголовков колонок значениями.
                    foreach (var item in rows[0].Split(','))
                    {

                        dt.Columns.Add(item);
                    }

                    // Отрисовка таблицы в случае, если пользователь решил вывести все элементы.
                    if (checkBox1.Checked)
                    {
                        for (int i = 1; i <= rows.Count - 1; i++)
                        {
                            atms.Add(new Банкомат(rows[i]));
                        }


                        dataGridView1.SuspendLayout();
                        Cursor = Cursors.AppStarting;
                        MyMethods.ShowTable(atms.Count, atms, ref dt);
                        Cursor = Cursors.Default;
                        //dataGridView1.DataSource = dt;
                        dataGridView1.ResumeLayout();
                    }
                    // В случе если пользовтель сам выбрал число в выводимых строк, осуществляется проверка введенных значений.
                    else if (numericTextBox1.IntValue > 0 && !string.IsNullOrWhiteSpace(numericTextBox1.Text))
                    {
                        if (numericTextBox1.IntValue > rows.Count - 1)// Если пользователь ввел значение превышающее допустимое.
                        {

                            if (MessageBox.Show("Запрашивоемое число строк превышает кол-во существующих." +
                                string.Format("\nМаксимальное число для вывода:{0}", rows.Count - 1) +
                              "\nБудут выведены все строки файла.",
                              "Ошибка ввода.", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {


                                for (int i = 1; i <= rows.Count - 1; i++)
                                {
                                    atms.Add(new Банкомат(rows[i]));
                                }
                                numericTextBox1.Text = (atms.Count).ToString();
                                dataGridView1.SuspendLayout();
                                Cursor = Cursors.AppStarting;
                                MyMethods.ShowTable(numericTextBox1.IntValue, atms, ref dt);
                                Cursor = Cursors.Default;

                                dataGridView1.ResumeLayout();
                            }
                            else { return; }
                        }
                        else // Если пользователь ввел все верно.
                        {

                            for (int i = 1; i <= numericTextBox1.IntValue; i++)
                            {
                                atms.Add(new Банкомат(rows[i]));
                            }
                            dataGridView1.SuspendLayout();
                            Cursor = Cursors.AppStarting;
                            MyMethods.ShowTable(numericTextBox1.IntValue, atms, ref dt);
                            Cursor = Cursors.Default;
                            dataGridView1.ResumeLayout();
                            dataGridView1.ClearSelection();

                        }

                    }
                    else {
                        MessageBox.Show("Введено некорректное кол-во строк для вывода", "Ошибка ввода.",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    button2.Enabled = true;
                    сохранитьToolStripButton.Enabled = true;
                    saveAsToolStripMenuItem.Enabled = true;
                    SaveMenuToolStripItem.Enabled = true;
                    сохранитькакToolStripButton.Enabled = true;
                    создатьToolStripButton.Enabled = true;
                    this.Text = "BANKOMATES -" + path;
                }


            }
            catch (ArgumentNullException ex)
            {
                numericTextBox1.Focus();
                System.Media.SystemSounds.Beep.Play();
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Веротяно, выбран недопустимый файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (checkBox1.Checked) numericTextBox1.Enabled = false;
            else numericTextBox1.Enabled = true;
            button1.Enabled = false;
            viewToolStripMenuItem.Enabled = false;
            button2.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) numericTextBox1.Enabled = false;
            else numericTextBox1.Enabled = true;
        }

        /// <summary>
        /// Обработка события в котрой отрисовывается порядковый номер строки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {

                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Сделаем метод сортировки нужных нам колонок программным.
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name == "bank_code" || column.Name == "accepts_money")
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                else column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }
        /// <summary>
        /// По клику на заголовок столбца выполняется сортировка.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_ColumnHeaderMouseClick( 
                                     object sender, DataGridViewCellMouseEventArgs e)
        {

            DataGridViewColumn newColumn = dataGridView1.Columns[e.ColumnIndex];

           
                if (newColumn.Name == "bank_code")
                {
                    dataGridView1.Columns["accepts_money"].HeaderCell.SortGlyphDirection = SortOrder.None;

                    if (direction == ListSortDirection.Ascending)
                    {
                        dataGridView1.SuspendLayout();
                    try {
                        atms.Sort((x, y) => int.Parse(x.bank_code).CompareTo(int.Parse(y.bank_code)));
                        Cursor = Cursors.AppStarting;
                        MyMethods.ShowTable(atms.Count, atms, ref dt);
                    }
                    catch (Exception) { MessageBox.Show("Файл содержит недопустимые для сортировки значения в поле \"bank_code\"","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Asterisk); }
                        Cursor = Cursors.Default;
                        // dataGridView1.DataSource = dt;
                        dataGridView1.ResumeLayout();
                        direction = ListSortDirection.Descending;

                        newColumn.HeaderCell.SortGlyphDirection =
                    direction == ListSortDirection.Ascending ?
                     SortOrder.Descending : SortOrder.Ascending;


                    }
                    else
                    {
                        dataGridView1.SuspendLayout();
                        atms.Reverse();
                        Cursor = Cursors.AppStarting;
                        MyMethods.ShowTable(atms.Count, atms, ref dt);
                        Cursor = Cursors.Default;

                        dataGridView1.ResumeLayout();
                        direction = ListSortDirection.Ascending;

                        newColumn.HeaderCell.SortGlyphDirection =
                    direction == ListSortDirection.Ascending ?
                    SortOrder.Descending : SortOrder.Ascending;

                    }
               


            }
            else if (newColumn.Name == "accepts_money")
            {
                dataGridView1.Columns["bank_code"].HeaderCell.SortGlyphDirection = SortOrder.None;

                if (direction == ListSortDirection.Ascending)
                {
                    dataGridView1.SuspendLayout();
                    atms.Sort((x, y) => x.accepts_money.CompareTo(y.accepts_money));
                    Cursor = Cursors.AppStarting;
                    MyMethods.ShowTable(atms.Count, atms, ref dt);
                    Cursor = Cursors.Default;
                    //  dataGridView1.DataSource = dt;
                    dataGridView1.ResumeLayout();
                    direction = ListSortDirection.Descending;


                    newColumn.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ?
                 SortOrder.Descending : SortOrder.Ascending;


                }
                else
                {
                    dataGridView1.SuspendLayout();
                    atms.Reverse();
                    Cursor = Cursors.AppStarting;
                    MyMethods.ShowTable(atms.Count, atms, ref dt);
                    Cursor = Cursors.Default;
                    //   dataGridView1.DataSource = dt;
                    dataGridView1.ResumeLayout();
                    direction = ListSortDirection.Ascending;

                    newColumn.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ?
                SortOrder.Descending : SortOrder.Ascending;

                }
            }
        }

       
        /// <summary>
        /// Событие, при обработке которого выполняется фильтрация по полю "access"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            if (!isFiltered)
            {
                backupAtms = new List<Банкомат>();
                backupAtms.AddRange(atms);
                isFiltered = true;
            }

            chooseFParameter choosfilterparameter = new chooseFParameter("Отфильтровать по access", this);
            choosfilterparameter.ShowDialog();
            if (choosfilterparameter.DialogResult == DialogResult.OK)
            {
                atms.RemoveAll((x) => { if (x.access != choosfilterparameter.FPatameter) return true; else return false; });
                choosfilterparameter.Close();
                choosfilterparameter.Dispose();
                Cursor = Cursors.AppStarting;
                MyMethods.ShowTable(atms.Count, atms, ref dt);
                Cursor = Cursors.Default;
                button1.Enabled = true;
            }
        }
        /// <summary>
        /// Событие, при обработке которого выполняется фильтрация по полю "american_express"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void amEMenuItem4_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            if (!isFiltered)
            {
                backupAtms = new List<Банкомат>();
                backupAtms.AddRange(atms);
                isFiltered = true;
            }
            chooseFParameter choosfilterparameter = new chooseFParameter("Отфильтровать по american_express", this);
            choosfilterparameter.ShowDialog();
            if (choosfilterparameter.DialogResult == DialogResult.OK)
            {
                atms.RemoveAll((x) => { if (x.american_express != choosfilterparameter.FPatameter) return true; else return false; });
                choosfilterparameter.Close();
                choosfilterparameter.Dispose();
                Cursor = Cursors.AppStarting;
                MyMethods.ShowTable(atms.Count, atms, ref dt);
                Cursor = Cursors.Default;
                button1.Enabled = true;
            }
        }
        /// <summary>
        /// Свойство формирующее коллекцию из уникальных значений поля "access"
        /// </summary>
        /// <returns>Возвращает коллекцию типа <see cref="List{String}"/></returns>
        public List<string> GetAccessList()
        {
            List<string> acc = new List<string>();
            for (int i = 0; i < atms.Count; i++)
            {
                if (!acc.Contains(atms[i].access))
                {
                    acc.Add(atms[i].access);
                }

            }
            return acc;
        }
        /// <summary>
        /// Свойство формирующее коллекцию из уникальных значений поля "american express"
        /// </summary>
        /// <returns>Возвращает коллекцию типа <see cref="List{String}" /></returns>
        public List<string> GetAEList()
        {
            List<string> ae = new List<string>();
            for (int i = 0; i < atms.Count; i++)
            {
                if (!ae.Contains(atms[i].american_express))
                {

                    ae.Add(atms[i].american_express);

                }
            }
            return ae;
        }
        /// <summary>
        /// Обработка события клика мышкой на кнопку для сброса изменений после фильтрации.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            atms.Clear();
            atms.AddRange(backupAtms);
            this.SuspendLayout();
            Cursor = Cursors.AppStarting;
            MyMethods.ShowTable(atms.Count, atms, ref dt);
            Cursor = Cursors.Default;
            this.ResumeLayout();
            button1.Enabled = false;
            button2.Enabled = true;
        }

        /// <summary>
        /// Обработка события при клике на заголовок строки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // При клике правой кнопкой открывается выпадающее меню, позволяющее пользователю сделать дальнейший выбор.
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position);
                currindex = e.RowIndex;


            }
        }
        /// <summary>
        /// Событие возникающее, если пользователь выбрал удаление строки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int firstindex = dataGridView1.FirstDisplayedScrollingRowIndex;
            atms.RemoveAt(currindex);
            Cursor = Cursors.AppStarting;
            MyMethods.ShowTable(atms.Count, atms, ref dt);
            if (dataGridView1.RowCount > 0) dataGridView1.FirstDisplayedScrollingRowIndex = firstindex;
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Событие возникающее, если пользователь выбрал изменение строки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int firstindex = dataGridView1.FirstDisplayedScrollingRowIndex;
            AddChange Change = new AddChange(ref atms, currindex);
            Change.ShowDialog();
            
            Cursor = Cursors.AppStarting;
            if (Change.DialogResult == DialogResult.OK)
            {
                Cursor = Cursors.AppStarting;
                dataGridView1.SuspendLayout();
                MyMethods.ShowTable(atms.Count, atms, ref dt);
                dataGridView1.ResumeLayout();
                dataGridView1.FirstDisplayedScrollingRowIndex = firstindex;
                Cursor = Cursors.Default;
                
            }
            dataGridView1.FirstDisplayedScrollingRowIndex = firstindex;
            
            
        }
        /// <summary>
        /// Событие, возникающее когда пользователь нажал на кнопку, осуществляющую добавление новой записи о Банкомате
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            AddChange Addnote = new AddChange(ref atms);
            Addnote.ShowDialog();
            if (Addnote.DialogResult == DialogResult.OK)
            {
                Cursor = Cursors.AppStarting;
                dataGridView1.SuspendLayout();
                MyMethods.ShowTable(atms.Count, atms, ref dt);
                dataGridView1.ResumeLayout();
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                Cursor = Cursors.Default;

            }


        }
        /// <summary>
        /// Обработка события при желении сохранить файл путем создания нового файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сохранитькакToolStripButton_Click(object sender, EventArgs e)
        {

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {


                // Перевести в кодировку csv
                List<string> encoded = MyMethods.Serialization(atms).ToList();
                string head = "region,city,address,installplace,int_cards_support,sbercart," +
                    "american_express,for_organizations,accepts_money,prints_onepass,access," +
                    "comments,bank_code,bank_name,org_id,org_name,phone";
                encoded.Insert(0, head);
                
                File.WriteAllLines(saveFileDialog1.FileName, encoded.ToArray(), Encoding.UTF8);

                path = saveFileDialog1.FileName;
                this.Text = "BANKOMATES" + path;
                сохранитьToolStripButton.Enabled = true;
            }

        }

        /// <summary>
        /// Обработка события при желении сохранить файл путем изменения текущего файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSave_btn_Click(object sender, EventArgs e)
        {
            
            // Перевести в кодировку csv
            List<string> encoded = MyMethods.Serialization(atms).ToList();
            string head = "region,city,address,installplace,int_cards_support,sbercart," +
                     "american_express,for_organizations,accepts_money,prints_onepass,access," +
                     "comments,bank_code,bank_name,org_id,org_name,phone";
            encoded.Insert(0, head);
            File.WriteAllLines(path, encoded.ToArray(), Encoding.UTF8);
        }

        private void справкаToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Требования:\n\n1) Поля файла должны быть отделены сиволом \",\"(запятая)\n" +
                "2) Файл должен задаваться ровно 17-ю полями точно в таком порядке:  region, city, address, installplace, int_cards_support, sbercart, " +
                     "american_express,for_organizations,accepts_money,prints_onepass,access," +
                     "comments,bank_code,bank_name,org_id,org_name,phone\n" +
                "4) Если поле содержит разделяющий символ(,) или кавычки(\"), то оно само заключается в кавычки(\"), а кавычки в поле заменяются на двойные(\"\")\n" +
                "5)Сортировку по полю \"bank_code\" невозможно осуществить, если в этих полях представлены нечисловые значения. "+
                "\n\nИнформация о функционале приложения:\n1)сортировска элементов осуществляется путем нажатия на заголовок столбца.\n2)Изменение и удаление осуществляется путем нажатия правой кнопкой мыши на соответсвующий загголовок строки."+
                "\n3)Пользователь может сбросить изменения после сортировки путем нажатия на соответствующую кнопку.\n\nПриятного пользования :)",
                    "Помощь", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Приложение предназначено для просмотра,\n обработки и сохранения обработанных\n данных из файла формата .csv\nАвтор: Мартиросян Тигран Оганнесович\n НИУ ВШЭ 2016\nПреподаватель: Подбельский В.В.",
                    "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
