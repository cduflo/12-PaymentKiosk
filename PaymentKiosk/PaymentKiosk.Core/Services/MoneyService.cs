using PaymentKiosk.Core.Domain;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PaymentKiosk.Core.Services
{
    public class MoneyService
    {
        private const string APIKey = "sk_test_4abUSbJ1Khdhi6X3GWGkHoQ2";

        public static bool Charge(Customer cus, CreditCard cc, decimal amount) //PRD)
        {
            var chargeDetails = new StripeChargeCreateOptions();
            chargeDetails.Amount = (int)amount * 100;
            chargeDetails.Currency = "usd";

            chargeDetails.Source = new StripeSourceOptions
            {
                Object = "card",
                Number = cc.CardNum,
                ExpirationMonth = cc.Expiration.Substring(0, 2),
                ExpirationYear = cc.Expiration.Substring(3, 2),
                Cvc = cc.CVC
            };

            var chargeService = new StripeChargeService(APIKey);
            var response = chargeService.Create(chargeDetails);

            if (response.Paid == false)
            {
                throw new Exception(response.FailureMessage);
            }

            return response.Paid;

        }

    }
}
