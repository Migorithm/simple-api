#if DEBUG
using Xunit;

namespace api.Dtos.Comment;


public class CommentDtoTests
{
    [Fact]
    public void CommentResponse_ToCommentDto_ShouldMapCommentToCommentResponse()
    {
        // Arrange
        var comment = new Models.Comment
        {
            Id = 1,
            Title = "est Title",
            CreatedOn = DateTime.Now,
            AppUser = new Models.AppUser { UserName = "Test User" },
            StockId = 1,
            Content = "Test Content"
        };

        // Act
        var result = Mapper.ToCommentDto(comment);

        // Assert
        Assert.Equal(comment.Id, result.Id);
        Assert.Equal(comment.Title, result.Title);
        Assert.Equal(comment.CreatedOn, result.CreatedOn);
        Assert.Equal(comment.AppUser.UserName, result.CreatedBy);
        Assert.Equal(comment.StockId, result.StockId);
        Assert.Equal(comment.Content, result.Content);
    }
    
}
#endif