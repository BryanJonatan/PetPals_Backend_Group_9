using System.ComponentModel.DataAnnotations;

namespace PetPals_BackEnd_Group_9.Models
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }

        [Required]
        public int OwnerId { get; set; }
        public User Owner { get; set; } = null!;

        [Required]
        public int SpeciesId { get; set; }
        public Species Species { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Breed { get; set; }

        public int Age { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required, MaxLength(20)]
        public string Status { get; set; } = "available";

        public decimal Price { get; set; }

    }
}
