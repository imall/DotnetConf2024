using Application.Models;

namespace Application.Repository;

/// <summary>
/// 書刊儲存庫
/// </summary>
public class BookRepository
{
    private readonly List<Book> _books =
    [
        new Book { Id = 1, Title = "0 陷阱！0 誤解！8 天重新認識 JavaScript！", AuthorId = 1, Author = "Kuro Hsu", Description = "本書就是希望能在這個主題當中，與各位讀者一起重新認識JavaScript：這個號稱「世界上最被人誤解的程式語言」。" },
        new Book { Id = 2, Title = "重新認識 Vue.js：008天絕對看不完的 Vue.js 3 指南", AuthorId = 1, Author = "Kuro Hsu", Description = "從零開始！快速上手！網羅完整Vue.js功能的實戰指南" },
        new Book { Id = 3, Title = "金魚都能懂的 CSS 選取器：金魚都能懂了你還怕學不會嗎", AuthorId = 2, Author = "Amos Lee", Description = "在網頁的世界中，有太多對新手不友善的因素，不管是教學文章的敘述方式、專有名詞的應用、學習章節的安排、或舉例與引導過程等，Amos為了解決這問題，便開始了「金魚都能懂」的系列教學，希望能用最淺顯易懂的教學與講解方式，帶領眾新手們，在新手村中學好學滿，自信滿滿地踏出新手村。" },
        new Book { Id = 4, Title = "金魚都能懂的CSS必學屬性：網頁設計必備寶典", AuthorId = 2, Author = "Amos Lee", Description = "本書內容針對網頁切版最常見的CSS屬性來詳細介紹，不管是剛接觸網頁的新手，或者是已接觸過一段時間的開發老手，對於該學習哪些什麼CSS屬性總是會有些混亂，因此本書針對「網頁切版」所需要的CSS屬性做完整詳細的說明，由淺入深，讓你可以理解哪些CSS屬性是一定要學習的。" },
        new Book { Id = 5, Title = ".NET Conf 總召真心話：社群與活動企劃幕後秘辛", AuthorId = 3, Author = "Poy Chang", Description = "這本書分為三部分，首先介紹了社群的特色和文化，講述了在技術社群中成為領導者的經驗。其次，探討了舉辦活動的重要性和背後需要考慮的種種因素，包括場地、日期、內容、行銷等。最後，分享了舉辦活動的一些幕後故事，透過這些小劇場呈現出社群中人與人之間的連結和互動。這本書描繪出一幅社群中人與人之間互動的生動畫面，吸引每位想一窺社群活動核心的人閱讀。" },
    ];

    /// <summary>
    /// 取得全部書刊
    /// </summary>
    /// <returns>所有藏書</returns>
    public List<Book> GetAll()
    {
        return _books;
    }

    /// <summary>
    /// 透過書籍 ID 取得書籍資訊
    /// </summary>
    /// <param name="id">你想查閱的書籍 ID</param>
    /// <returns>書籍資訊</returns>
    public Book? GetById(int id)
    {
        return _books.FirstOrDefault(b => b.Id == id);
    }


    /// <summary>
    /// 建立書籍
    /// </summary>
    /// <param name="book">你想新增的書刊內容</param>
    /// <returns>建立成功與否</returns>
    public bool Create(Book book)
    {
        var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
        if (existingBook != null) return false;

        _books.Add(book);
        return true;
    }


    /// <summary>
    /// 更新書籍資訊
    /// </summary>
    /// <param name="book">你想更新的書籍資料</param>
    /// <returns>更新成功與否</returns>
    public bool Update(Book book)
    {
        var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
        if (existingBook == null) return false;

        existingBook.Title = book.Title;
        existingBook.AuthorId = book.AuthorId;
        return true;
    }

    /// <summary>
    /// 刪除書籍
    /// </summary>
    /// <param name="id">你想刪除的書籍 ID</param>
    /// <returns>刪除成功與否</returns>
    public bool Delete(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        return book != null && _books.Remove(book);
    }
}
