using Crudular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crudular.Services
{
    public class LitsService
    {
        public IQueryable<Item> OrderBySort(IQueryable<Item> records, string sort, string sortDirection)
        {
            if (sort.ToLower() == "boxid")
            {
                if (sortDirection.ToLower() == "asc")
                    return records.OrderBy(r => r.BoxID);
                else
                    return records.OrderByDescending(r => r.BoxID);
            }
            if (sort.ToLower() == "catalognumber")
            {
                if (sortDirection.ToLower() == "asc")
                    return records.OrderBy(r => r.CatalogNumber);
                else
                    return records.OrderByDescending(r => r.CatalogNumber);
            }
            if (sort.ToLower() == "hostspecies")
            {
                if (sortDirection.ToLower() == "asc")
                    return records.OrderBy(r => r.HostSpecies);
                else
                    return records.OrderByDescending(r => r.HostSpecies);
            }
            if (sort.ToLower() == "ishazardous")
            {
                if (sortDirection.ToLower() == "asc")
                    return records = records.OrderBy(r => r.IsHazardous);
                else
                    return records.OrderByDescending(r => r.IsHazardous);
            }
            if (sort.ToLower() == "itemid")
            {
                if (sortDirection.ToLower() == "asc")
                    return records = records.OrderBy(r => r.ItemID);
                else
                    return records.OrderByDescending(r => r.ItemID);
            }

            if (sort.ToLower() == "itemname")
            {
                if (sortDirection.ToLower() == "asc")
                    return records = records.OrderBy(r => r.ItemName);
                else
                    return records.OrderByDescending(r => r.ItemName);
            }
            if (sort.ToLower() == "notes")
            {
                if (sortDirection.ToLower() == "asc")
                    return records = records.OrderBy(r => r.Notes);
                else
                    return records.OrderByDescending(r => r.Notes);
            }
            if (sort.ToLower() == "orderdate")
            {
                if (sortDirection.ToLower() == "asc")
                    return records = records.OrderBy(r => r.OrderDate);
                else
                    return records.OrderByDescending(r => r.OrderDate);
            }
            if (sort.ToLower() == "potsnumber")
            {
                if (sortDirection.ToLower() == "asc")
                    return records = records.OrderBy(r => r.POTSNumber);
                else
                    return records.OrderByDescending(r => r.POTSNumber);
            }
            if (sort.ToLower() == "quantity")
            {
                if (sortDirection.ToLower() == "asc")
                    return records = records.OrderBy(r => r.Quantity);
                else
                    return records.OrderByDescending(r => r.Quantity);
            }
            if (sort.ToLower() == "vendor")
            {
                if (sortDirection.ToLower() == "asc")
                    return records = records.OrderBy(r => r.Vendor);
                else
                    return records.OrderByDescending(r => r.Vendor);
            }
            if (sort.ToLower() == "workingdilution")
            {
                if (sortDirection.ToLower() == "asc")
                    return records = records.OrderBy(r => r.WorkingDilution);
                else
                    return records.OrderByDescending(r => r.WorkingDilution);
            }
            return records;
        }

        public List<Item> GetInventory(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            using (LitsEntities db = new LitsEntities())
            {
                var records = (from a in db.Items
                               where
                                   a.BoxID.ToString().Contains(search) ||
                                   a.CatalogNumber.ToString().Contains(search) ||
                                   a.HostSpecies.Contains(search) ||
                                   a.IsHazardous.ToString().Contains(search) ||
                                   a.ItemID.ToString().Contains(search) ||
                                   a.ItemName.Contains(search) ||
                                   a.Notes.Contains(search) ||
                                   a.OrderDate.ToString().Contains(search) ||
                                   a.POTSNumber.Contains(search) ||
                                   a.Quantity.Contains(search) ||
                                   a.Vendor.Contains(search) ||
                                   a.WorkingDilution.Contains(search)
                               select a);
                totalRecord = records.Count();
                string sorting = sort + " " + sortdir;
                records = OrderBySort(records, sort, sortdir);


                if (pageSize > 0)
                {
                    records = records.Skip(skip).Take(pageSize);
                }
                return records.ToList();
            }

        }

        public List<string> GetSpeciesFilter()
        {
            using (LitsEntities db = new LitsEntities())
            {
                return db.Items.Select(s => s.HostSpecies).Distinct().ToList();
            }
        }

        public List<string> GetVendorFilter()
        {
            using (LitsEntities db = new LitsEntities())
            {
                return db.Items.Select(s => s.Vendor).Distinct().ToList();
            }
        }

        public List<string> GetBoxIdFilter()
        {
            using (LitsEntities db = new LitsEntities())
            {
                return db.Items.Select(s => s.BoxID.ToString()).Distinct().ToList();
            }
        }

        public string[][] getGrindInfo(int id)
        {
            using (LitsEntities db = new LitsEntities())
            {
                var items = db.Items.Where(r => r.BoxID == id).ToList();
                string[][] info = new string[items.Count][];
                for (int i = 0; i < items.Count; i++)
                {
                    
                    for (int j = 0; j < 2; j++)
                    {
                        if (j % 2 == 0)
                            info[i][j] = "A" + j;
                        else
                            info[i][j] = items[i].ItemName;
                    }
                }
                return info;
            }
        }
    }
}