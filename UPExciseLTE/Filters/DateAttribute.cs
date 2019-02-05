using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UPExciseLTE.Filters
{
    public class DateAttribute : RangeAttribute
    {
        public DateAttribute()
          : base(typeof(DateTime), DateTime.Now.AddDays(-7).ToShortDateString(), DateTime.Now.AddDays(7).ToShortDateString()) { }
    }
}