using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WiredBrainCoffee.Models;
using WiredBrainCoffee.Services;

namespace WiredBrainCoffee.Pages
{
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
}
