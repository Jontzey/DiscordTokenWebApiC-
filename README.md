# DiscordTokenWebApiCsharp
 small project to exchange token with discord api and get some user data with httpclient

#How to make it work!
1. In launch settings json there is environment variables where you will put your client id and client secret from Discord application.
2. From the Discord dev portal you need an generated url with the box identity checked.
3. Paste the url in your browser and then authenticate.
4. Take the code parameter from the url ctrl + c (the string after "code=") and paste it in GetToken request method on swagger.
5. You got an response with a access and refresh token, take the accesstoken and paste it in the PostToken request method on swagger.
6. And voila!! you got data.

# Information
this is just a small project to see how oauth2 works and get a better understanding. If you want to publish the code or have something in a public repo just like mine, be sure to not expose your client id or secret
as the lunch.settings.json will follow with the push and therefore i replaced my local enviromentvariables with "xxxxx"

# Thank you for visiting my page
