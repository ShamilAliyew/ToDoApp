using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoAppApi.Enums;

public class Todo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("title")]
    public string Title { get; set; }
    [Column("description")]
    public string Description { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("priority")]
    [EnumDataType(typeof(Priority))]
    public Priority Priority { get; set; }
    [Column("created_date", TypeName = "DATETIME")]

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    [Column("deadline", TypeName = "DATETIME")]

    public DateTime? Deadline { get; set; }

  
    [NotMapped]
    public int? RemainingDays => Deadline.HasValue ? (int?)(Deadline.Value - DateTime.UtcNow).TotalDays : null;
    [Column("is_completed")]
    public bool IsCompleted { get; set; } = false;
    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;

    [Column("user_id")]
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    [Column("category_id")]
    public int? CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
}
