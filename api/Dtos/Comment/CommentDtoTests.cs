#if DEBUG
using FluentAssertions;
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
        
        result.Title.Should().NotBeNullOrEmpty();
        result.Id.Should().BePositive();
        result.CreatedOn.Should().BeAfter(DateTime.MinValue);
        result.CreatedBy.Should().NotBeNullOrEmpty();
        result.StockId.Should().BePositive();
        result.Content.Should().NotBeNullOrEmpty();
        
    }
    
}
#endif