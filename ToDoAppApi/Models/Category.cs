using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("categories")]
public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("category_name")]
    public string CategoryName { get; set; }
    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
    [Required]
    [Column("user_id")]
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    public ICollection<Todo> Todos { get; set; } = new List<Todo>();

}
