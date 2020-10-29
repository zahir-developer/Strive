namespace Admin.API
{
    public class SendMessageDto
    {
        public string ConnectionId { get; set; }
        public int EmployeeId { get; set; }
        public string Initial { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }

    }
}