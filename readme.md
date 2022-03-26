## Software Frameworks

## Assignment: Reactive Systems

## Author: se21m024

<br/>

# Summary

This is a Hasura GraphQL demo project.<br/>

# Repository

Please clone the following repository:
<br/>
https://github.com/se21m024/ReactiveSystemsHasuraDemo
<br/><br/>

# Hosting

The Web GUI for the user and the Web API for the Transactions Service are both hosted as App Services in Azure.
![AzurePortal](./Screenshots/AzurePortal.png)
<br/><br/>
They are available at:

- Web GUI: https://hasuragui.azurewebsites.net/
- Web API: https://appservicecode.azurewebsites.net/swagger/index.html

The data is persited in an Heroku database and accessible via the GraphQL enpoint https://transactions-demo.hasura.app/v1/graphql/.<br/><br/>

# Technology

The Web GUI was implemented as .NET 6 Blazor project.
The Web API was implemented as .NET 6 Web Api project.
Both reference a common project '' that capsulates the functionality provided by the GraphQL .NET library Strawberry Shake (https://chillicream.com/docs/strawberryshake).<br/><br/>

# Web GUI (User interface)

Via the user interface, new payments can be created via GraphQL mutations.

![Gui2](./Screenshots/Gui2.png)

The 5 newest payments and transactions can be seen in the bottom of the page. The newly created payment is instantly visible. The new transaction is only visible after it has been created via the Web API (see next chapter) and the 'Reload Hasura Data' button has been clicked (see chapter 'Restrictions').

![Gui3](./Screenshots/Gui3.png)
<br/><br/>

# Web API (Transactions Service)

By calling POST https://appservicecode.azurewebsites.net/Transactions, transactions are created for all open payments via GraphQL mutations. A payment is open as long as no corresponding transaction was created. The web service method returns the number of transactions that were created.

![TransactionsWebApi](./Screenshots/TransactionsWebApi.png)

# Persistence

## Payments Table

![HasuraPayment](./Screenshots/HasuraPayment.png)

## Transactions Table

![HasuraTransactions](./Screenshots/HasuraTransactions.png)
<br/><br/>

# Restrictions

It was planed that the Web API as well as the Web GUI stream changes of the payment and transactions data via GraphQL subscriptions. The necessary code to achive this was implemented but unfortunately I was not able to connect to the websocket of the Hasura Cloud instance. I experienced a 404 NotFound error when trying to connect to the websocket. Even after long investigation and various different approaches I could not manage to resolve this issue. Because of this the two apps poll changes of the data via GraphQL queries.<br/><br/>

![WebSocket-NotFound](./Screenshots/WebSocket-NotFound.png)
<br/><br/>

WebSocket Client Registration
![SubscriptionCode1](./Screenshots/SubscriptionCode0.png)
<br/><br/>

Subscription Code
![SubscriptionCode1](./Screenshots/SubscriptionCode1.png)
<br/><br/>

GraphQL Subscription
![SubscriptionCode1](./Screenshots/SubscriptionCode2.png)
