namespace SaveService.Auth.Api.Repository
{
    //Is created to make AuthenticateUserAsync method more common for login and register models:
    //AuthenticateUserAsync(ICommonType request, UserContext context)
    //login and register models are implementing it
    public interface IAssociation
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
