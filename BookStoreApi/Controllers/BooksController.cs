using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;

/// <summary>
/// Controlador dos livros
/// </summary>
[Produces("application/json")]
[Route("books")]
public class BooksController : ControllerBase
{
  private readonly ServiceBooks _sBooks;

  #region Constructor
  /// <summary>
  /// Contrutor do controle
  /// </summary>
  /// <param name="serviceBooks">Serviço de livros</param>
  public BooksController(ServiceBooks serviceBooks)
  {
    _sBooks = serviceBooks;
  }
  #endregion

  #region CRUD
  /// <summary>
  /// Incluir um novo livro
  /// </summary>
  /// <param name="newBook">Informações do novo livro</param>
  /// <response code="200">Retorna a view do novo livro</response>
  /// <response code="400">Mensagem de erro</response>
  [HttpPost]
  [Route("newbook")]
  [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult> NewBook([FromBody] Book newBook)
  {
    return Ok(await _sBooks.NewBook(newBook));
  }

  /// <summary>
  /// Buscar um livro pelo Id
  /// </summary>
  /// <param name="id">Identificador do livro</param>
  /// <response code="200">Retorna a view do livro</response>
  /// <response code="400">Mensagem de erro</response>
  [HttpPost]
  [Route("getbook/{id}")]
  [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult> GetBook(string id)
  {
    return Ok(await _sBooks.GetBook(id));
  }

  /// <summary>
  /// Buscar lista dos livros existentes
  /// </summary>
  /// <response code="200">Retorna uma lista de livros</response>
  /// <response code="400">Mensagem de erro</response>
  [HttpPost]
  [Route("listbooks")]
  [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult> ListBooks()
  {
    return Ok(await _sBooks.ListBooks());
  }

  /// <summary>
  /// Atualizar um livro
  /// </summary>
  /// <param name="updatedBook">Informações atualizadas do livro</param>
  /// <response code="200">Retorna a view atualizada do livro</response>
  /// <response code="400">Mensagem de erro</response>
  [HttpPut]
  [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult> UpdateBook([FromBody] Book updatedBook)
  {
    return Ok(await _sBooks.UpdateBook(updatedBook));
  }

  /// <summary>
  /// Deleta um livro
  /// </summary>
  /// <param name="id">Identificador do livro</param>
  /// <response code="200">Mensagem de sucesso</response>
  /// <response code="400">Mensagem de erro</response>
  [HttpDelete]
  [Route("{id}")]
  [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult> DeleteBook(string id)
  {
    return Ok(await _sBooks.DeleteBook(id));
  }
  #endregion
}

