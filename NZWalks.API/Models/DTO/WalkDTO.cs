namespace NZWalks.API.Models.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        // Navigation properties
        public RegionDTO RegionDTO { get; set; }
        public WalkDifficultyDTO WalkDifficultyDTO { get; set; }
    }
}
