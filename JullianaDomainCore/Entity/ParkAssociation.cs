using System;
using System.Collections.Generic;
using System.Text;

namespace JullianaDomainCore.Entity
{
    public class ParkAssociation : EntityBase
    {
        public Park ParkName1 { get; set; }
        public Park ParkName2 { get; set; }
        public ParkAttribute Attribute { get; set; }
        public double ParkRelation { get; set; }
    }
}