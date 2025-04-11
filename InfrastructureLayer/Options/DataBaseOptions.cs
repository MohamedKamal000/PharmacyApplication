namespace InfrastructureLayer
{
    public class DataBaseOptions
    {
        public string SqlConnection { get; set; } = null!;
        
        public string SystemBehaviour { get; set; } = null!;

        public string AdminBehaviour { get; set; } = null!;
    }
}