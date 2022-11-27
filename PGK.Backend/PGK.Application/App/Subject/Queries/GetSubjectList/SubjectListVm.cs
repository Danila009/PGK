namespace PGK.Application.App.Subject.Queries.GetSubjectList
{
    public class SubjectListVm
    {
        public int Count
        {
            get
            {
                return Subjects.Count;
            }
        }
     
        public virtual IList<SubjectDto> Subjects { get; set; } = new List<SubjectDto>();
    }
}
