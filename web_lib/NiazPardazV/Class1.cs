using NiazPardaz;
using System.ComponentModel;

namespace web_lib.NiazPardazV
{
    public class Class1
    {
        public string username { get; set; }
        public string password { get; set; }
        public string numSender { get; set; }

        public enum SendSmsReturnType
        {
            [Description("")]
            None = -10,

            [Description("ارسال با موفقیت انجام شد")]
            SendWasSuccessful = 0,

            [Description("نام کاربر یا کلمه عبور نامعتبر می باشد")]
            InvalidUserNameOrPassword = 1,

            [Description("کاربر مسدود شده است")]
            UserBlocked = 2,

            [Description("شماره فرستنده نامعتبر است")]
            InvalidSenderNumber = 3,

            [Description("محدودیت در ارسال روزانه")]
            LimitationInDailySend = 4,

            [Description("تعداد گیرندگان حداکثر 100 شماره می باشد")]
            LimitationInRecieverCount = 5,

            [Description("خط فرسنتده غیرفعال است")]
            SenderLineIsInactive = 6,

            [Description("متن پیامک شامل کلمات فیلتر شده است")]
            SmsContentFilteredWordsIsIncluded = 7,

            [Description("اعتبار کافی نیست")]
            NoCredit = 8,

            [Description("سامانه در حال بروز رسانی است")]
            SystemBeingUpdated = 9,

            [Description("پیاده سازی نشده است")]
            NotImplemented = 10
        }


        public async System.Threading.Tasks.Task<SendSmsReturnType> sendAsync(string reciverNum, string messege)
        {
            var sendServiceClient = new SendServiceClient();
            long[] recId = null;
            byte[] status = null;
            var result = await sendServiceClient.SendSMSAsync(new SendSMSRequest(username, password,
                numSender, new[] { reciverNum },
                messege, false, recId, status));//ref
            var z = (SendSmsReturnType)result.SendSMSResult;
            return z;
        }
        public async System.Threading.Tasks.Task<decimal> GetCreditAsync(string reciverNum, string messege)
        {
            var sendServiceClient = new SendServiceClient();
            var credit = await sendServiceClient.GetCreditAsync(username, password);

            return credit;
        }

    }
}
