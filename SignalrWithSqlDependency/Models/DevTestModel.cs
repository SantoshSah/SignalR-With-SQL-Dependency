using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SignalrWithSqlDependency.Models
{
    public class DevTestModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please Enter CampaignName")]
        public string CampaignName { get; set; }
        [Required(ErrorMessage = "Please Enter Date")]
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Please Enter Clicks")]
        public int? Clicks { get; set; }
        [Required(ErrorMessage = "Please Enter Conversions")]
        public int? Conversions { get; set; }
        [Required(ErrorMessage = "Please Enter Impressions")]
        public int? Impressions { get; set; }
        [Required(ErrorMessage = "Please Enter AffiliateName")]
        public string AffiliateName { get; set; }
    }
}