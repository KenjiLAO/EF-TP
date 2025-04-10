public class EventHistoryDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string AttendanceStatus { get; set; }
    }