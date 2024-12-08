using Application.Models;
using Application.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.V1;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly BookRepository _bookRepository;

    public BookController(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    [HttpGet(Name = "GetAllBooks")]
    public ActionResult<IEnumerable<Book>> GetAllBooks()
    {
        return Ok(_bookRepository.GetAll());
    }
    
    [HttpGet("{id:int}", Name = "GetBookById")]
    public ActionResult<Book> GetBookById(int id)
    {
        var book = _bookRepository.GetById(id);
        if (book == null)
        {
            return NotFound($"書籍 ID {id} 不存在");
        }

        return Ok(book);
    }
    
    [HttpPost(Name = "CreateBook")]
    public ActionResult<bool> CreateBook([FromBody] Book book)
    {
        var result = _bookRepository.Create(book);
        if (result == false)
        {
            return BadRequest("儲存失敗，請聯繫系統管理員");
        }

        return Ok(result);
    }
    
    [HttpPut("{id:int}", Name = "UpdateBook")]
    public ActionResult<bool> UpdateBook(int id, [FromBody] Book book)
    {
        if (id != book.Id)
        {
            return BadRequest("ID 不一致");
        }

        var result = _bookRepository.Update(book);
        if (result == false)
        {
            return NotFound($"無法更新，書籍 ID {id} 不存在");
        }

        return Ok(result);
    }
    
    [HttpDelete("{id:int}", Name = "DeleteBook")]
    public ActionResult<bool> DeleteBook(int id)
    {
        var book = _bookRepository.GetById(id);
        if (book == null)
        {
            return NotFound($"無法刪除，書籍 ID {id} 不存在");
        }

        var result = _bookRepository.Delete(id);
        if (result == false)
        {
            return BadRequest("刪除失敗，請聯繫系統管理員");
        }

        return Ok(result);
    }
}
