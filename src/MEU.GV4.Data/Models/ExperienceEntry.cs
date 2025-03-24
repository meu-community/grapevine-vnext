namespace MEU.GV4.Data.Models
{
    public enum ExperienceChangeType
    {
        Earned,
        Deducted,
        SetEarned,
        Spent,
        Unspent,
        SetUnspent,
        Comment
    }

    public class ExperienceEntry
    {
        public DateTimeOffset? EntryDate { get; set; }
        public decimal Change { get; set; }
        public ExperienceChangeType Type { get; set; }
        public string? Reason { get; set; }
        public decimal Earned { get; set; }
        public decimal Unspent { get; set; }
    }
}
