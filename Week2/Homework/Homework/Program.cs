using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    // ABSTRACT CLASS: LibraryItem
    public abstract class LibraryItem
    {
        public int Id { get; }
        public string Title { get; set; }
        public int PublicationYear { get; }

        protected LibraryItem(int id, string title, int publicationYear)
        {
            Id = id;
            Title = title;
            PublicationYear = publicationYear;
        }

        public abstract void DisplayInfo();

        public virtual decimal CalculateLateReturnFee(int daysLate)
        {
            return daysLate * 0.50m;
        }
    }

    // INTERFACE: IBorrowable
    public interface IBorrowable
    {
        DateTime? BorrowDate { get; set; }
        DateTime? ReturnDate { get; set; }
        bool IsAvailable { get; }

        void Borrow();
        void Return();
    }

    // CLASS: Book
    public class Book : LibraryItem, IBorrowable
    {
        public string Author { get; }
        public int Pages { get; set; }
        public string Genre { get; set; }

        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsAvailable => ReturnDate != null || BorrowDate == null;

        public Book(int id, string title, int publicationYear, string author)
            : base(id, title, publicationYear)
        {
            Author = author;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[Book] ID: {Id}, Title: {Title}, Author: {Author}, Year: {PublicationYear}, Genre: {Genre}, Pages: {Pages}, Available: {IsAvailable}");
        }

        public void Borrow()
        {
            if (!IsAvailable)
            {
                Console.WriteLine($"'{Title}' is currently not available.");
                return;
            }
            BorrowDate = DateTime.Now;
            ReturnDate = null;
            Console.WriteLine($"You borrowed '{Title}'.");
        }

        public void Return()
        {
            if (IsAvailable)
            {
                Console.WriteLine($"'{Title}' is not currently borrowed.");
                return;
            }
            ReturnDate = DateTime.Now;
            Console.WriteLine($"You returned '{Title}'.");
        }

        public override decimal CalculateLateReturnFee(int daysLate)
        {
            return daysLate * 0.75m;
        }
    }

    // CLASS: DVD
    public class DVD : LibraryItem, IBorrowable
    {
        public string Director { get; }
        public int Runtime { get; set; }
        public string AgeRating { get; set; }

        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsAvailable => ReturnDate != null || BorrowDate == null;

        public DVD(int id, string title, int publicationYear, string director)
            : base(id, title, publicationYear)
        {
            Director = director;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[DVD] ID: {Id}, Title: {Title}, Director: {Director}, Year: {PublicationYear}, Runtime: {Runtime} mins, Age Rating: {AgeRating}, Available: {IsAvailable}");
        }

        public void Borrow()
        {
            if (!IsAvailable)
            {
                Console.WriteLine($"'{Title}' is currently not available.");
                return;
            }
            BorrowDate = DateTime.Now;
            ReturnDate = null;
            Console.WriteLine($"You borrowed '{Title}'.");
        }

        public void Return()
        {
            if (IsAvailable)
            {
                Console.WriteLine($"'{Title}' is not currently borrowed.");
                return;
            }
            ReturnDate = DateTime.Now;
            Console.WriteLine($"You returned '{Title}'.");
        }

        public override decimal CalculateLateReturnFee(int daysLate)
        {
            return daysLate * 1.00m;
        }
    }

    // CLASS: Magazine
    public class Magazine : LibraryItem
    {
        public int IssueNumber { get; }
        public string Publisher { get; set; }

        public Magazine(int id, string title, int publicationYear, int issueNumber)
            : base(id, title, publicationYear)
        {
            IssueNumber = issueNumber;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[Magazine] ID: {Id}, Title: {Title}, Issue #{IssueNumber}, Year: {PublicationYear}, Publisher: {Publisher}");
        }
    }

    // CLASS: Library
    public class Library
    {
        private List<LibraryItem> items = new List<LibraryItem>();

        public void AddItem(LibraryItem item)
        {
            items.Add(item);
        }

        public LibraryItem? SearchByTitle(string titlePart)
        {
            return items.FirstOrDefault(item => item.Title.IndexOf(titlePart, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public void DisplayAllItems()
        {
            Console.WriteLine("===== Library Items =====");
            foreach (var item in items)
            {
                item.DisplayInfo();
            }
        }
    }

    // PROGRAM
    class Program
    {
        static void Main()
        {
            // Create library
            var library = new Library();

            // Add items
            var book1 = new Book(1, "The Great Gatsby", 1925, "F. Scott Fitzgerald")
            {
                Genre = "Classic Fiction",
                Pages = 180
            };

            var book2 = new Book(2, "Clean Code", 2008, "Robert C. Martin")
            {
                Genre = "Programming",
                Pages = 464
            };

            var dvd1 = new DVD(3, "Inception", 2010, "Christopher Nolan")
            {
                Runtime = 148,
                AgeRating = "PG-13"
            };

            var magazine1 = new Magazine(4, "National Geographic", 2023, 56)
            {
                Publisher = "National Geographic Partners"
            };

            library.AddItem(book1);
            library.AddItem(book2);
            library.AddItem(dvd1);
            library.AddItem(magazine1);

            // Display all items
            library.DisplayAllItems();

            // Borrow and return demonstration
            Console.WriteLine("\n===== Borrowing Demonstration =====");
            book1.Borrow();
            dvd1.Borrow();

            // Try to borrow again
            book1.Borrow();

            // Display changed status
            Console.WriteLine("\n===== Updated Status =====");
            book1.DisplayInfo();
            dvd1.DisplayInfo();

            // Return item
            Console.WriteLine("\n===== Return Demonstration =====");
            book1.Return();

            // Search for an item
            Console.WriteLine("\n===== Search Demonstration =====");
            var foundItem = library.SearchByTitle("Clean");
            if (foundItem != null)
            {
                Console.WriteLine("Found item:");
                foundItem.DisplayInfo();
            }
            else
            {
                Console.WriteLine("Item not found");
            }
        }
    }
}

