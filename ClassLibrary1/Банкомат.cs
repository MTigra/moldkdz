using System.Collections.Generic;
namespace ClassLibrary1{
    public class Банкомат{
        public Адрес adr;
        public Банкомат(string _region, string _city, string _address, string _installplace,
            string _int_cards_support, string _sbercart, string _american_express, 
            string _for_organizations, string _accepts_money, string _prints_onepass, 
            string _access, string _comments, string _bank_code, string _bank_name,
            string _org_id, string _org_name, string _phone)  {
            adr = new Адрес(_region, _city,  _address, _installplace);
            int_cards_support = _int_cards_support; sbercart = _sbercart; american_express = _american_express; for_organizations = _for_organizations;
            accepts_money = _accepts_money; prints_onepass = _prints_onepass; access= _access; comments= _comments; bank_code= _bank_code; bank_name= _bank_name;
            org_id = _org_id; org_name= _org_name; phone = _phone;   }
        public Банкомат(string csvRow)
        {
            List<string> atmqualities = ClassLibrary1.MyMethods.Parserow(csvRow);
                adr = new Адрес(atmqualities[0], atmqualities[1], atmqualities[2], atmqualities[3]);
                int_cards_support = atmqualities[4]; sbercart = atmqualities[5]; american_express = atmqualities[6];
                for_organizations = atmqualities[7];
                accepts_money = atmqualities[8]; prints_onepass = atmqualities[9]; access = atmqualities[10];
                comments = atmqualities[11]; bank_code = atmqualities[12]; bank_name = atmqualities[13];
                org_id = atmqualities[14]; org_name = atmqualities[15]; phone = atmqualities[16];
        }
        public Банкомат() { adr = new Адрес(); }
        public string int_cards_support { get; set; }        public string sbercart { get; set; }        public string american_express { get; set; }
        public string for_organizations { get; set; }        public string accepts_money { get; set; }
        public string prints_onepass { get; set; }        public string access { get; set; }
        public string comments { get; set; }        public string bank_code { get; set; }
        public string bank_name { get; set; }        public string org_id { get; set; }
        public string org_name { get; set; }        public string phone { get; set; }
    }}
