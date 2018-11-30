using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiredBrainCoffee.Services;

namespace WiredBrainCoffee
{
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
}
