using System;
using System.Collections.Generic;
using System.Linq;
using Manifesto.Models.DTOs;
using Manifesto.Models.Entities;
using Manifesto.Models.InputModels;
using AutoMapper;

namespace Manifesto.Repositories
{
    public class BookRepository
    {
        private readonly BookDbContext _dbContext = new BookDbContext(); 
        public int CreateBook(BookInputModel book)
        { 
            var nextId = _dbContext.Books.Count() + 1;
            var entity = Mapper.Map<Book>(book);
            entity.Id = nextId;
            _dbContext.Books.Add(entity);
            _dbContext.SaveChanges();
            return nextId;
        }

        public void DeleteBookById(int id)
        {
            var entity = _dbContext.Books.FirstOrDefault(b => b.Id == id);
            _dbContext.Books.Remove(entity);
        }

        public BookDTO GetBookById(int id)
        {
            return Mapper.Map<BookDTO>(_dbContext.Books.FirstOrDefault(b => b.Id == id));
        }

        public IEnumerable<BookDTO> GetAllBooks(string category)
        {
            return Mapper.Map<IEnumerable<BookDTO>>(_dbContext.Books.Where(b => b.Category == category));
        }

        public void UpdateBookById(BookInputModel book, int id)
        {
            var updateBook = _dbContext.Books.FirstOrDefault(r => r.Id == id);

            if (updateBook == null) { return; /* Throw some exception */ }

            // Update properties
             updateBook.Name = book.Name;
             updateBook.Author = book.Author;
             updateBook.Description = book.Description;
             updateBook.ImageUrl = book.ImageUrl;
             updateBook.Isbn = book.Isbn;
             updateBook.Category = book.Category;
             updateBook.Pages = book.Pages;
             updateBook.ModifiedOn= DateTime.Now;
        }
    }
}