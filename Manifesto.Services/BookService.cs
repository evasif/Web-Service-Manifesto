using System;
using System.Collections.Generic;
using System.Linq;
using Manifesto.Models.DTOs;
using Manifesto.Models.Entities;
using Manifesto.Models.InputModels;
using AutoMapper;
using Manifesto.Repositories;

namespace Manifesto.Services
{
    public class BookService
    {
        private readonly BookRepository _bookRepository = new BookRepository();
        public int CreateBook(BookInputModel book)
        { 
            return _bookRepository.CreateBook(book);
        }

        public void DeleteBookById(int id)
        {
            _bookRepository.DeleteBookById(id);
        }

        public BookDTO GetBookById(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null) { throw new Exception(message: $"Book with id {id} was not found."); }
            return book;
        }

        public IEnumerable<BookDTO> GetAllBooks(string category)
        {
            return _bookRepository.GetAllBooks(category);
        }

        public void UpdateBookById(BookInputModel book, int id)
        {
           var entity = _bookRepository.GetBookById(id);
            if (entity == null) { throw new Exception($"Book with id {id} was not found."); }
            _bookRepository.UpdateBookById(book, id);
        }
    }
}