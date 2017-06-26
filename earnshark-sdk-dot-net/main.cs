using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace earnshark_sdk_dot_net
{
    public class Class1
    {
        static HttpClient client = new HttpClient();

        static string baseURL = "https://app.earnshark.com/prod/";
        static string appDir = "http://earnsharkbeta.com.s3-website-eu-west-1.amazonaws.com/";

        public async Task<JToken> addNewSubscription(int product_id, string key, JObject jObject)
        {
            JToken newSubscription = null;
            HttpResponseMessage response = await client.PostAsJsonAsync(baseURL + "product/" + product_id.ToString() + "/addsubscriptionfromapi?key=" + key.ToString(), jObject);

            if (response.IsSuccessStatusCode)
            {
                string temp = await response.Content.ReadAsStringAsync();

                newSubscription = JToken.Parse(temp);
            }

            return newSubscription;
        }

        public async Task<JToken> getAccountInformation(int product_id, string account_id, string key)
        {
            string path = baseURL + "product/" + product_id + "/subscriptioninfo/" + account_id + "?key=" + key;

            JToken account = null;

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string temp = await response.Content.ReadAsStringAsync();
                account = JToken.Parse(temp);
            }
            return account;
        }

        public async Task<JToken> renewSubscription(int product_id, string key, int subscription_id, int new_license_id)
        {
            string path = baseURL + "product/" + product_id + "/subscription/" + subscription_id + "/apiRenewSubscription/" + new_license_id + "?key=" + key;

            JToken subscription = null;

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string temp = await response.Content.ReadAsStringAsync();
                subscription = JToken.Parse(temp);
            }
            return subscription;
        }

        public async Task<JToken> getLicenseInformation(int product_id, string key, int license_id)
        {
            string path = baseURL + "product/" + product_id + "/license/" + license_id + "/getlicensefromapi?key=" + key;

            JToken license = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string temp = await response.Content.ReadAsStringAsync();
                license = JToken.Parse(temp);
            }
            return license;
        }

        public async Task<JToken> getAccountPayments(int product_id, string account_id, string key)
        {
            string path = baseURL + "product/" + product_id + "/account/" + account_id + "/transactions?key=" + key;

            JToken payments = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string temp = await response.Content.ReadAsStringAsync();
                payments = JToken.Parse(temp);
            }
            return payments;
        }

        public async Task<JToken> getAllLicensesOfProduct(int product_id, string key)
        {
            string path = baseURL + "product/" + product_id + "/license/all?key=" + key;

            JToken licenses = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string temp = await response.Content.ReadAsStringAsync();
                licenses = JToken.Parse(temp);
            }
            return licenses;
        }

        public async Task<JToken> getPaymentToken(JObject body)
        {
            JToken transactionId = null;
            HttpResponseMessage response = await client.PostAsJsonAsync(baseURL + "payments/getTransactionID", body);

            if (response.IsSuccessStatusCode)
            {
                string temp = await response.Content.ReadAsStringAsync();
                transactionId = JToken.Parse(temp);
            }

            return transactionId;
        }

        public string getTransactionURL(JToken key)
        {
            JToken shortToken = key.Value<string>("shortToken");
            return appDir + "payment2.html?transactionID=" + shortToken;
        }
    }
}
