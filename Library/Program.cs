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
            /*FillLibrary();
            FillBookBag();
*/
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
        }

        public static void ViewBookBag()
        {
            Console.WriteLine("Here's the book bag books");
        }

        public static void AddBookToBookBag()
        {
            Console.WriteLine("Book added to book bag");
        }

        public static void ReturnBorrowedBook()
        {
            Console.WriteLine("Book removed from book bag");
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

        private static void WriteBooks()
        {
            int counter = 1;
            foreach (Book book in Library)
            {
                Console.WriteLine($"{counter}: {book.Title} - Written by {book.Author.FullName}. {book.NumberOfPages} pages {book.Genre}");
            }
        }

        public static void AddABook(string title, string firstName, string lastName, int pages, Genre genre)
        {
            Book book = new Book()
            {
                Title = title,
                Author = new Author(firstName, lastName),
                PageNumber = pages,
                Genre = genre
            };

            Library.Add(book);
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
    }
}
