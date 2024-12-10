using System;
using System.Collections.Generic;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; } // "User" or "Librarian"
    public List<Book> BorrowedBooks { get; set; }
    private const double PenaltyRatePerDay = 1.0;

    public User(int id, string name, string role = "User")
    {
        Id = id;
        Name = name;
        Role = role;
        BorrowedBooks = new List<Book>();
    }

    public void BorrowBook(Book book)
    {
        if (book.IsAvailable)
        {
            book.IsAvailable = false;
            book.BorrowedDate = DateTime.Now;
            book.DueDate = DateTime.Now.AddDays(14); // 2 weeks due date
            BorrowedBooks.Add(book);
            Console.WriteLine($"{Name} borrowed {book.Title}. Due by {book.DueDate?.ToShortDateString()}.");
        }
        else
        {
            Console.WriteLine($"{book.Title} is currently unavailable.");
        }
    }

    public void ReturnBook(Book book)
    {
        if (BorrowedBooks.Contains(book))
        {
            BorrowedBooks.Remove(book);
            if (DateTime.Now > book.DueDate)
            {
                int lateDays = (DateTime.Now - book.DueDate.Value).Days;
                double penalty = lateDays * PenaltyRatePerDay;
                Console.WriteLine($"{Name} returned {book.Title} late by {lateDays} days. Penalty: ${penalty:F2}.");
            }
            else
            {
                Console.WriteLine($"{Name} returned {book.Title} on time.");
            }
            book.IsAvailable = true;
            book.BorrowedDate = null;
            book.DueDate = null;
        }
        else
        {
            Console.WriteLine($"{Name} has not borrowed {book.Title}.");
        }
    }

    public void ListBorrowedBooks()
    {
        Console.WriteLine($"{Name} has borrowed the following books:");
        if (BorrowedBooks.Count == 0)
        {
            Console.WriteLine("No borrowed books.");
        }
        else
        {
            foreach (var book in BorrowedBooks)
            {
                Console.WriteLine($"- {book.Title} by {book.Author}, Due: {book.DueDate?.ToShortDateString()}");
            }
        }
    }
}
