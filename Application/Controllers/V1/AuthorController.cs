using Application.Models;
using Application.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.V1;

/// <summary>
/// 作者操作
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthorController(AuthorRepository authorRepository) : ControllerBase
{
    /// <summary>
    /// 取得全部作者
    /// </summary>
    /// <returns>所有作者資訊</returns>
    [HttpGet(Name = "GetAllAuthors")]
    public ActionResult<IEnumerable<Author>> GetAllAuthors()
    {
        return Ok(authorRepository.GetAll());
    }
    
    /// <summary>
    /// 根據 ID 查詢作者
    /// </summary>
    /// <param name="id">作者 ID</param>
    /// <returns>作者詳細資訊</returns>
    /// <remarks>
    /// 此方法會根據提供的作者 ID 查詢對應的作者詳細資訊。
    /// </remarks>
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
    
    /// <summary>
    /// 根據作者 ID 取得作者的書籍
    /// </summary>
    /// <param name="id">作者ID</param>
    /// <returns>作者擁有的書目</returns>
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
    
    /// <summary>
    /// 新增作者
    /// </summary>
    /// <param name="author">作者資訊</param>
    /// <returns>新增成功與否</returns>
    /// <remarks>
    /// # 新增作者功能
    /// ## 功能說明
    /// 此方法用於接收並新增作者資訊至系統中，適用於需要擴充作者資料的應用情境。
    /// ### 注意事項
    /// - 請確保提供的作者資訊完整且格式正確。
    /// - 系統會驗證資料是否重複，避免新增重複的作者紀錄。
    /// 
    /// ---
    /// 
    /// **範例資料：**
    /// 
    /// *請參考以下 JSON 結構：*  
    /// **[文件連結](https://developer.mozilla.org/zh-TW/docs/Learn/JavaScript/Objects/JSON)**
    /// ```json
    /// {
    ///    "id": 4,
    ///    "name": "jeff"
    /// }
    /// ```
    /// 
    /// **說明：**
    /// - `id`：作者的唯一識別碼。
    /// - `name`：作者全名。
    /// 
    /// **若新增成功，系統將回傳 `true`；失敗則回傳 `false`。**
    /// </remarks>
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
    
    /// <summary>
    /// 更新作者資訊
    /// </summary>
    /// <param name="id">欲更新的作者 ID</param>
    /// <param name="author">作者資訊</param>
    /// <returns>更新成功與否</returns>
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
    
    /// <summary>
    /// 刪除作者
    /// </summary>
    /// <param name="id">作者 ID</param>
    /// <returns>刪除成功與否</returns>
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
