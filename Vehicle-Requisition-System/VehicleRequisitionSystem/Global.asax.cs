﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using VehicleRequisitionSystem.Models.EntityModels;
using VehicleRequisitionSystem.Models.ViewModels;

namespace VehicleRequisitionSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfAttribute), typeof(RequiredIfValidator));


            //Mapper
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Request, RequestListVM>();
                //cfg.CreateMap<Order, OrderDto>();
            } );
        }
    }
}
