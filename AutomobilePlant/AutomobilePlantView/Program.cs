using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutomobilePlantBusinessLogic.BusinessLogics;
using AutomobilePlantContracts.BusinessLogicsContracts;
using AutomobilePlantContracts.StoragesContracts;
using AutomobilePlantFileImplement.Implements;
using AutomobilePlantFileImplement;
using Unity;
using Unity.Lifetime;

namespace AutomobilePlantView
{
    static class Program
    {
        private static IUnityContainer container = null;

        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run((Container.Resolve<FormMain>()));
            FileDataListSingleton.SaveFileDataListSingleton();
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IDetailStorage,
            DetailStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderStorage, OrderStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICarStorage, CarStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDetailLogic, DetailLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICarLogic, CarLogic>(new
            HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
