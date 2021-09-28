using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Commander.Models
{
    public class PeriodCommand
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class LocationCommand
    {
        public string key { get; set; }
        public string value { get; set; }
    }
    public class RequestsCommand
    {
        public string key { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string info { get; set; }
    }
    public class Command
    {
        [Key]
        [Required]
        [MaxLength(250)]
        public string task_id { get; set; }

        [Required]
        public bool completed { get; set; }

        public string result { get; set; }
        [NotMapped]
        public bool valid { get; set; }
        [NotMapped]
        public string url { get; set; }
        [NotMapped]
        public string[] doc_types { get; set; }
        [NotMapped]
        public List<PeriodCommand> periodsCommand { get; set; }
        [NotMapped]
        public List<LocationCommand> locationCommands { get; set; }
        [NotMapped]
        public List<RequestsCommand> requestsCommands { get; set; }
        [NotMapped]
        public string response { get; set; }
        [NotMapped]
        public string phone { get; set; }
        [NotMapped]
        public string error_code { get; set; }
        [NotMapped]
        public string error_text { get; set; }
    }
}