﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealState.Controllers
{
    public class AgentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}