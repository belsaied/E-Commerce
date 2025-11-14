namespace Domain.Exceptions
{
    public sealed class UserNotFoundException:NotFoundException
    {
        public UserNotFoundException(string email):base($"the User with the email : {email} Not found")
        {
            
        }
    }
}
