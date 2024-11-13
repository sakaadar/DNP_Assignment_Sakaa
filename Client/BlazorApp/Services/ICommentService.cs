using DTO;

namespace BlazorApp.Services;

public interface ICommentService
{
    public Task<CreateUpdateCommentDto> createCommentasync(CreateUpdateCommentDto comment);
}