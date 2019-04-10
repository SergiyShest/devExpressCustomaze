using datagrid_mvc5.Models;
using DevExtreme.AspNet.Data;
using Newtonsoft.Json;
using NLog;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace datagrid_mvc5.Controllers
{

    public class OrdersController : Controller
    {


        Northwind _db = new Northwind();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        [HttpGet]
        public ActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var dt = DateTime.Now;
            ActionResult ret = null;
            try
            {
                Debug.WriteLine("=====================Debug=>" + loadOptions);
                logger.Info("=>" + loadOptions);


                loadOptions.PrimaryKey = new[] { "OrderID" };
                System.Diagnostics.Debug.WriteLine(_db.Orders.Count());
                var ordersQuery = from o in _db.Orders
                                  select new
                                  {
                                      o.OrderID,
                                      o.CustomerID,
                                      CustomerName = o.Customer.ContactName,
                                      o.EmployeeID,
                                      EmployeeName = o.Employee.FirstName + " " + o.Employee.LastName,
                                      o.OrderDate,
                                      o.RequiredDate,
                                      o.ShippedDate,
                                      o.ShipVia,
                                      ShipViaName = o.Shipper.CompanyName,
                                      o.Freight,
                                      o.ShipName,
                                      o.ShipAddress,
                                      o.ShipCity,
                                      o.ShipRegion,
                                      o.ShipPostalCode,
                                      o.ShipCountry
                                  };

                var loadResult = DataSourceLoader.Load(ordersQuery, loadOptions);
                 ret = Content(JsonConvert.SerializeObject(loadResult), "application/json");
                var time = DateTime.Now - dt;
                var mess = string.Format("time= s={0}:{1} ", time.Seconds, time.Milliseconds);
                Trace.WriteLine(mess);
                logger.Info("=>" + mess);
            }
            catch (Exception ex)
            {
             logger.Info("=>" + ex.Message);
                throw;
            }
            return ret;
        }

        [HttpGet]
        public ActionResult GetShipCountry(DataSourceLoadOptions loadOptions)
        {
            // loadOptions.PrimaryKey = new[] { "OrderID" };
            var ordersQuery = from o in _db.Orders
                              select new
                              {
                                  o.ShipCountry
                              };
            var json = JsonConvert.SerializeObject(ordersQuery.Distinct().ToList());
            return Content(json, "application/json");
        }


        [HttpPut]
        public ActionResult Put(int key, string values)
        {
            var order = _db.Orders.Find(key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
            {
                Response.StatusCode = 400;
                return Content(ModelState.GetFullErrorMessage(), "text/plain");
            }

            _db.SaveChanges();
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Post(string values)
        {
            var order = new Order();
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
            {
                Response.StatusCode = 400;
                return Content(ModelState.GetFullErrorMessage(), "text/plain");
            }

            _db.Orders.Add(order);
            _db.SaveChanges();

            return new EmptyResult();
        }

        [HttpDelete]
        public ActionResult Delete(int key)
        {
            var order = _db.Orders.Find(key);
            _db.Orders.Remove(order);
            _db.SaveChanges();

            return new EmptyResult();
        }
    }
}