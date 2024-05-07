using Api.Commons.Extensions;
using Api.Commons.Models;
using Api.Entities;
using Api.Features.Questions.GetQuestions.Endpoint;
using Api.Infrastructure;
using Api.Infrastructure.Data;
using FastEndpoints;

namespace Api.Features.Questions.GetPagedQuestions.Command;

public record GetPagedQuestionsCommand(int PageSize = 10, int PageNumber = 1) : ICommand<PaginatedList<Response>>;

public class GetPagedQuestionsCommandHandler(ApplicationDbContext dbContext, AutoMapper.IMapper mapper)
    : ICommandHandler<GetPagedQuestionsCommand, PaginatedList<Response>>
{
    public async Task<PaginatedList<Response>> ExecuteAsync(GetPagedQuestionsCommand command, CancellationToken ct)
    {
        return await dbContext.Questions.AsQueryable()
                                        .PaginatedListProjectToAsync<Question, Response>(command.PageNumber, command.PageSize, mapper.ConfigurationProvider, ct);
    }
}