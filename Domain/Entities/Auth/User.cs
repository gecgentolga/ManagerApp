using Domain.Entities.Abstract;

namespace Domain.Entities.Auth;

public class User:IEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public bool Type { get; set; }//0:manager ,1: player
    public int? Manager_Id { get; set; }
    public string? Player_Id { get; set; }
}