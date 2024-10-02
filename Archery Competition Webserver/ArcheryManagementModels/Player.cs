using System;
using System.Collections.Generic;

namespace Archery_Competition_Webserver.ArcheryManagementModels;

public partial class Player
{
    public int PlayerId { get; set; }

    public int? ClubId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? BowType { get; set; }

    public string? Gender { get; set; }

    public virtual ICollection<Bow> Bows { get; set; } = new List<Bow>();

    public virtual Club? Club { get; set; }

    public virtual ICollection<Game> GameLosers { get; set; } = new List<Game>();

    public virtual ICollection<Game> GamePlayers { get; set; } = new List<Game>();

    public virtual ICollection<Game> GameWinners { get; set; } = new List<Game>();

    public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();
}
