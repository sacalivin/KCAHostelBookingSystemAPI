using KCAHostelBookingSystemAPI.Models;
using Newtonsoft.Json;
using RestSharp;

namespace KCAHostelBookingSystemAPI.Services
{
    public class MpesaService
    {
        string token = "";
        public void GenerateToken()
        {
            using (var client = new RestClient(" https://sandbox.safaricom.co.ke/oauth/v1/generate?grant_type=client_credentials"))
            {
                var request = new RestRequest("", Method.Get);
                request.AddHeader("Authorization", "Basic cFJZcjZ6anEwaThMMXp6d1FETUxwWkIzeVBDa2hNc2M6UmYyMkJmWm9nMHFRR2xWOQ==");
                request.AddParameter("text/plain", "", ParameterType.RequestBody);
                var response = client.Execute(request);
                Token tokenObj = JsonConvert.DeserializeObject<Token>(response.Content);
                token = tokenObj.access_token;
                Console.WriteLine(token);




            }

        }

        MpesaToMpesa jsonObject = new()
        {
            BusinessShortCode = 174379,
            Password = "MTc0Mzc5YmZiMjc5ZjlhYTliZGJjZjE1OGU5N2RkNzFhNDY3Y2QyZTBjODkzMDU5YjEwZjc4ZTZiNzJhZGExZWQyYzkxOTIwMjIxMjAxMTMyNTE1    ",
            Timestamp = "20221201110919",
            TransactionType = "CustomerPayBillOnline",
            Amount = 2000,
            PartyA = 254713624672,
            PartyB = 174379,
            PhoneNumber = 254713624672,
            CallBackURL = "https://mydomain.com/path",
            AccountReference = "KCAUHostel",
            TransactionDesc = "BookingDeposit"
        };

        public void SendPaymentRequest(long payerPhone)
        {
            jsonObject.PhoneNumber = payerPhone;
            GenerateToken(); // generates the aut token

            using (var client2 = new RestClient("https://sandbox.safaricom.co.ke/mpesa/stkpush/v1/processrequest"))
            {
                jsonObject.GeneratePassword();
                string jsonText = JsonConvert.SerializeObject(jsonObject);
                var request2 = new RestRequest("", Method.Post);
                request2.AddHeader("Content-Type", "application/json");
                request2.AddHeader("Authorization", $"Bearer {token}");
                request2.AddParameter("application/json", jsonText, ParameterType.RequestBody);

                var response2 = client2.Execute(request2);
                Console.WriteLine(response2.Content);

            }
        }



    }
}
