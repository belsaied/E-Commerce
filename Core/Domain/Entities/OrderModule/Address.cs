namespace Domain.Entities.OrderModule
{
    // It will not cause an Error unless you use the both Address[user/Order] Entities in the same class later 
    public class Address
    {
        // must add a parameterless constructor as the EF must pass on it while migration and if it isn't be found it causes an error.
        public Address()
        {
            
        }
        public Address(string firstName, string lastName, string country, string city, string street)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            City = city;
            Street = street;
        }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
    }
}
