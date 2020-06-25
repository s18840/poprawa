using System;

namespace poprawa.DTO.Responses
{
    public class MemberTasks
    {
        public string TaskName { get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; }

        public string ProjectName { get; set; }

        public string TaskTypeName { get; set; }
    }
}