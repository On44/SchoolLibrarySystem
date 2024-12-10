using System;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime? BorrowedDate { get; set; }
    public DateTime? DueDate { get; set; }

    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        IsAvailable = true;
    }

    public override string ToString()
    {
        string availability = IsAvailable ? "Available" : $"Checked out (Due: {DueDate?.ToShortDateString()})";
        return $"{Title} by {Author} (ISBN: {ISBN}) - {availability}";
    }
}
