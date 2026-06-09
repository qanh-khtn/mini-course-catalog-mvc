namespace MiniCourseCatalog.Mvc.Options;

public class TrainingCenterConfig
{
    public const string SectionName = "TrainingCenterConfig";
    public int LowSeatThreshold { get; set; } = 3;
    public string CenterName { get; set; } = "Mini Training Center";
}
