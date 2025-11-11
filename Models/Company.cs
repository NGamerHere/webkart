namespace WebCart.Models;

public class Company
{
    public int Id {get ; set ;}
    public required string Name     {get ; set ;}
    public required string GstNumber     {get ; set ;}
    public required string Address  {get ; set ;}
    public required DateTime CreatedAt  {get ; set ;}
    public required DateTime UpdatedAt  {get ; set ;}
    public int UserId { get; set; }
    public User User { get; set; }
}