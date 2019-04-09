namespace datagrid_mvc5.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;

    public partial class Northwindmy : DbContext
    {
        public Northwindmy()
            : base("name=Northwind")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Territory> Territorys { get; set; }
       public virtual DbSet<NamedTerritory> NamedTerritorys { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Territory>().Property(x => x.TerritoryID);
            modelBuilder.Entity<Territory>()
                 .Property(x => x.TerritoryID);
                //.HasDiscriminator<string>("ContractType");



        }
    }
    public class Territory
    {
        public String TerritoryID { get; set; }
        public String TerritoryDescription { get; set; }
        public int RegionId { get; set; }

    }

    public class NamedTerritory : Territory
    {
        [Required]
        public string Name { get; set; }
    }



}
