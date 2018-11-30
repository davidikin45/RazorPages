# Razor Pages
## Useful Information
* Create a /Pages folder
* Can have multiple forms on the same page
* Partial Views don't have associated C# class

## Configuration
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().AddRazorPagesOptions(opts =>
    {
        opts.Conventions.AddPageRoute("/index", "home");
        opts.Conventions.AddPageRoute("/index", "wired");
        opts.RootDirectory = "/Features";
    });

    services.Configure<RouteOptions>(opts =>
    {
        opts.ConstraintMap.Add("promo", typeof(PromoConstraint));
    });

    services.AddScoped<IMenuService, MenuService>();
    services.AddLogging();
}
```

## Razor Page List
```
@page "/page-route"
@model IndexModel
<div>

</div>
```

```
public class MenuModel : PageModel
{
    private ILogger<MenuModel> logger;
    private IMenuService menuService;

    public List<MenuItem> Menu { get; set; }

    public MenuModel(IMenuService menuService, ILogger<MenuModel> logger)
    {
        this.logger = logger;
        this.menuService = menuService;
    }

    public void OnGet()
    {
        try
        {
            Menu = menuService.GetMenuItems();
            throw new Exception();
        }
        catch (Exception e)
        {
            logger.Log(LogLevel.Debug, "Could not retrieve menu.");
        }
    }
}
```

## Razor Page Item
```
@page "{id}"
@model ItemModel
<div>

</div>
```

```
public class ItemModel : PageModel
{
    public MenuItem Item {get; private set;}
    public void OnGet(int id)
    {
     
    }
    
    public void OnPost(int id)
    {
     
    }
}
```

## Razor Page Contact
```
@page "/thoughts/{token:promo}"
@model ContactModel
```
```
public class ContactModel : PageModel
{
        [BindProperty]
        public Contact Contact { get; set; }
        public string Message { get; private set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                EmailService.SendEmail(Contact);
                return RedirectToPage("Confirmation", "Contact");
            }
            else
            {
                return Page();
            }
        }

        public IActionResult OnPostSubscribe(string address)
        {
            EmailService.SendEmail(address);
            return RedirectToPage("Confirmation", "Subscribe");
        }
}
```

## Useful Tag Helpers
```
<a class="nav-link" asp-page="Index">Home</a>
<a asp-page="item" asp-route-id="@item.id">link</a>
<img asp-append-version="true" class="card-img-top" src="~/images/menu/@item.ImageFile" alt="">
<environment include="Development">
    <link rel="stylesheet" href="~/css/bootstrap.css" />
    <link rel="stylesheet" href="~/css/wiredbrain.css" />
</environment>
<environment include="Production">
    <link rel="stylesheet" href="~/css/bootstrap.css" />
    <link rel="stylesheet" href="~/css/wiredbrain.css" />
</environment>
```

## Form Tag Helpers
```
<form asp-page="Contact" method="post" name="sentMessage" id="contactForm">
    <div class="control-group form-group">
        <div class="controls">
            <label asp-for="Contact.Name">Name:</label>
            <input asp-for="Contact.Name" class="form-control">
            <span asp-validation-for="Contact.Name"></span>
        </div>
    </div>
    <div class="control-group form-group">
        <div class="controls">
            <label asp-for="Contact.Phone">Phone:</label>
            <input asp-for="Contact.Phone" class="form-control">
            <span asp-validation-for="Contact.Phone"></span>
        </div>
    </div>
    <div class="control-group form-group">
        <div class="controls">
            <label asp-for="Contact.Email">Email:</label>
            <input asp-for="Contact.Email" class="form-control">
            <span asp-validation-for="Contact.Email"></span>
        </div>
    </div>
    <div class="control-group form-group">
        <div class="controls">
            <label asp-for="Contact.Message">Message:</label>
            <textarea asp-for="Contact.Message" rows="10" cols="100" class="form-control"></textarea>
            <span asp-validation-for="Contact.Message"></span>
        </div>
    </div>
    <div id="success"></div>
    <button type="submit" class="btn btn-success" id="sendMessageButton">Send Message!</button>
</form>
<form asp-page-handler="Subscribe">
        <div class="control-group form-group">
            <input name="address" type="text" class="form-control" placeholder="Email" />
        </div>
        <div class="control-group form-group">
            <input type="submit" class="btn btn-success" value="Join" />
        </div>
</form>
```
## Caching
https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/cache-tag-helper?view=aspnetcore-2.1
* expires-after
* expires-on
* expires-sliding
* vary-by-cookie
* vary-by-header
* vary-by-query
* vary-by-route
* vary-by-user
```
<cache expires-sliding="TimeSpan.FromSeconds(10)" vary-by-query="time">
</cache>
```

## Partial Views
```
 <partial name="MobileWidget" model="@("Download Today")" />
 ```
 ```
 @model string

<div class="card">
    <div class="card-body">
        <h2>@Model</h2>
        <div class="description">
            <p>Stay wired on the go with these benefits:</p>
            <ul>
                <li>Coupons and promotions</li>
                <li>Brainiacs club benefits</li>
                <li>Store locator</li>
                <li>Latest menu options</li>
            </ul>
        </div>
        <img class="img-fluid rounded" src="~/images/download-small.png" alt="">
    </div>
    <div class="card-footer">
        <a href="#" class="btn btn-success">Download</a>
    </div>
</div>
 ```
 
 ## Sections
 ```
 @section FooterThird {
    <p><a class="text-white" href="#">Allergen Information</a></p>
    <p><a class="text-white" href="#">Ingredient Information</a></p>
    <p><a class="text-white"href="#">Our Food Partners</a></p>
}
```
```
@if (IsSectionDefined("FooterThird"))
{
    RenderSection("FooterThird");
}
else
{
    <p class="m-0 text-white">Coffee Fact of the Day</p>
    <p class="m-0 text-white">More cups of coffee are drank every day than there are people on earth. <i>(May not be true)</i></p>
}
```
 ## View Components
/Pages/Components/Component/Default.html
```
@model IEnumerable<MenuItem>

<div class="card">
    <div class="card-body">
        <h2 class="card-title">Popular Brews</h2>
        @foreach (var item in Model)
        {
            <p>
                <span class="card-text text-success">@item.Name</span>
                @item.Summary
            </p>
        }
    </div>
    <div class="card-footer">
        <a href="#" class="btn btn-success">Full Menu</a>
    </div>
</div>
```
/Components/PopularBrews.cs
```
 public class PopularBrews : ViewComponent
    {
    private IMenuService menuService;

    public PopularBrews(IMenuService menuService)
    {
        this.menuService = menuService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int count)
    {
        return View(menuService.GetPopularItems().Take(count));
    }
}
```
ViewImports.cshtml
```
@using WiredBrainCoffee
@using WiredBrainCoffee.Models
@namespace WiredBrainCoffee.Pages
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, WiredBrainCoffee
```
```
<vc:popular-brews count="1" />
```