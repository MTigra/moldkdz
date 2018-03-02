using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;

namespace ClassLibrary1
{
    /// <summary>
    /// Класс наследуемый от <see cref="TextBox"/> осуществляющий проверку на недопустимые символы. 
    /// </summary>
    public class NumericTextBox : TextBox
    {
        // Флаг, отвечающий за недопустимость пробелов.
        bool allowSpace = false;

        /// <summary>
        ///  Метод обрабатывающий, нажатие пользователем клавиши.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            if (Char.IsDigit(e.KeyChar))
            {
                // Десятичные числа.
            }
            else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
             keyInput.Equals(negativeSign))
            {
                //Введенная клавиша является разделителем или знаком "минус"
                e.Handled = true;
                System.Media.SystemSounds.Beep.Play();
            }
            else if (e.KeyChar == '\b')
            {
                // Возврат каретки.
            }

            else if (this.allowSpace && e.KeyChar == ' ')
            {

            }
            else
            {
                // Введенная клавиша является недопустимой.
                e.Handled = true;
                System.Media.SystemSounds.Beep.Play();


            }
        }
        /// <summary>
        /// Возвращает десятичное значение числа введенного в NumericTextBox
        /// </summary>
        public int IntValue
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.Text) || string.IsNullOrEmpty(this.Text))
                {

                    throw new ArgumentNullException("Кол-во строк требуемых для вывода.", "Значение не может быть пустым.");

                }
                else
                    return Int32.Parse(this.Text);
            }
        }

        public bool AllowSpace
        {
            set
            {
                this.allowSpace = value;
            }

            get
            {
                return this.allowSpace;
            }
        }
    }
    public class MyMethods
    {

        /// <summary>
        /// Метод для обрезания боковых кавычек.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string myTrim(string str)
        {
            if (str.StartsWith("\"") && str.EndsWith("\"")) str = str.Substring(1, str.Length - 2);
            else return str;
            return str;
        }

        /// <summary>
        /// Метод для парса строки
        /// </summary>
        /// <param name="csvRow"> строка типа CSV </param>
        /// <returns name="lstFields">  Возвращает лист элементов полученных из строки </returns>
        static public List<string> Parserow(string csvRow)
        {
            List<string> lstFields = new List<string>();
            bool iq = false;
            string temp;
            int st = 0;
            for (int i = 0; i < csvRow.Length; i++)
            {
                if (csvRow[i] == ',' && !iq)
                {
                    temp = myTrim(csvRow.Substring(st, i - st));
                    lstFields.Add(temp.Replace("\"\"", "\""));
                    st = i + 1;
                }

                if (csvRow[i] == '"' && !iq) iq = true;
                else if (csvRow[i] == '"' && iq) iq = false;

            }

            if (!string.IsNullOrEmpty(myTrim(csvRow.Substring(st).Replace("\"\"", "\""))))
            {
                temp = myTrim(csvRow.Substring(st));
                lstFields.Add(temp.Replace("\"\"", "\""));
            }
            return lstFields;
        }

        public static void ShowTable(int N, List<Банкомат> atms, ref System.Data.DataTable table)
        {


            table.Rows.Clear();
            for (int i = 0; i < N; i++)
            {
                table.Rows.Add(atms[i].adr.region, atms[i].adr.city, atms[i].adr.adress, atms[i].adr.installplace,
                    atms[i].int_cards_support, atms[i].sbercart, atms[i].american_express, atms[i].for_organizations,
                        atms[i].accepts_money, atms[i].prints_onepass, atms[i].access, atms[i].comments,
                        atms[i].bank_code, atms[i].bank_name, atms[i].org_id, atms[i].org_name, atms[i].phone);

            }


        }


        //  Метод для перевода строки в кодировку csv
        private static string SaveEncode(string s)
        {
            if (s.Contains(",") || s.Contains("\""))
            {
                s = "\"" + s.Replace("\"", "\"\"") + "\"";
            }
            return s;
        }

        // Метод сериализации для дальнейшего сохранения
        public static string[] Serialization(List<Банкомат> atms)
        {
            string[] serializedArray = new string[atms.Count];
            for (int i = 0; i < serializedArray.Length; i++)
            {
                serializedArray[i] = SaveEncode(atms[i].adr.region) + "," + SaveEncode(atms[i].adr.city) + "," +
                                     SaveEncode(atms[i].adr.adress) + "," + SaveEncode(atms[i].adr.installplace) + "," +
                                     SaveEncode(atms[i].int_cards_support) + "," + SaveEncode(atms[i].sbercart) + "," +
                                     SaveEncode(atms[i].american_express) + "," + SaveEncode(atms[i].for_organizations) + "," +
                                     SaveEncode(atms[i].accepts_money) + "," + SaveEncode(atms[i].prints_onepass) + "," +
                                     SaveEncode(atms[i].access) + "," + SaveEncode(atms[i].comments) + "," +
                                     SaveEncode(atms[i].bank_code) + "," + SaveEncode(atms[i].bank_name) + "," +
                                     SaveEncode(atms[i].org_id) + "," + SaveEncode(atms[i].org_name) + "," +
                                     SaveEncode(atms[i].phone);
            }
            return serializedArray;
        }


    }

}
