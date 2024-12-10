using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class Library
{
    private List<Book> books; // List of books in the library
    private List<User> users; // List of registered users

    public Library()
    {
        books = LoadBooks(); // Load books from file
        users = new List<User>(); // Initialize empty user list
    }

    // Add a new book to the library (Librarians only)
    public void AddBook(Book book, User user)
    {
        if (user.Role == "Librarian")
        {
            books.Add(book);
            SaveBooks(); // Save to file after adding
            Console.WriteLine($"Book '{book.Title}' added successfully.");
        }
        else
        {
            Console.WriteLine("Only librarians can add books.");
        }
    }

    // Remove a book from the library (Librarians only)
    public void RemoveBook(string isbn, User user)
    {
        if (user.Role == "Librarian")
        {
            var book = books.Find(b => b.ISBN == isbn);
            if (book != null)
            {
                books.Remove(book);
                SaveBooks(); // Save to file after removing
                Console.WriteLine($"Book '{book.Title}' removed successfully.");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }
        else
        {
            Console.WriteLine("Only librarians can remove books.");
        }
    }

    // Search for books by title or author
    public List<Book> SearchBooks(string query)
    {
        var results = books.FindAll(b =>
            b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            b.Author.Contains(query, StringComparison.OrdinalIgnoreCase));

        if (results.Count > 0)
        {
            Console.WriteLine("Search Results:");
            foreach (var book in results)
            {
                Console.WriteLine(book.ToString());
            }
        }
        else
        {
            Console.WriteLine("No books found.");
        }

        return results; // Return the list of found books
    }

    // Register a new user
    public void RegisterUser(User user)
    {
        users.Add(user);
        Console.WriteLine($"User '{user.Name}' registered successfully.");
    }

    // Save books to a JSON file
    private void SaveBooks()
    {
        try
        {
            File.WriteAllText("books.json", JsonConvert.SerializeObject(books, Formatting.Indented));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving books: {ex.Message}");
        }
    }

    // Load books from a JSON file
    private List<Book> LoadBooks()
    {
        try
        {
            if (File.Exists("books.json"))
            {
                var deserializedBooks = JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText("books.json"));
                return deserializedBooks ?? new List<Book>(); // Provide a default value if null
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading books: {ex.Message}");
        }
        return new List<Book>(); // Return empty list if file doesn't exist or fails to load
    }
}
