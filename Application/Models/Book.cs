using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models;

/// <summary>
/// 書刊資訊
/// </summary>
public class Book
{
    /// <summary>
    /// 書籍 ID
    /// </summary>
    /// <example>6</example>
    [Required]
    public int Id { get; set; }
    
    /// <summary>
    /// 書名
    /// </summary>
    /// <example>金魚都能懂的CSS必學屬性：網頁設計必備寶典</example>
    [Required]
    public string Title { get; set; }
    
    /// <summary>
    /// 作者ID
    /// </summary>
    /// <example>2</example>
    [JsonIgnore]
    public int AuthorId { get; set; }
    
    /// <summary>
    /// 作者名稱
    /// </summary>
    /// <example>Amos Lee</example>
    [Required]
    public string Author { get; set; }
    
    /// <summary>
    /// 書籍描述
    /// </summary>
    /// <example>本書內容針對網頁切版最常見的CSS屬性來詳細介紹，不管是剛接觸網頁的新手，或者是已接觸過一段時間的開發老手，對於該學習哪些什麼CSS屬性總是會有些混亂，因此本書針對「網頁切版」所需要的CSS屬性做完整詳細的說明，由淺入深，讓你可以理解哪些CSS屬性是一定要學習的。</example>
    [MinLength(10)]
    [MaxLength(150)]
    public string Description { get; set; }
}
