namespace ballastLaneApi.Application.ViewModels;

public class UserViewModel
{
    public int? UserId { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
}