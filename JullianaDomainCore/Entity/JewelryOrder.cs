using JullianaApi.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace JullianaDomainCore.Entity
{
    public partial class JewelryOrder : EntityBase
    {
        public ApplicationUser User { get; set; }
        public string Name { get; set; }
        public string JewelryType { get; set; }
        public string GemType { get; set; }
        public string Price { get; set; }
    }

    public partial class JewelrySaved : EntityBase
    {
        public ApplicationUser User { get; set; }
        public string Name { get; set; }
        public string JewelryType { get; set; }
        public string GemType { get; set; }
        public string Price { get; set; }
    }
}