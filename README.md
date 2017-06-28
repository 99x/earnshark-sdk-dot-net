# earnshark-sdk-dot-net

[![Join the chat at https://gitter.im/99xt/earnshark-sdk-js](https://badges.gitter.im/99xt/earnshark-sdk-js.svg)](https://gitter.im/99xt/earnshark-sdk-js?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![serverless](http://public.serverless.com/badges/v3.svg)](http://www.serverless.com)
[![NuGet version](https://badge.fury.io/nu/earnshark-sdk-dot-net.svg)](https://badge.fury.io/nu/earnshark-sdk-dot-net)
[![license](https://img.shields.io/npm/l/earnshark-sdk.svg)](https://www.npmjs.com/package/earnshark-sdk)

This is the .NET SDK to call https://app.earnshark.com API. It contains methods to call the EarnShark API making the application integration fast and easy.

## This Plugin Requires
* .NET Runtime Environment 4.0 or later.

## Installation

You can install the package using [NuGet](https://www.nuget.org/) package gallery.

`Install-Package earnshark-sdk-dot-net`

Through this you will be able to access the EarnShark API through your code.

After installation of the EarnShark SDK package call the following command in your main function: `RunAsync().Wait();`
Then create a method: `static async Task RunAsync(){}`
Within the method initialize the sdk with `earnshark-sdk earnsharksdk = new earnsharksdk();`

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

Using RunAsync methods, enter the code to call the above available methods.

Initialize the ID's needed

```csharp
string key = "<YOUR KEY FROM EarnShark Dashboard>";

string account_id = "<ACCOUNT_ID>";

int product_id = "<PRODUCT_ID>"; // integer
```

Alternatively, you can use the application's `app.config` file to save your IDs.

```xml
<add key="key" value=""/>

<add key="account_id" value=""/>

<add key="product_id" value=""/>
```

And access them in your code as follows,

```csharp
string key = ConfigurationManager.AppSettings["key"].ToString();

string account_id = Guid.NewGuid().ToString();

int product_id = Int32.Parse(ConfigurationManager.AppSettings["product_id"]);
```


### To retrieve the account information
```csharp
JArray accountInformation = await earnsharksdk.getAccountInformation(product_id, account_id, key);
```
### To get the information related to a license
```csharp
int license_id = "<ID>"; // integer

string license_token = "<YOUR LICENSE TOKEN>";

JObject licenseInformation = await earnsharksdk.getLicenseInformation(product_id, license_token,license_id);
```
### To get all the product licenses
```csharp
JArray allLicenses = await earnsharksdk.getAllLicensesOfProduct(product_id, key);
```
### To create a new user account
```csharp
JObject user =  new JObject(
                 new JProperty("account",
                    new JObject(
                        new JProperty("name", "<name>"), //string
                        new JProperty("email", "<email>"), //string
                        new JProperty("accountID", "<account_id>"), //string
                        new JProperty("start_date", "<date>" // Date format MM/dd/yyyy)
                    )),
                 new JProperty("license_id", <license_id>), //integer
                 new JProperty("enableNotifications", "true" //boolean),
                 new JProperty("sendInvoiceNow", "false" //boolean)
            );

JObject newUser = await earnsharksdk.addNewSubscription(product_id, key, user);
```
### Generate a transaction token for the Payment URL of an Account
```csharp
JObject body = new JObject(
                 new JProperty("redirect", "<redirect_url>"), //string
                 new JProperty("account_id", "<account_id>"), //string
                 new JProperty("product_id", product_id), //integer
                 new JProperty("key", key)
            );

JObject transactionId = await earnsharksdk.getPaymentToken(body);

string url = earnsharksdk.getTransactionURL(transactionId);
```
### To retrieve All Payment Transactions for an Account
```csharp
JArray accountPayments = await earnsharksdk.getAccountPayments(product_id, account_id, key);
```
### To renew or update a Subscription
```csharp
JObject newSubs = await earnsharksdk.addNewSubscription(product_id, key, jObject);
```
