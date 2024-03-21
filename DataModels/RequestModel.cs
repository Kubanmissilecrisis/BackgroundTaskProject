using System.ComponentModel.DataAnnotations;


namespace WebCheckerAPI.DataModels
{
    public class RequestModel
    {
        [Key]
        public int CardId { get; set; }

        [Required]
        public int CardNumber { get; set; }

        public DateTime Date { get; set; } // in mssql i can use regular DateTime

        [Required]
        public string Status { get; set; } = string.Empty;


    }

    public class RequestSettings
    {
        [Key]
        public int CommandId { get; set; }
        [Required]
        public int TimeAmount { get; set; }
        [Required]
        public string? TimeType { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string? Url { get; set; }
        public int CardID { get; set; }

    }

    public class RequestStatus
    {
        [Key]
        public int ResultId { get; set; }
        [Required]
        public int CardNumber { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string? Url { get; set; }
        [Required]
        public string? Status { get; set; }
    }

    public class CommandObjectsModel
    {
        [Key]
        public int CommandId { get; set; }
        [Required]
        public int TimeAmount { get; set; }
        [Required]
        public string? TimeType { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string? Url { get; set; }
        public int CardID { get; set; }

    }


}