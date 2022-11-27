namespace PGK.WebApi.Models.Teacher
{
    public class TeacherUpdateRaportichkaRowModel
    {
        public int NumberLesson { get; set; }
        public bool Confirmation { get; set; }
        public int Hours { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        public int RaportichkaId { get; set; }
    }
}
