using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NJUArchery_SQL_Component.ArcheryModels;

public partial class Equipment
{
    public int EquipmentId { get; set; }

    public string? Name { get; set; }

    /// <summary>
    /// Only for bow and bow piece.
    /// </summary>
    public int? Pounds { get; set; }

    /// <summary>
    /// Owner
    /// </summary>
    public int? PlayerId { get; set; }

    /// <summary>
    /// Owner
    /// </summary>
    public int? ClubId { get; set; }

    /// <summary>
    /// To describe the type of this equipment, for example: Bow, BowString, etc. Decided by encoder from datareader.
    /// </summary>
    /// 
    [Description("Enum: 1 as Traditional Bow, 2 as Recurve Bow, 3 as Recurve Bow Piece")]
    public int? Type { get; set; }

    public string? Note { get; set; }

    public virtual Club? Club { get; set; }

    public virtual Player? Player { get; set; }
}
