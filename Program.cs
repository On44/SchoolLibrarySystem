using System;

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        // Register users
        User librarian = new User(1, "Alice", "Librarian");
        User student = new User(2, "Bob", "Student");
        library.RegisterUser(librarian);
        library.RegisterUser(student);

        // Add books (by librarian)
        Console.WriteLine("\nAdding Books...");
        library.AddBook(new Book("1984", "George Norwin", "12345"), librarian);
        library.AddBook(new Book("The Monk Who Sold His Ferrari", "Colwil Meakins", "67890"), librarian);
        library.AddBook(new Book("Who Will Cry When You Die", "Lempy Brigit", "11121"), librarian);

        // Attempt to add books by non-librarian
        Console.WriteLine("\nAttempting to add a book as a non-librarian:");
        library.AddBook(new Book("Moby Dick", "Herman Melville", "33321"), student);

        // Search for books by title or author
        Console.WriteLine("\nSearching for '1984':");
        var searchResults = library.SearchBooks("1984");

        Console.WriteLine("\nSearching for books by 'George Orwell':");
        searchResults = library.SearchBooks("George Orwell");

        // Borrow a book (student)
        if (searchResults.Count > 0)
        {
            Console.WriteLine("\nBorrowing '1984'...");
            student.BorrowBook(searchResults[0]);
        }

        // Simulate overdue return
        if (student.BorrowedBooks.Count > 0)
        {
            Console.WriteLine("\nSimulating overdue book return:");
            student.BorrowedBooks[0].DueDate = DateTime.Now.AddDays(-5); // Set due date to 5 days ago
            student.ReturnBook(student.BorrowedBooks[0]);
        }

        // Display library books
        Console.WriteLine("\nCurrent library collection:");
        library.SearchBooks(""); // Empty search to display all books

        // Remove a book (by librarian)
        Console.WriteLine("\nRemoving '1984' by Librarian:");
        library.RemoveBook("12345", librarian);

        // Display library books after removal
        Console.WriteLine("\nLibrary collection after removal:");
        library.SearchBooks("");
    }
}
