using System.Collections.Generic;

namespace poprawa.DTO.Responses
{
    public class GetTasksResponse
    {
        public int IdTeamMember { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<MemberTasks> Tasks { get; set; }
    }

}