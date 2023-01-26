namespace PGK.WebApi.Models.Raportichka
{
    public class UpdateRaportichkaRowModel
    {
        public int NumberLesson { get; set; }
        public int Hours { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int RaportichkaId { get; set; }
    }
}
