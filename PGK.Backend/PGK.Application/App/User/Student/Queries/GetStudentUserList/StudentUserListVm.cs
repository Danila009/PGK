namespace PGK.Application.App.User.Student.Queries.GetStudentUserList
{
    public class StudentUserListVm
    {
        public int Count
        {
            get
            {
                return Students.Count;
            }
        }
        public IList<StudentDto> Students { get; set; } = new List<StudentDto>();
    }
}
