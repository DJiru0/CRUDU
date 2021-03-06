//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Crudular.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.ItemLocations = new HashSet<ItemLocation>();
        }
    
        public int ItemID { get; set; }
        [Display(Name = "Box ID")]
        public int BoxID { get; set; }
        [Display(Name = "Box Size")]
        public int BoxSize { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Display(Name = "Order Date")]
        public System.DateTime OrderDate { get; set; }
        [Display(Name = "Vendor")]
        public string Vendor { get; set; }
        [Display(Name = "Host Species")]
        public string HostSpecies { get; set; }
        [Display(Name ="Working Dilution")]
        public string WorkingDilution { get; set; }
        [Display(Name = "Quantity")]
        public string Quantity { get; set; }
        [Display(Name = "POTS Number")]
        public string POTSNumber { get; set; }
        [Display(Name = "Catalog Number")]
        public string CatalogNumber { get; set; }
        [Display(Name = "Notes")]
        public string Notes { get; set; }
        [Display(Name = "Is Hazardous")]
        public bool IsHazardous { get; set; }
        [Display(Name = "Location")]
        public string Location { get; set; }

        public virtual Box Box { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemLocation> ItemLocations { get; set; }
    }
}
