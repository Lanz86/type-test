using Api.Features.Questions.CreateQuestion.Command;
using Api.Infrastructure;
using Api.Infrastructure.Data;
using Api.Services;
using FastEndpoints;

namespace Api.Features.Questions.CreateQuestion.Endpoint;

public class Endpoint(ApplicationDbContext dbContext, IUser user) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/api/questions");
        //AllowAnonymous();
    }
    
    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var result = await new CreateQuestionCommand()
        {
            Title = request.Title,
            Answers = request.Answers
        }.ExecuteAsync(ct);
        
        await SendAsync(new Response(result), cancellation: ct);
    }
}