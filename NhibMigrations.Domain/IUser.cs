namespace NhibMigrations.Domain
{
    public interface IUser
    {
        int Id { get; set; }
        string Email { get; set; }
        string Address { get; set; }
    }
}
