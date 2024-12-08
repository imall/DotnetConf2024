using System.ComponentModel.DataAnnotations;

namespace Application.Models;

/// <summary>
/// 作者資訊
/// </summary>
public class Author
{
    /// <summary>
    /// **作者** ID 資訊
    /// </summary>
    /// <remarks>雖然「作者」有標記粗體，但型別資訊在 Swagger 預設已經是粗體，所以不會有特殊效果</remarks>
    /// <example>4</example>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// 作者名稱
    /// </summary>
    /// <example>Jeff</example>
    [Required]
    public string Name { get; set; }
}
