namespace APIEFSPAserviceStudy.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using APIEFSPAserviceStudy.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<APIEFSPAserviceStudy.Data.APIEFSPAserviceStudyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(APIEFSPAserviceStudy.Data.APIEFSPAserviceStudyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Authors.AddOrUpdate(x => x.AuthorId,
                    new Author() { AuthorId = 1, Name = "Jane Austen" },
                    new Author() { AuthorId = 2, Name = "Charles Dickens" },
                    new Author() { AuthorId = 3, Name = "Miguel de Cervantes" }
                );

            context.Books.AddOrUpdate(x => x.BookId,
                    new Book()
                    {
                        BookId = 1,
                        Title = "Pride and Prejudice",
                        Year = 1813,
                        AuthorId = 1,
                        Price = 9.99M,
                        Genre = "Comedy of manners"
                    },
                    new Book()
                    {
                        BookId = 2,
                        Title = "Northanger Abbey",
                        Year = 1817,
                        AuthorId = 1,
                        Price = 12.95M,
                        Genre = "Gothic parody"
                    },
                    new Book()
                    {
                        BookId = 3,
                        Title = "David Copperfield",
                        Year = 1850,
                        AuthorId = 2,
                        Price = 15,
                        Genre = "Bildungsroman"
                    },
                    new Book()
                    {
                        BookId = 4,
                        Title = "Don Quixote",
                        Year = 1617,
                        AuthorId = 3,
                        Price = 8.95M,
                        Genre = "Picaresque"
                    }
                );
        }
    }
}
