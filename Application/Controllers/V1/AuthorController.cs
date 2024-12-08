using Application.Models;
using Application.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.V1;

[ApiController]
[Route("api/[controller]")]
public class AuthorController(AuthorRepository authorRepository) : ControllerBase
{
    [HttpGet(Name = "GetAllAuthors")]
    public ActionResult<IEnumerable<Author>> GetAllAuthors()
    {
        return Ok(authorRepository.GetAll());
    }
    
    [HttpGet("{id:int}", Name = "GetAuthorById")]
    public ActionResult<Author> GetAuthorById(int id)
    {
        var author = authorRepository.GetById(id);
        if (author == null)
        {
            return NotFound($"作者 ID {id} 不存在");
        }

        return Ok(author);
    }
    
    [HttpGet("{id:int}/books", Name = "GetBooksByAuthor")]
    public ActionResult<IEnumerable<Book>> GetBooksByAuthor(int id)
    {
        var author = authorRepository.GetById(id);
        if (author == null)
        {
            return NotFound($"作者 ID {id} 不存在");
        }

        return Ok(authorRepository.GetBooksByAuthor(id));
    }
    
    [HttpPost(Name = "CreateAuthor")]
    public ActionResult<bool> CreateAuthor([FromBody] Author author)
    {
        var result = authorRepository.Create(author);
        if (result == false)
        {
            return BadRequest("儲存失敗，請聯繫系統管理員");
        }

        return Ok(result);
    }
    
    [HttpPut("{id:int}", Name = "UpdateAuthor")]
    public IActionResult UpdateAuthor(int id, [FromBody] Author author)
    {
        if (id != author.Id)
        {
            return BadRequest("ID 不一致");
        }

        var result = authorRepository.Update(author);
        if (result == false)
        {
            return NotFound($"作者 ID {id} 不存在");
        }


        return Ok(result);
    }
    
    [HttpDelete("{id:int}", Name = "DeleteAuthor")]
    public IActionResult DeleteAuthor(int id)
    {
        var result = authorRepository.Delete(id);
        if (result == false)
        {
            return NotFound($"無法刪除，作者 ID {id} 不存在");
        }

        return Ok(result);
    }
}
