using System;
using System.Collections.Generic;

namespace AssetManagement.Models;

public partial class EmployeeAssetsView
{
    public string AssetId { get; set; } = null!;

    public string AssetName { get; set; } = null!;

    public string EmployeeId { get; set; } = null!;

    public string EmployeeName { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime DateOfRequest { get; set; }

    public DateTime DateOfAssign { get; set; }

    public string ReqId { get; set; } = null!;
}
