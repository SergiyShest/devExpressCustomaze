using Microsoft.VisualStudio.TestTools.UnitTesting;
using datagrid_mvc5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace datagrid_mvc5.Models.Tests
{
    [TestClass()]
    public class NorthwindTests
    {
        [TestMethod()]
        public void NorthwindTest()
        {
            Northwindmy nv = new Northwindmy();
            var all=     nv.Territorys.ToList();
            
            var all2 =     nv.NamedTerritorys.ToList();
            foreach(var t in all)
            {
                Debug.WriteLine(t.GetType().Name);
            }
           //  empl.LastName = null;
           // var err=nv.GetValidationErrors().ToList();
           // Assert.AreEqual(0, err.Count());
           // empl.FirstName = null;
           //  err = nv.GetValidationErrors().ToList();
           // Assert.AreEqual(1, err.Count());
           //var err0   =   err[0].ValidationErrors.ToList()[0].ToString();
            

        }
    }
}