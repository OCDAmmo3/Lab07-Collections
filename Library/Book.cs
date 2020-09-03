namespace BookLibrary
{
    public class Book
    {
        public string Title { get; set; }

        public Author Author { get; set; }

        public int PageNumber { get; set; }

        public Genre Genre { get; set; }


    }

    public enum Genre
    {
        horror,
        fantasy,
        romance,
        adventure,
        drama,
        nonfiction
    }
}
