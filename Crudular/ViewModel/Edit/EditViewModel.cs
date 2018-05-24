using Crudular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crudular.ViewModel.Edit
{
    public class EditViewModel
    {

        public EditViewModel(Item item)
        {
            this.item = item;
            this.BoxSize = 9;
        }
        public Item item { get; set; }

        public int BoxSize { get; set; }
    }

}