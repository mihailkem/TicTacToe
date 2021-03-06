﻿using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TicTacToe.Models;

namespace TicTacToe
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            Database.SetInitializer<TicTacToeContext>(null);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<AutomapperProfile>());
            Container.IocContainer.Setup();
           
        }
    }
}
