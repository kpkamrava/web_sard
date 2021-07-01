namespace web_lib.zarinpal
{
    public class pay
    {
        private string _MerchantID, _CallbackURL, _urlpay = "https://www.zarinpal.com/pg/StartPay/";


        public pay(string MerchantID, string CallbackURL)
        {
            _MerchantID = MerchantID;

            _CallbackURL = CallbackURL;

        }

        public async System.Threading.Tasks.Task<string> StartPayAsync(int _Amount, string _Description, string _Email = "", string _Mobile = "")
        {

            try
            {
                ZarniPal.PaymentGatewayImplementationServicePortTypeClient request = new ZarniPal.PaymentGatewayImplementationServicePortTypeClient();

                var value = await request.PaymentRequestAsync(_MerchantID, _Amount, _Description, _Email, _Mobile, _CallbackURL);
                if (value.Body.Status > 0)
                {

                    return value.Body.Authority;
                }
                else
                {
                    return value.ToString();
                }
            }
            catch
            {
                return null;
            }
        }
        public string URL
        {
            get { return _urlpay; }
        }
        public async System.Threading.Tasks.Task<ZarniPal.PaymentVerificationResponse> CheckPaymentStatusAsync(string autohority, int _Amount)
        {
            ZarniPal.PaymentGatewayImplementationServicePortTypeClient request = new ZarniPal.PaymentGatewayImplementationServicePortTypeClient();

            return await request.PaymentVerificationAsync(_MerchantID, autohority, _Amount);


        }
        public string MerchantID
        {
            get { return _MerchantID; }
        }
        public string CallbackURL
        {
            get { return _CallbackURL; }
        }

        public class PayArgs
        {
            private int _Status;
            private string _Autohority;
            private long _RefID;
            public PayArgs(int Status, string Autohority, long RefID)
            {
                _Status = Status;
                _Autohority = Autohority;
                _RefID = RefID;
            }
            public int Status
            {
                get { return _Status; }
            }
            public string Autohority
            {
                get { return _Autohority; }
            }
            public long RefID
            {
                get { return _RefID; }
            }
            private string GetFromLastSlash(string text)
            {
                int where = text.LastIndexOf('/');
                return text.Substring(where);
            }
        }
    }

}
