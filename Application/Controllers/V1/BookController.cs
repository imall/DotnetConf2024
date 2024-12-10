using Application.Models;
using Application.Repository;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.V1;

/// <summary>
/// 書刊操作
/// </summary>
[Obsolete]
[ApiController]
[ApiVersion(1.0, Deprecated = true)] // 標記為棄用
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json", "application/xml")] // 設定回應格式
[Consumes("application/json")] // 設定接收格式
public class BookController : ControllerBase
{
    private readonly BookRepository _bookRepository;

    public BookController(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    /// <summary>
    /// 取得全部書刊
    /// </summary>
    /// <returns>所有藏書</returns>
    /// <response code="200">成功取得所有書籍</response>
    [HttpGet(Name = "GetAllBooks")]
    [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Book>> GetAllBooks()
    {
        return Ok(_bookRepository.GetAll());
    }

    /// <summary>
    /// 透過書籍 ID 取得書籍資訊
    /// </summary>
    /// <param name="id">你想查閱的書籍 ID</param>
    /// <returns>書籍資訊</returns>
    /// <response code="200">成功取得書籍</response>
    [HttpGet("{id:int}", Name = "GetBookById")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public ActionResult<Book> GetBookById(int id)
    {
        var book = _bookRepository.GetById(id);
        if (book == null)
        {
            return NotFound($"書籍 ID {id} 不存在");
        }

        return Ok(book);
    }

    /// <summary>
    /// 建立書籍
    /// </summary>
    /// <param name="book">你想新增的書刊內容</param>
    /// <returns>建立成功與否</returns>
    /// <response code="200">成功建立書籍</response>
    /// <response code="400">儲存失敗，請聯繫系統管理員</response>
    [HttpPost(Name = "CreateBook")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public ActionResult<bool> CreateBook([FromBody] Book book)
    {
        var result = _bookRepository.Create(book);
        if (result == false)
        {
            return BadRequest("儲存失敗，請聯繫系統管理員");
        }

        return Ok(result);
    }

    /// <summary>
    /// 更新書籍資訊
    /// </summary>
    /// <param name="id">你想更新的書籍 ID</param>
    /// <param name="book">你想更新的書籍資料</param>
    /// <returns>更新成功與否</returns>
    /// <response code="200">成功更新書籍</response>
    /// <response code="400">ID 不一致</response>
    /// <response code="404">無法更新，書籍 ID 不存在</response>
    [HttpPut("{id:int}", Name = "UpdateBook")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
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

    /// <summary>
    /// 刪除書籍
    /// </summary>
    /// <param name="id">你想刪除的書籍 ID</param>
    /// <returns>刪除成功與否</returns>
    /// <response code="200">成功刪除書籍</response>
    /// <response code="404">無法刪除，書籍 ID 不存在</response>
    [HttpDelete("{id:int}", Name = "DeleteBook")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
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
