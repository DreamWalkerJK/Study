﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using APIRESTStudy.Data;
using APIRESTStudy.DTOs;
using APIRESTStudy.Models;

namespace APIRESTStudy.Controllers
{
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        private APIRESTStudyContext db = new APIRESTStudyContext();

        private static readonly Expression<Func<Book, BookDto>> AsBookDto =
            x => new BookDto
            {
                Title = x.Title,
                Author = x.Author.Name,
                Genre = x.Genre
            };

        // GET: api/Books
        [Route("")]
        public IQueryable<BookDto> GetBooks()
        {
            return db.Books.Include(b => b.Author).Select(AsBookDto);
        }

        // GET: api/Books/5
        [Route("id:int")]
        [ResponseType(typeof(BookDto))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            //Book book = await db.Books.FindAsync(id);
            BookDto book = await db.Books.Include(b => b.Author)
                .Where(b => b.BookId == id)
                .Select(AsBookDto)
                .FirstOrDefaultAsync();
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [Route("{id:int}/details")]
        [ResponseType(typeof(BookDetailDto))]
        public async Task<IHttpActionResult> GetBookDetail(int id)
        {
            var book = await (from b in db.Books.Include(b => b.Author)
                              where b.BookId == id
                              select new BookDetailDto
                              {
                                  Title = b.Title,
                                  Genre = b.Genre,
                                  Price = b.Price,
                                  Description = b.Description,
                                  Author = b.Author.Name
                              }).FirstOrDefaultAsync();

            if(book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [Route("{genre}")]
        public IQueryable<BookDto> GetBooksByGenre(string genre)
        {
            return db.Books.Include(b => b.Author)
                .Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                .Select(AsBookDto);
        }

        [Route("~/api/authors/{authorId:int}/books")]
        public IQueryable<BookDto> GetBooksByAuthor(int authorId)
        {
            return db.Books.Include(b => b.Author)
                .Where(b => b.AuthorId == authorId)
                .Select(AsBookDto);
        }

        [Route("date/{publishDate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [Route("date/{*publishDate:datetime:regex(\\d{4}/\\d{2}/\\d{2})}")]
        public IQueryable<BookDto> GetBooksByPublishDate(DateTime publishDate)
        {
            return db.Books.Include(b => b.Author)
                .Where(b => DbFunctions.TruncateTime(b.PublishDate) == DbFunctions.TruncateTime(publishDate))
                .Select(AsBookDto);
        }

        #region
        //// PUT: api/Books/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutBook(int id, Book book)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != book.BookId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(book).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Books
        //[ResponseType(typeof(Book))]
        //public async Task<IHttpActionResult> PostBook(Book book)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Books.Add(book);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = book.BookId }, book);
        //}

        //// DELETE: api/Books/5
        //[ResponseType(typeof(Book))]
        //public async Task<IHttpActionResult> DeleteBook(int id)
        //{
        //    Book book = await db.Books.FindAsync(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Books.Remove(book);
        //    await db.SaveChangesAsync();

        //    return Ok(book);
        //}
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.BookId == id) > 0;
        }
    }
}