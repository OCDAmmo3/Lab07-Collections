namespace BookLibrary
{
    public class Author
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName => $"{FirstName} {LastName}";

        public Author(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
