﻿namespace ApiBook.Models.DTOs
{
    public class BooksDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
