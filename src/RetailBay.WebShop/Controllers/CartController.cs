using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RetailBay.Core.Interfaces;

namespace RetailBay.WebShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICatalogService _catalogService;
        //public HomeController(ICatalogService catalogService)
        //{
        //    _catalogService = catalogService;
        //}
    }
}
