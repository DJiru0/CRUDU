using Crudular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crudular.ViewModel.Home
{
    public class IndexViewModel
    {
        private LitsEntities db = new LitsEntities();
        public IndexViewModel()
        {
            items = db.Items.ToList();
            VendorFilter = db.Items.Select(s => s.Vendor).Distinct().ToList();
            SpeciesFilter = db.Items.Select(s => s.HostSpecies).Distinct().ToList();
            BoxIdFilter = db.Items.Select(s => s.BoxID.ToString()).Distinct().ToList();
        }
        public List<Item> items { get; set; }

        public List<string> VendorFilter { get; set; }

        public List<string> SpeciesFilter { get; set; }

        public List<string> BoxIdFilter { get; set; }
    }
}