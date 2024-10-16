using System;
using System.Collections.Generic;

namespace NJUArchery_SQL_Component.ArcheryModels;

public partial class Player
{
    public int PlayerId { get; set; }

    public int? ClubId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? BowType { get; set; }

    public string? Gender { get; set; }

    public virtual Club? Club { get; set; }

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();

    public virtual ICollection<Game> GamePlayerAs { get; set; } = new List<Game>();

    public virtual ICollection<Game> GamePlayerBs { get; set; } = new List<Game>();

    public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();
}
