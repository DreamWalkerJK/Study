var ViewModel = function () {
    var self = this;
    self.books = ko.observableArray();
    self.authors = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();

    self.newBook = {
        Author: ko.observable(),
        Genre: ko.observable(),
        Price: ko.observable(),
        Title: ko.observable(),
        Year: ko.observable()
    }

    var booksUri = '/api/books/';
    var authorsUri = '/api/authors/';

    self.getBookDetail = function (item) {
        ajaxHelper(booksUri + item.BookId, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllBooks() {
        ajaxHelper(booksUri, 'GET').done(function (data) {
            self.books(data);
        });
    }

    function getAuthors() {
        ajaxHelper(authorsUri, 'GET').done(function (data) {
            self.authors(data);
        });
    }

    self.addBook = function (formElement) {
        var book = {
            AuthorId: self.newBook.Author().AuthorId,
            Genre: self.newBook.Genre(),
            Price: self.newBook.Price(),
            Title: self.newBook.Title(),
            Year: self.newBook.Year()
        };

        ajaxHelper(booksUri, 'POST', book).done(function (item) {
            self.books.push(item);
        });
    }

    // Fetch the initial data.
    getAllBooks();
    getAuthors();
};

ko.applyBindings(new ViewModel());