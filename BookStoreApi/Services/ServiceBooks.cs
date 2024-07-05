using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class ServiceBooks
{
  private readonly IMongoCollection<Book> _booksCollection;

  #region Constructor
  public ServiceBooks(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
  {
    var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
    var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
    _booksCollection = mongoDatabase.GetCollection<Book>(bookStoreDatabaseSettings.Value.BooksCollectionName);
  }
  #endregion

  #region CRUD
  public async Task<Book> NewBook(Book newBook)
  {
    if (string.IsNullOrEmpty(newBook.BookName))
    {
      throw new Exception("Nome do livro inválido.");
    }
    await _booksCollection.InsertOneAsync(newBook);
    return newBook;
  }
  public async Task<Book?> GetBook(string id)
  {
    Book book = await _booksCollection.Find(b => b.Id == id).FirstOrDefaultAsync()
      ?? throw new Exception("Livro não encontrado.");
    return book;
  }
  public async Task<List<Book>> ListBooks()
  {
    List<Book> books = await _booksCollection.Find(_ => true).ToListAsync()
      ?? throw new Exception("Nenhum livro foi encontrado.");
    return books;
  }
  public async Task<Book> UpdateBook(Book updatedBook)
  {
    Book book = await _booksCollection.Find(b => b.Id == updatedBook.Id).FirstOrDefaultAsync()
      ?? throw new Exception("Livro não encontrado.");
    await _booksCollection.ReplaceOneAsync(x => x.Id == book.Id, updatedBook);
    return updatedBook;
  }
  public async Task<string> DeleteBook(string id)
  {
    Book book = await _booksCollection.Find(b => b.Id == id).FirstOrDefaultAsync()
      ?? throw new Exception("Livro não encontrado.");
    await _booksCollection.DeleteOneAsync(x => x.Id == book.Id);
    return "Livro deletado com sucesso.";
  }
  #endregion
}
