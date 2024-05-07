using Api.Commons.Models;
using Api.Features.Questions.GetPagedQuestions.Command;
using Api.Infrastructure;
using Api.Infrastructure.Data;
using FastEndpoints;

namespace Api.Features.Questions.GetQuestions.Endpoint;

public class Endpoint(ApplicationDbContext dbContext) : Endpoint<Request, PaginatedList<Response>>
{
    
    public override void Configure()
    {
        Get("/api/questions");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var result = await new GetPagedQuestionsCommand()
        {
            PageSize = req.PageSize,
            PageNumber = req.PageIndex
        }.ExecuteAsync(ct);
        
        await SendAsync(result, cancellation: ct);
    }
}