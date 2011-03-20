For detailed information on ASP.NET Sprites and the Image Optimization Framework, go to http://aspnet.codeplex.com/releases/view/61896

QUICK START:

1) Add your images to the "App_Sprites" directory.
2) Depending on your application type:

****************************
*** ASP.NET Web Forms 4 ****
****************************

<asp:ImageSprite ID="Sprite1" runat="server" ImageUrl="~/App_Sprites/YOUR_IMAGE.jpg" />

**********************************
*** ASP.NET MVC 3 (ASPX Views) ***
**********************************

<%: Sprite.ImportStylesheet("~/App_Sprites/") %>
<%: Sprite.Image("~/App_Sprites/YOUR_IMAGE.jpg") %>

********************************************************
*** ASP.NET MVC 3 (Razor Views) or ASP.NET Web Pages ***
********************************************************

@Sprite.ImportStylesheet("~/App_Sprites/")
@Sprite.Image("~/App_Sprites/YOUR_IMAGE.jpg")