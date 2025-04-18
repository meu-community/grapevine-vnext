﻿namespace MEU.GV4.Base.Models;

public class Experience
{
    public decimal Earned { get; set; }
    public decimal Unspent { get; set; }
    public List<ExperienceEntry> Entries { get; set; } = [];
}
