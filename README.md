SyncMyCal
=========

Since Google no longer offers an easy way to sync your Outlook calendar to your Google 
calendar and i didn't want to pay money for such a basic feature i felt like i need 
to take action and create some nifty little tool. Well this is the outcome.

Build
-----
Just download/check out/clone the repository and build the project. 
NuGet should take care of the missing libraries on the first build (package restore)

Since using Google Services requires an API-Key you might run out of available requests if
a lot of other users will use this tool too. So you maybe want to create an API Key for your self.
Just visit [this link at Google Developer](https://developers.google.com/api-client-library/python/guide/aaa_apikeys) for more details

These Keys need to be put in the GoogleCalendar Class-File:

```csharp
  ClientSecrets secrets = new ClientSecrets 
  { 
      ClientId = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx.apps.googleusercontent.com", 
      ClientSecret = "yyyyyyyyyyyyyyyyyyyyyyyyy" 
  };
```

Install
-------
Since this is a very basic tool you don't need any installation, just copy the .exe
to some place on your HDD and run it. 


Use
---
After starting the app just hit the "Add" button and connect to the calendars you would
like to use. (This is limited to Outlook -> Google right now - but an update is coming, promise!)

Thats it, SyncMyCal will now sync every interval of minutes configured by you.

Problems
--------
If you have any issues what soever please create an issue here at github.

Have fun with SyncMyCal, i hope it helps you as much as it helps me.

[@bigbasti](https://twitter.com/bigbasti)
