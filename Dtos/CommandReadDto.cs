using System.ComponentModel.DataAnnotations.Schema;

namespace Commander.Dtos
{
    public class CommandReadDto
    {

        public string task_id { get; set; }


        public bool completed { get; set; }

        public string result { get; set; }
        [NotMapped]
        public bool valid { get; set; }
        [NotMapped]
        public string url { get; set; }
        [NotMapped]
        public string[] doc_types { get; set; }
        [NotMapped]
        public string[] periods { get; set; }
        [NotMapped]
        public string[] locations { get; set; }
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