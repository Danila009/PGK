using System.ComponentModel;

namespace PGK.Domain.Journal
{
    public enum JournalEvaluation
    {
       [Description("2")]
       HAS_2,
       [Description("3")]
       HAS_3,
       [Description("4")]
       HAS_4,
       [Description("5")]
       HAS_5,
       [Description("н")]
       HAS_H,
    }
}
