using poprawa.DTO.Responses;

namespace poprawa.DAL
{
    public interface IDbService
    {
        public GetTasksResponse GetMemberTasks(int id);

        public void DeleteProject(int id);
    }
}