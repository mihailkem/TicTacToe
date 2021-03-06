﻿using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.MicroKernel.Registration;
using TicTacToe.Models;
using TicTacToe.GameRepository;
using AutoMapper;

namespace TicTacToe.Container
{
    public static class IocContainer
    {
        public static IWindsorContainer container;

        public static void Setup()
        {
            container = new WindsorContainer().Install(FromAssembly.This());
                
            container.Register(
                Component.For<IRepository>().ImplementedBy<Repository>()
                .Named("Repository")
                .LifeStyle.Transient                
                .DynamicParameters((r, k) => { k["Context"] = new TicTacToeContext();})                
                );
                    
            container.Register(Component.For<IMapper>().UsingFactoryMethod(x =>
            {
                return new MapperConfiguration(c =>
                {
                    c.AddProfile<AutomapperProfile>();
                }).CreateMapper();
            }));

            container.Register(AllTypes.FromThisAssembly()
                .Pick().If(t => t.Name.EndsWith("Controller"))
                .Configure(configurer => configurer.Named(configurer.Implementation.Name))                
                .LifestylePerWebRequest());

            WindsorControllerFactory controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            //var sched = container.Resolve<IGameRepository>();
        }
    }
}