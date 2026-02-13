using Atlas.Domain.Entities;
using Atlas.Domain.Enums;

namespace Atlas.UnitTests.Entities;

public class ArticleUnitTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreateDraft_AndGenerateSlug()
    {
        // Arrange
        var title = "Hello World!";
        var content = "Some content";

        // Act
        var article = new Article(title, content);

        // Assert
        Assert.Equal(title, article.Title);
        Assert.Equal(content, article.Content);
        Assert.Equal(ArticleStatus.Draft, article.Status);
        Assert.Null(article.PublishedAt);

        Assert.Equal("hello-world", article.Slug);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidTitle_ShouldThrow(string? title)
    {
        var ex = Assert.Throws<ArgumentException>(() => new Article(title!, "content"));

        Assert.Equal("title", ex.ParamName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidContent_ShouldThrow(string? content)
    {
        var ex = Assert.Throws<ArgumentException>(() => new Article("title", content!));

        Assert.Equal("content", ex.ParamName);
    }

    [Fact]
    public void ChangeTitle_ShouldUpdateTitle_AndSlug()
    {
        var article = new Article("Old Title", "content");

        article.ChangeTitle("C# Is Awesome!!");

        Assert.Equal("C# Is Awesome!!", article.Title);
        Assert.Equal("c-is-awesome", article.Slug);
    }

    [Fact]
    public void ChangeContent_ShouldUpdateContent()
    {
        var article = new Article("Title", "old");

        article.ChangeContent("new content");

        Assert.Equal("new content", article.Content);
    }

    [Fact]
    public void Publish_ShouldSetStatus_AndPublishedAt()
    {
        var article = new Article("Title", "content");
        var before = DateTime.UtcNow;

        article.Publish();

        Assert.Equal(ArticleStatus.Published, article.Status);
        Assert.NotNull(article.PublishedAt);

        Assert.True(article.PublishedAt >= before);
        Assert.True(article.PublishedAt <= DateTime.UtcNow.AddSeconds(2));
    }

    [Fact]
    public void Publish_WhenAlreadyPublished_ShouldDoNothing()
    {
        var article = new Article("Title", "content");

        article.Publish();
        var firstDate = article.PublishedAt;

        article.Publish();

        Assert.Equal(firstDate, article.PublishedAt);
    }

    [Fact]
    public void RevertToDraft_ShouldClearPublishedDate()
    {
        var article = new Article("Title", "content");
        article.Publish();

        article.RevertToDraft();

        Assert.Equal(ArticleStatus.Draft, article.Status);
        Assert.Null(article.PublishedAt);
    }

    [Fact]
    public void Archive_ShouldSetArchived_AndClearPublishedDate()
    {
        var article = new Article("Title", "content");
        article.Publish();

        article.Archive();

        Assert.Equal(ArticleStatus.Archived, article.Status);
        Assert.Null(article.PublishedAt);
    }
}
