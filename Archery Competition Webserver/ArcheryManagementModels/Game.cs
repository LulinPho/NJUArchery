using System;
using System.Collections.Generic;

namespace Archery_Competition_Webserver.ArcheryManagementModels;

public partial class Game
{
    public int GameId { get; set; }

    public int? CompetitionId { get; set; }

    public string? GameType { get; set; }

    /// <summary>
    /// qualification
    /// </summary>
    public int? PlayerId { get; set; }

    /// <summary>
    /// qualification
    /// </summary>
    public short? ScoreSum { get; set; }

    /// <summary>
    /// matchplay
    /// </summary>
    public int? WinnerId { get; set; }

    /// <summary>
    /// matchplay
    /// </summary>
    public int? LoserId { get; set; }

    /// <summary>
    /// matchplay
    /// </summary>
    public sbyte? WinnerSetScore { get; set; }

    /// <summary>
    /// matchplay
    /// </summary>
    public sbyte? LoserSetScore { get; set; }

    /// <summary>
    /// matchplay
    /// </summary>
    public sbyte? MatchplayIdentifier { get; set; }

    public double? Distance { get; set; }

    public string? TargetType { get; set; }

    public string? Half { get; set; }

    public virtual Competition? Competition { get; set; }

    public virtual Player? Loser { get; set; }

    public virtual Player? Player { get; set; }

    public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();

    public virtual Player? Winner { get; set; }
}
