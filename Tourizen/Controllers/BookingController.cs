using System;
using System.Web.Mvc;
using Tourizen.Models;
using Tourizen.ViewModels;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using PayPal.AdaptivePayments;
using PayPal.AdaptivePayments.Model;
using System.Web;

namespace Tourizen.Controllers
{
    public class BookingController : Controller
    {
        private MainDBContext db = new MainDBContext();

        //
        // GET: /Booking/

        public ActionResult Confirmation()
        {
            GuestDetailForm model = new GuestDetailForm();
            return View(model);
        }

        [HttpPost]
        public ActionResult Confirmation(GuestDetailForm model)
        {
            if (ModelState.IsValid)
            {
                var detail = Session["Detail"] as DetailModel;

                GuestModel Guest = new GuestModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Country = model.Country
                };

                db.Guests.Add(Guest);
                db.SaveChanges();

                detail.Booking.BookingStatus = BookingStatus.NEW;
                detail.Booking.Paid = false;
                detail.Booking.GuestId = Guest.GuestId;

                db.Bookings.Add(detail.Booking);
                db.SaveChanges();

                return RedirectToAction("PayWithPayPal");
            }

            return View(model);

        }

        public ActionResult PayWithPaypal()
        {
            return View();
        }
        

        public ActionResult Pay()
        {
            var detail = Session["Detail"] as DetailModel;

            NameValueCollection parameters = new NameValueCollection();

            ReceiverList receiverList = new ReceiverList();
            receiverList.receiver = new List<Receiver>();
            PayRequest request = new PayRequest();

            RequestEnvelope requestEnvelope = new RequestEnvelope("en_US");
            request.requestEnvelope = requestEnvelope;

            Receiver merchant = new Receiver();
            // (Required) Amount to be paid to the receiver
            merchant.amount = detail.Booking.TotalCharge;
            // Receiver's email address. This address can be unregistered with
            // paypal.com. If so, a receiver cannot claim the payment until a PayPal
            // account is linked to the email address. The PayRequest must pass
            // either an email address or a phone number. Maximum length: 127 characters
            merchant.email = detail.Hotel.MerchantEmail;

            //This receiver is the primary receiver,who pay the fees
            //merchant.primary = false;

            merchant.paymentType = "SERVICE";

            receiverList.receiver.Add(merchant);

            ReceiverList receiverLst = new ReceiverList(receiverList.receiver);
            request.receiverList = receiverLst;

            // The action for this request. Possible values are: PAY – Use this
            // option if you are not using the Pay request in combination with
            // ExecutePayment. CREATE – Use this option to set up the payment
            // instructions with SetPaymentOptions and then execute the payment at a
            // later time with the ExecutePayment. PAY_PRIMARY – For chained
            // payments only, specify this value to delay payments to the secondary
            // receivers; only the payment to the primary receiver is processed.
            request.actionType = "PAY";

            // The code for the currency in which the payment is made; you can
            // specify only one currency, regardless of the number of receivers
            request.currencyCode = "USD";

            // URL to redirect the sender's browser to after canceling the approval
            // for a payment; it is always required but only used for payments that
            // require approval (explicit payments)
            request.cancelUrl = ConfigurationManager.AppSettings["CANCEL_URL"];

            // URL to redirect the sender's browser to after the sender has logged
            // into PayPal and approved a payment; it is always required but only
            // used if a payment requires explicit approval
            request.returnUrl = ConfigurationManager.AppSettings["RETURN_URL"];

            //Possible fee payer: //SENDER,PRIMARYRECEIVER,SECONDARYONLY,EACHRECEIVER
            request.feesPayer = "EACHRECEIVER";

            AdaptivePaymentsService service = null;
            PayResponse response = null;
            try
            {
                // Configuration map containing signature credentials and other required configuration.
                Dictionary<string, string> configurationMap = Configuration.GetAcctAndConfig();

                // Creating service wrapper object to make an API call and loading
                // configuration map for your credentials and endpoint
                service = new AdaptivePaymentsService(configurationMap);

                response = service.Pay(request);
            }
            catch(System.Exception ex)
            {
                return RedirectToAction("Error", new { message = ex.Message });
            }

            Dictionary<string, string> responseValues = new Dictionary<string, string>();
            string redirectUrl = null;

            if (!response.responseEnvelope.ack.ToString().Trim().ToUpper().Equals(AckCode.FAILURE.ToString()) && !response.responseEnvelope.ack.ToString().Trim().ToUpper().Equals(AckCode.FAILUREWITHWARNING.ToString()))
            {
                redirectUrl = ConfigurationManager.AppSettings["PAYPAL_REDIRECT_URL"] + "_ap-payment&paykey=" + response.payKey;
                // The pay key, which is a token you use in other Adaptive Payment APIs 
                // (such as the Refund Method) to identify this payment. 
                // The pay key is valid for 3 hours; the payment must be approved while the 
                // pay key is valid. 
                responseValues.Add("Pay Key", response.payKey);

                // The status of the payment. Possible values are:
                // CREATED – The payment request was received; funds will be transferred once the payment is approved
                // COMPLETED – The payment was successful
                // INCOMPLETE – Some transfers succeeded and some failed for a parallel payment or, for a delayed chained payment, secondary receivers have not been paid
                // ERROR – The payment failed and all attempted transfers failed or all completed transfers were successfully reversed
                // REVERSALERROR – One or more transfers failed when attempting to reverse a payment
                // PROCESSING – The payment is in progress
                // PENDING – The payment is awaiting processing
                responseValues.Add("Payment Execution Status", response.paymentExecStatus);

            }

            responseValues.Add("Acknowledgement", response.responseEnvelope.ack.ToString().Trim().ToUpper());

            return Redirect(redirectUrl);
        }

        public ActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failure()
        {
            return View();
        }

    }
}
