using System;
using System.Collections.Generic;

namespace AssetManagement.Models;

public partial class EmployeeHavingAsset
{
    public string EmployeeId { get; set; } = null!;

    public string AssetId { get; set; } = null!;

    public DateTime DateOfAssign { get; set; }

    public string ReqId { get; set; } = null!;
}
