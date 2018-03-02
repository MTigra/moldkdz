
namespace ClassLibrary1
{
    public class Адрес
    {
        public Адрес(string _region, string _city, string _address, string _installplace)
        {
            region = _region;
            city = _city;
            adress = _address;
            installplace = _installplace;
        }

        public Адрес()
        {

        }
        public string region { get; set; }
        public string city { get; set; }
        public string adress { get; set; }
        public string installplace { get; set; }
        
    }
}
