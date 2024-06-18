namespace AssetManagement.Models
{
    public class UpdateEmployeeHavingAssetsVM
    {
        public string EmployeeId { get; set; } = null!;
        public string AssetId { get; set; } = null!;
        public DateTime DateOfAssign { get; set; }
        public string ReqId { get; set; } = null!;
    }
}
