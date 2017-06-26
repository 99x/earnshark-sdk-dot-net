# earnshark-sdk-dot-net
 
This is the .NET SDK to call https://app.earnshark.com API. Contains methods to call the EarnShark API making the application integration fast. 

## This Plugin Requires
* .NET Runtime Environment

## Installation

`Install-Package earnshark-sdk-dot-net`

Through this you will be able to access the EarnShark API through your code.

After installation of the EarnShark SDK package call the following command in your main function: `RunAsync().Wait();`  
Then create a method: 'static async Task RunAsync(){}'  
Within the method initialize the sdk with 'earnshark-sdk earnsharksdk = new earnsharksdk();'

## Available Methods
* getAccountInformation - Retrieve information on a particular account/subscription
* getLicenseInformation - Retrieve information on a particular license
* getAllLicensesOfProduct - Retrieve all the license data for a particular product
* addNewSubscription - Add a new subscription to a product
* getAccountPayments - Returns all the payment transactions associated with the account
* renewSubscription - Renew/Update a Subscription
* getPaymentToken - Returns the token for transactions
* getTransactionURL - Returns the payment portal URL for a subscription(linked to PayPal)


## Usage Sample

Within the RunAsync mehtos enter the code to call the available methods

Initialize the ID's needed

```.NET
string key = 'YOUR KEY FROM EarnShark Dashboard';

string account_id = 'ACCOUNT_ID';

int product_id = 'PRODUCT_ID'; // integer
```
### To retrieve the account information 
```.NET
JToken account = await earnsharksdk.getAccountInformation(product_id, account_id, key);
```
### To get the information related to a license 
```.NET
int license_id = 'ID'; // integer

string license_token = 'YOUR LICENSE TOKEN';

JToken license = await earnsharksdk.getLicenseInformation(product_id, license_token, license_id);
```
### To get all the product licenses 
```.NET
JToken allLicenses = await earnsharksdk.getAllLicensesOfProduct(product_id, key);
```
### To create a new user account 
```.NET
JObject jObject = JObject.Parse(@"
                { ""account"":{
                        ""name"":""Account Name"",
                        ""email"":""Account@example.com"",
                        ""accountID"":""123456"",
                        ""start_date"":""01/01/2016""
                    },
                    ""license_id"":""0"",
                    ""enableNotifications"":""false"",
                    ""sendInvoiceNow"":""true""
                }");

JToken newSubs = await earnsharksdk.addNewSubscription(product_id, key, jObject);
```
### Generate a transaction token for the Payment URL of an Account
```.NET
JToken transactionId = await earnsharksdk.getPaymentToken(body);
string url = earnsharksdk.getTransactionURL(transactionId);
```
### To retrieve All Payment Transactions for an Account
```.NET
JToken accountPayments = await earnsharksdk.getAccountPayments(product_id, account_id, key);
```
### To renew or update a Subscription
```.NET
JToken newSubs = await s.addNewSubscription(product_id, key, jObject);
```


 
