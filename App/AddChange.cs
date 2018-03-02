using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ClassLibrary1;

namespace App
{
    /// <summary>
    /// Представляет окно для редактирования или добавления новых элементов таблицы
    /// </summary>
    public partial class AddChange : Form
    {
        /// <summary>
        /// Ссылка на коллекцию состоящую из объектов класса <see cref="Банкомат"/>
        /// </summary>
        List<Банкомат> Blist; 

        /// <summary>
        /// Индекс объекта с которым ведется работа
        /// </summary>
        int currindex;

        /// <summary>
        /// Флаг, отвечающий за выполняемый процесс.
        /// 0 - для изменения объекта, 1 - для добавления нового 
        /// </summary>
        int option;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddChange"/> предназначенный для изменения данных объектов класса <see cref="Банкомат"/> в коллекции 
        /// </summary>
        /// 
        /// <param name="Blist">Ссылка на коллекцию состоящую из объектов класса <see cref="Банкомат"/></param>
        /// <param name="currindex">Индекс объекта с которым ведется работа</param>
        public AddChange(ref List<Банкомат> Blist, int currindex)
        {
            this.option = 0;
            this.currindex = currindex;
            this.Blist = Blist;
            InitializeComponent();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddChange"/> предназначенный для создания нового объекта класса <see cref="Банкомат"/> в коллекции
        /// </summary>
        /// <param name="Blist">Ссылка на коллекцию состоящую из объектов класса <see cref="Банкомат"/></see></param>
        public AddChange(ref List<Банкомат> Blist)
        {
            this.option = 1;

            this.Blist = Blist;
            InitializeComponent();
        }
        /// <summary>
        /// Обработка события загрузки формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddChange_Load(object sender, EventArgs e)
        {


            switch (option)
            {
                case 0: // При выбранной опции для изменения
                    {
                        button1.Text = "Изменить";
                        //заполнение текстбоксов исходными значениями
                        textBox1.Text = Blist[currindex].adr.region;
                        textBox2.Text = Blist[currindex].adr.city;
                        textBox3.Text = Blist[currindex].adr.adress;
                        textBox4.Text = Blist[currindex].adr.installplace;
                        textBox5.Text = Blist[currindex].int_cards_support;
                        textBox6.Text = Blist[currindex].sbercart;
                        textBox7.Text = Blist[currindex].american_express;
                        textBox8.Text = Blist[currindex].for_organizations;
                        textBox9.Text = Blist[currindex].accepts_money;
                        textBox10.Text = Blist[currindex].prints_onepass;
                        textBox11.Text = Blist[currindex].access;
                        textBox12.Text = Blist[currindex].comments;
                        numericTextBox1.Text = Blist[currindex].bank_code;
                        textBox13.Text = Blist[currindex].bank_name;
                        numericTextBox2.Text = Blist[currindex].org_id;
                        textBox14.Text = Blist[currindex].org_name;
                        textBox15.Text = Blist[currindex].phone;
                    }
                    break;
                case 1:// При выбранной опции "Добавить"
                    {
                        button1.Text = "Добавить";

                    }
                    break;

            }
        }

        /// <summary>
        /// Обработка события клика на кнопку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            switch (option)
            {
                case 0://изменить
                    {
                        Blist[currindex].adr.region = textBox1.Text;
                        Blist[currindex].adr.city = textBox2.Text;
                        Blist[currindex].adr.adress = textBox3.Text;
                        Blist[currindex].adr.installplace = textBox4.Text;
                        Blist[currindex].int_cards_support = textBox5.Text;
                        Blist[currindex].sbercart = textBox6.Text;
                        Blist[currindex].american_express = textBox7.Text;
                        Blist[currindex].for_organizations = textBox8.Text;
                        Blist[currindex].accepts_money = textBox9.Text;
                        Blist[currindex].prints_onepass = textBox10.Text;
                        Blist[currindex].access = textBox11.Text;
                        Blist[currindex].comments = textBox12.Text;
                        Blist[currindex].bank_code = numericTextBox1.Text;
                        Blist[currindex].bank_name = textBox13.Text;
                        Blist[currindex].org_id = numericTextBox2.Text;
                        Blist[currindex].org_name = textBox14.Text;
                        Blist[currindex].phone = textBox15.Text;
                        DialogResult = DialogResult.OK;
                    }
                    break;
                case 1://Добавить
                    {
                        Банкомат atm = new Банкомат();
                                             
                        
                        atm.adr.region = textBox1.Text;
                        atm.adr.city = textBox2.Text;
                        atm.adr.adress = textBox3.Text;
                        atm.adr.installplace = textBox4.Text;
                        atm.int_cards_support = textBox5.Text;
                        atm.sbercart = textBox6.Text;
                        atm.american_express = textBox7.Text;
                        atm.for_organizations = textBox8.Text;
                        atm.accepts_money = textBox9.Text;
                        atm.prints_onepass = textBox10.Text;
                        atm.access = textBox11.Text;
                        atm.comments = textBox12.Text;
                        if (string.IsNullOrWhiteSpace(numericTextBox1.Text)||numericTextBox1.IntValue<0)// Проверка на корректность ввода в "bank_code"
                        {
                         
                            MessageBox.Show("Значение в поле \"bank_code\" должно быть положительным целым числом.", "Ошибка ввода!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            numericTextBox1.Focus();
                            break;
                        }
                        else
                        {
                            atm.bank_code = numericTextBox1.Text;
                           
                           
                        }

                        atm.bank_name = textBox13.Text;

                        if (string.IsNullOrWhiteSpace(numericTextBox2.Text) || numericTextBox2.IntValue < 0)// Проверка на корректность ввода в поле org_id
                        {
                          
                            MessageBox.Show("Значение в поле \"org_id\" должно быть положительным целым числом.", "Ошибка ввода!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            numericTextBox2.Focus();
                            break;
                        }
                        else
                        {
                           atm.org_id = numericTextBox2.Text;
                            

                        }
                       atm.org_name = textBox14.Text;
                       atm.phone = textBox15.Text;
                       
                            Blist.Add(atm);
                            DialogResult = DialogResult.OK;
                            
                        

                    }
                    break;
            }
        }
    }
}