using Newtonsoft.Json;
using System.Text;

namespace KCAHostelBookingSystemAPI.Models
{
    public class MpesaToMpesa
    {
        [JsonProperty("BusinessShortCode")]
        public int BusinessShortCode { get; set; }
        public string Password { get; set; }
        public string Timestamp { get; set; }
        public string TransactionType { get; set; }
        public int Amount { get; set; }
        public long PartyA { get; set; }
        public int PartyB { get; set; }
        public long PhoneNumber { get; set; }
        public string CallBackURL { get; set; }
        public string AccountReference { get; set; }
        public string TransactionDesc { get; set; }

        public string GeneratePassword()
        {
            string pass = BusinessShortCode + "bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919" + Timestamp;
            Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(pass));
            return Password;

        }
    }
}
