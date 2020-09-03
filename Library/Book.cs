namespace BookLibrary
{
    public class Book
    {
        public string Title { get; set; }

        public Author Author { get; set; }

        public enum Genre
        {
            horror = 5,
            fantasy = 10,
            romance = 15,
            adventure = 20,
            drama = 25
        }
    }
}
