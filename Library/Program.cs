using System;
using System.Collections.Generic;

namespace BookLibrary
{
    class Program
    {
        public static Library<Book> Library { get; set; }
        public static List<Book> BookBag { get; set; }

        static void Main(string[] args)
        {
            Console.Clear();
            UserInterface();
        }

        static void UserInterface()
        {
            Library = new Library<Book>();
            BookBag = new List<Book>();
            FillLibrary();
            FillBookBag();

            string cont = "1";
            while (cont == "1")
            {
                string value = Start();
                switch (value)
                {
                    case "1":
                        ViewLibraryBooks();
                        break;
                    case "2":
                        AddBookToLibrary();
                        break;
                    case "3":
                        RemoveBookFromLibrary();
                        break;
                    case "4":
                        ViewBookBag();
                        break;
                    case "5":
                        AddBookToBookBag();
                        break;
                    case "6":
                        ReturnBorrowedBook();
                        break;
                    default:
                        Console.WriteLine("Incorrect command. Please try again.");
                        Console.ReadLine();
                        break;
                }
                Console.WriteLine("Enter 1 to return to the menu, or just hit enter to quit.");
                cont = Console.ReadLine();
            }
        }

        public static void ViewLibraryBooks()
        {
            Console.Clear();
            Console.WriteLine("Library includes: ");
            Console.WriteLine();
            WriteBooks();
            Console.WriteLine();
        }

        public static void AddBookToLibrary()
        {
            Console.WriteLine("Enter a title:");
            string titleInput = Console.ReadLine();

            Console.WriteLine("Enter an author's first name:");
            string firstInput = Console.ReadLine();

            Console.WriteLine("Enter the author's last name:");
            string lastInput = Console.ReadLine();

            int pages = 0;
            while (pages <= 0)
                pages = GetPages();

            int genreNum = -1;
            while (genreNum < 0)
            {
                genreNum = GetGenre();
            }

            AddABook(titleInput, firstInput, lastInput, pages, (Genre)genreNum);

            Console.Clear();

            Console.WriteLine("Book added! Library now includes:");
            WriteBooks();
        }

        public static void RemoveBookFromLibrary()
        {
            Console.Clear();

            Console.WriteLine("Library includes:");
            WriteBooks();

            int bookToRemove = -1;
            while (bookToRemove < 0)
                bookToRemove = RemoveWhichBook();

            Console.Clear();

            Console.WriteLine("Book removed. Library now includes:");
            WriteBooks();
        }

        public static void ViewBookBag()
        {
            int counter = 1;
            foreach (Book book in BookBag)
                if (book != null)
                    Console.WriteLine($"{counter++}: {book.Title} - Written by {book.Author.FullName}. {book.PageNumber} pages {book.Genre}");
        }

        public static void AddBookToBookBag()
        {
            Dictionary<int, Book> books = new Dictionary<int, Book>();

            int counter = 1;
            foreach (Book book in Library)
            {
                books.Add(counter, book);
                Console.WriteLine($"{counter++}: {book.Title} - Written by {book.Author.FullName}");
            }

            int input = -1;
            while (input < 0)
            {
                Console.WriteLine("Choose a book: (pick a number)");
                string borrowed = Console.ReadLine();
                input = IsItValid(borrowed);
            }

            books.TryGetValue(input + 1, out Book borrowedBook);
            Library.Remove(input);
            BookBag.Add(borrowedBook);

            Console.WriteLine("Added book to Book Bag! Your Book Bag now includes:");
            ViewBookBag();
        }

        public static void ReturnBorrowedBook()
        {
            Dictionary<int, Book> books = new Dictionary<int, Book>();

            int counter = 1;
            foreach (Book book in BookBag)
            {
                books.Add(counter, book);
                if (book != null)
                    Console.WriteLine($"{counter++}: {book.Title} - Written by {book.Author.FullName}");
            }

            int input = -1;
            while (input < 0)
            {
                Console.WriteLine("Choose a book: (pick a number)");
                string borrowed = Console.ReadLine();
                input = IsItValid(borrowed);
            }

            books.TryGetValue(input + 1, out Book returnedBook);
            BookBag.Remove(returnedBook);
            Library.Add(returnedBook);

            Console.WriteLine("Returned book from Book Bag! Your Book Bag now includes:");
            ViewBookBag();
        }

        static string Start()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the library. Please keep quiet, and select a number: ");
            Console.WriteLine("1 - View Library Collection");
            Console.WriteLine("2 - Add a Book to the Library");
            Console.WriteLine("3 - Remove a Book from the Library");
            Console.WriteLine("4 - View Checked Out Books");
            Console.WriteLine("5 - Check Out Another Book");
            Console.WriteLine("6 - Return a Checked Out Book \n");

            return Console.ReadLine();
        }

        private static int IsItValid(string borrowed)
        {
            if (!int.TryParse(borrowed, out int selection))
            {
                return -1;
            }
            return selection - 1;
        }

        public static int RemoveWhichBook()
        {
            Console.WriteLine("Which book would you like to remove? (Choose a number)");
            string removal = Console.ReadLine();

            if (int.TryParse(removal, out int removalNumber))
            {
                if (!Library.Remove(removalNumber - 1))
                {
                    Console.WriteLine("Incorrect number, try again.");
                    return -1;
                }
                return int.Parse(removal);
            }
            else
            {
                Console.WriteLine("Invalid input, try again.");
                return -1;
            }
        }

        private static void WriteBooks()
        {
            int counter = 1;
            foreach (Book book in Library)
            {
                if (book != null)
                    Console.WriteLine($"{counter++}: {book.Title} - Written by {book.Author.FullName}. {book.PageNumber} pages {book.Genre}");
            }
        }

        public static void AddABook(string title, string firstName, string lastName, int pages, Genre genre)
        {
            Book book = CreateBook(title, firstName, lastName, pages, genre);

            Library.Add(book);
        }

        public static void AddABookToBag(string title, string firstName, string lastName, int pages, Genre genre)
        {
            Book book = CreateBook(title, firstName, lastName, pages, genre);

            BookBag.Add(book);
        }

        private static Book CreateBook(string title, string firstName, string lastName, int pages, Genre genre)
        {
            return new Book()
            {
                Title = title,
                Author = new Author(firstName, lastName),
                PageNumber = pages,
                Genre = genre
            };
        }

        public static int GetPages()
        {
            Console.WriteLine("Enter amount of pages:");
            string pageNum = Console.ReadLine();
            if (int.TryParse(pageNum, out int numberOfPages))
            {
                if (numberOfPages < 0)
                    return 0;
                else
                    return numberOfPages;
            }
            return 0;
        }

        public static int GetGenre()
        {
            Console.WriteLine("Choose a genre by entering the corresponding number:");

            int counter = 1;
            foreach (Genre genre in Enum.GetValues(typeof(Genre)))
                Console.WriteLine($"{counter++}: {genre}");
            string genreInput = Console.ReadLine();

            int number = Convert.ToInt32(genreInput);

            if (number < 0 || number > counter - 1)
            {
                Console.WriteLine("That's not a proper genre.");
                return 0;
            }

            return number - 1;
        }

        public static void FillLibrary()
        {
            AddABook("Harry Potter and the Philosopher's Stone", "J.K.", "Rowling", 223, (Genre)1);
            AddABook("Harry Potter and the Chamber of Secrets", "J.K.", "Rowling", 251, (Genre)1);
            AddABook("Harry Potter and the Prisoner of Azkaban", "J.K.", "Rowling", 317, (Genre)1);
            AddABook("Harry Potter and the Goblet of Fire", "J.K.", "Rowling", 636, (Genre)1);
            AddABook("Harry Potter and the Order of the Phoenix", "J.K.", "Rowling", 766, (Genre)1);
            AddABook("Harry Potter and the Half Blood Prince", "J.K.", "Rowling", 607, (Genre)1);
            AddABook("Harry Potter and the Deathly Hallows", "J.K.", "Rowling", 607, (Genre)1);
        }

        public static void FillBookBag()
        {
            AddABookToBag("Lord of the Rings: The Fellowship of the Ring", "J.R.R.", "Tolkien", 423, (Genre)1);
            AddABookToBag("Lord of the Rings: The Two Towers", "J.R.R.", "Tolkien", 352, (Genre)1);
            AddABookToBag("Lord of the Rings: Return of the King", "J.R.R.", "Tolkien", 416, (Genre)1);
        }
    }
}
