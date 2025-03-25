namespace MEU.GV4.Data.Models.METClassic
{
    public abstract class METCharacter : Character
    {
        public string? Title { get; set; }
        public string? Nature { get; set; }
        public string? Demeanor { get; set; }
        public TraitList? PhysicalTraits { get; set; }
        public TraitList? SocialTraits { get; set; }
        public TraitList? MentalTraits { get; set; }
        public TraitList? NegativePhysicalTraits { get; set; }
        public TraitList? NegativeSocialTraits { get; set; }
        public TraitList? NegativeMentalTraits { get; set; }
        public int PhysicalMax { get; set; }
        public int SocialMax { get; set; }
        public int MentalMax { get; set; }
        public int Willpower { get; set; }
        public int WillpowerTemp { get; set; }
        public TraitList? Abilities { get; set; }
        public TraitList? Influences { get; set; }
        public TraitList? Backgrounds { get; set; }
        public TraitList? Derangements { get; set; }
        public TraitList? Merits { get; set; }
        public TraitList? Flaws { get; set; }
        public TraitList? Health { get; set; }
        public TraitList? Miscellanious { get; set; }

    }
}
