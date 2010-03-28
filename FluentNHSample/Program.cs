using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHSample.Entities;
using NHibernate;
using FluentNHSample.Conventions;
using FluentNHibernate.Conventions.Helpers;

namespace FluentNHSample {
    class Program {
        static void Main(String[] args) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = CreateSessionFactory();
            }
            catch (Exception e) {
                Console.WriteLine("Exception occurred: " + e.Message);
            }
            Console.WriteLine("Session factory was built");

            using (var session = sessionFactory.OpenSession()) {
                using (var transaction = session.BeginTransaction()) {
                    // create a couple of Stores each with some Products and Employees
                    var barginBasin = new Store { Name = "Bargin Basin" };
                    var superMart = new Store { Name = "SuperMart" };

                    var potatoes = new Product { Name = "Potatoes", Price = 3.60 };
                    var fish = new Product { Name = "Fish", Price = 4.49 };
                    var milk = new Product { Name = "Milk", Price = 0.79 };
                    var bread = new Product { Name = "Bread", Price = 1.29 };
                    var cheese = new Product { Name = "Cheese", Price = 2.10 };
                    var waffles = new Product { Name = "Waffles", Price = 2.41 };

                    var daisy = new Employee { FirstName = "Daisy", LastName = "Harrison" };
                    var jack = new Employee { FirstName = "Jack", LastName = "Torrance" };
                    var sue = new Employee { FirstName = "Sue", LastName = "Walkters" };
                    var bill = new Employee { FirstName = "Bill", LastName = "Taft" };
                    var joan = new Employee { FirstName = "Joan", LastName = "Pope" };

                    // add products to the stores, there's some crossover in the products in each
                    // store, because the store-product relationship is many-to-many
                    AddProductsToStore(barginBasin, potatoes, fish, milk, bread, cheese);
                    AddProductsToStore(superMart, bread, cheese, waffles);

                    // add employees to the stores, this relationship is a one-to-many, so one
                    // employee can only work at one store at a time
                    AddEmployeesToStore(barginBasin, daisy, jack, sue);
                    AddEmployeesToStore(superMart, bill, joan);

                    // save both stores, this saves everything else via cascading
                    session.SaveOrUpdate(barginBasin);
                    session.SaveOrUpdate(superMart);

                    transaction.Commit();
                }

                // retreive all stores and display them
                using (session.BeginTransaction()) {
                    var stores = session.CreateCriteria<Store>().List<Store>();

                    foreach (Store store in stores) 
                        Console.WriteLine("id: {0} name: {1} employee count: {2}", store.Id, store.Name, store.Staff.Count);       
                }

                Console.WriteLine("Press any key to quit");
                Console.ReadKey();
            }
        }

        private static ISessionFactory CreateSessionFactory() {
            Console.WriteLine("Building session factory");
            return Fluently.Configure()
                   .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("Database"))).
                    Mappings(m => 
                            m.FluentMappings.AddFromAssemblyOf<Program>()
                            .Conventions.AddFromAssemblyOf<CustomForeignKeyConvention>()
                            )
                    .BuildSessionFactory();
        }

        public static void AddProductsToStore(Store store, params Product[] products) {
            foreach (var product in products) 
                store.addProduct(product);
        }

        public static void AddEmployeesToStore(Store store, params Employee[] employees) {
            foreach (var employee in employees) 
                store.addEmployee(employee);
        }
    }
}
