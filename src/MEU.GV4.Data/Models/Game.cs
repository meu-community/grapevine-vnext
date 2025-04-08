namespace MEU.GV4.Data.Models
{
    /// <summary>
    /// The Game class is the top-level container for all game-related data.
    /// </summary>
    public class Game
    {
        public string? Title { get; set; }
        public string? Website { get; set; }
        public string? EMail { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public string? UsualPlace { get; set; }
        public string? UsualTime { get; set; }


        public List<Player> Players { get; set; } = [];
        public List<Character> Characters { get; set; } = [];
        public List<CalendarEntry> Calendar { get; set; } = [];
    }
}
