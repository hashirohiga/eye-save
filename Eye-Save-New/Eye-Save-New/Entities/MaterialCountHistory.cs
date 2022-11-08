using System;
using System.Collections.Generic;

namespace Eye_Save_New.Entities
{
    public partial class MaterialCountHistory
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public DateTime ChangeDate { get; set; }
        public double CountValue { get; set; }

        public virtual Material Material { get; set; } = null!;
    }
}
