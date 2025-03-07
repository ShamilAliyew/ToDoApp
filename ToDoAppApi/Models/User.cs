using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("users")]
[Index(nameof(Username), IsUnique = true)] 
[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("first_name")]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("last_name")]
    public string LastName { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("username")]
    public string Username { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("password")]
    public string Password { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("email")]
    public string Email { get; set; }
    [Column("registered_date", TypeName = "DATETIME")]
    public DateTime RegisteredDate { get; set; } = DateTime.Now;
    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;

   
    
    public virtual ICollection<Todo> Todos { get; set; }= new List<Todo>();
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

}
