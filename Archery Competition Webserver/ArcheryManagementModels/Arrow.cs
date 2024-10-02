using System;
using System.Collections.Generic;

namespace Archery_Competition_Webserver.ArcheryManagementModels;

public partial class Arrow
{
    public int ArrowId { get; set; }

    public int? RoundId { get; set; }

    public sbyte? ArrowNum { get; set; }

    public sbyte? Score { get; set; }

    public virtual Round? Round { get; set; }
}
