namespace SmartServe.Application.Models
{
    public class VehicleAIDataModel
    {
        public dynamic Vehicle { get; set; }
        public IEnumerable<dynamic> ServiceJobs { get; set; }
        public IEnumerable<dynamic> UsedProducts { get; set; }
        public IEnumerable<dynamic> Issues { get; set; }
        public IEnumerable<dynamic> ServiceStats { get; set; }
    }
}
