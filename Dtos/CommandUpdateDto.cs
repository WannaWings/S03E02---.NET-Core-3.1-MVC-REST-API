using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Commander.Dtos
{
    public class PeriodUpdateDto
    {
        public string key { get; set; }
        public string value { get; set; }
    }
    public class LocationUpdateDto
    {
        public string key { get; set; }
        public string value { get; set; }
    }
    public class RequestsUpdateDto
    {
        public string key { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string info { get; set; }
    }
    public class CommandUpdateDto
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
        public List<PeriodUpdateDto> periods { get; set; }
        [NotMapped]
        public List<LocationUpdateDto> locationUpdateDtos { get; set; }
        [NotMapped]
        public List<RequestsUpdateDto> requestsUpdateDtos { get; set; }
        [NotMapped]
        public string response { get; set; }
        [NotMapped]
        public string phone { get; set; }
        [NotMapped]
        public string text { get; set; }
        [NotMapped]
        public string error_code { get; set; }
        [NotMapped]
        public string error_text { get; set; }


    }
}