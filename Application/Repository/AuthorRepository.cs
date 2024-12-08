using Application.Models;

namespace Application.Repository;

/// <summary>
/// 作者資訊儲存庫
/// </summary>
/// <param name="bookRepository">書刊儲存庫</param>
public class AuthorRepository(BookRepository bookRepository)
{
    private readonly List<Author> _authors =
    [
        new Author { Id = 1, Name = "Kuro Hsu" },
        new Author { Id = 2, Name = "Amos Lee" },
        new Author { Id = 3, Name = "Poy Chang" }
    ];

    /// <summary>
    /// 取得全部作者
    /// </summary>
    /// <returns>所有作者資訊</returns>
    public List<Author> GetAll()
    {
        return _authors;
    }

    /// <summary>
    /// 根據 ID 查詢作者
    /// </summary>
    /// <param name="authorId">作者 ID</param>
    /// <returns>作者詳細資訊</returns>
    public Author? GetById(int authorId)
    {
        return _authors.FirstOrDefault(a => a.Id == authorId);
    }

    /// <summary>
    /// 根據作者 ID 取得作者的書籍
    /// </summary>
    /// <param name="authorId">作者ID</param>
    /// <returns>作者擁有的書目</returns>
    public List<Book> GetBooksByAuthor(int authorId)
    {
        return bookRepository.GetAll().Where(b => b.AuthorId == authorId).ToList();
    }

    
    /// <summary>
    /// 新增作者
    /// </summary>
    /// <param name="author">作者資訊</param>
    /// <returns>新增成功與否</returns>
    public bool Create(Author author)
    {
        var existingAuthor = _authors.FirstOrDefault(a => a.Id == author.Id);

        if (existingAuthor != null)
            return false;

        _authors.Add(author);
        return true;
    }


    /// <summary>
    /// 更新作者資訊
    /// </summary>
    /// <param name="author">作者資訊</param>
    /// <returns>更新成功與否</returns>
    public bool Update(Author author)
    {
        var existingAuthor = _authors.FirstOrDefault(a => a.Id == author.Id);

        if (existingAuthor == null)
            return false;
        
        existingAuthor.Name = author.Name;
        return true;
    }

    /// <summary>
    /// 刪除作者
    /// </summary>
    /// <param name="authorId">作者 ID</param>
    /// <returns>刪除成功與否</returns>
    public bool Delete(int authorId)
    {
        var author = _authors.FirstOrDefault(a => a.Id == authorId);
        return author != null && _authors.Remove(author);
    }
}
