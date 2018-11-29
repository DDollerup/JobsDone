using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsDone.Models
{
    public class CaseVM
    {
        public Case Case { get; set; }
        public User User { get; set; }
        public List<TaskVM> Tasks { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var task in Tasks) totalPrice += task.Relation.Price;
                return totalPrice;
            }
        }
    }
}