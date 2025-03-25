﻿namespace MEU.GV4.Data.Models
{
    /// <summary>
    /// The Character class is the base class that all other character types inherit from
    /// </summary>
    public abstract class Character
    {
        public string? Name { get; set; }
        public string? Player { get; set; }
        public string? Status { get; set; }
        public bool IsNPC { get; set; }
        public TraitList Equipment { get; set; } = [];
        public TraitList Hangouts { get; set; } = [];
        public string? Biography { get; set; }
        public string? Notes { get; set; }
        public string? Other { get; set; }
        public Experience CharacterExperience { get; set; } = new Experience();
        public DateTimeOffset? CreateDate { get; set; }
        public DateTimeOffset? ModifyDate { get; set; }
    }
}
