using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Commander.Dtos
{
    public class PeriodCreateDto
    {
        public string key { get; set; }
        public string value { get; set; }
    }
    public class LocationCreateDto
    {
        public string key { get; set; }
        public string value { get; set; }
    }
    public class RequestsCreateDto
    {
        public string key { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string info { get; set; }
    }
    public class CommandCreateDto
    {
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
        public List<PeriodCreateDto> periodCreateDto { get; set; }
        [NotMapped]
        public List<LocationCreateDto> locationCreateDto { get; set; }
        [NotMapped]
        public List<RequestsCreateDto> requestsCreateDto { get; set; }
        [NotMapped]
        public string locations { get; set; }
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