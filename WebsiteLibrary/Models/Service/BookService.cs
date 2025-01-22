using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebsiteLibrary.Models.Entites;
using WebsiteLibrary.Models.Interface;

namespace WebsiteLibrary.Models.Service
{
    public class BookService : IBookService
    {
        private readonly LibraryDatabaseContext _context;

        public BookService(LibraryDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(int bookId, Book updatedBook)
        {
            var existingBook = await _context.Books.FindAsync(bookId);
            if (existingBook == null)
                throw new KeyNotFoundException("Book not found");

            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.Publisher = updatedBook.Publisher;
            existingBook.PublishedYear = updatedBook.PublishedYear;
            existingBook.Isbn = updatedBook.Isbn;
            existingBook.Genre = updatedBook.Genre;
            existingBook.Quantity = updatedBook.Quantity;
            existingBook.Price = updatedBook.Price;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
                throw new KeyNotFoundException("Book not found");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            var book = await _context.Books
                .Include(b => b.Categories)
                .FirstOrDefaultAsync(b => b.BookId == bookId);
            if (book == null)
                throw new KeyNotFoundException("Book not found");

            return book;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Categories)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string keyword)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(keyword) || b.Author.Contains(keyword))
                .ToListAsync();
        }

        public async Task UpdateQuantityAsync(int bookId, int quantity)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
                throw new KeyNotFoundException("Book not found");

            book.Quantity = quantity;
            await _context.SaveChangesAsync();
        }
    }
}
