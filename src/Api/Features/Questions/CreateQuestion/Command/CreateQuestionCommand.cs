using Api.Entities;
using Api.Features.Questions.CreateQuestion.Endpoint;
using Api.Features.Questions.CreateQuestion.Events;
using Api.Infrastructure;
using Api.Infrastructure.Data;
using Api.Services;
using FastEndpoints;

namespace Api.Features.Questions.CreateQuestion.Command;

public class CreateQuestionCommand : ICommand<Guid>
{
    public string Title { get; set; }
    public List<AnswerModel> Answers { get; set; }
}

public class CommandCreateQuestionCommandHandler(ApplicationDbContext dbContext, IUser user) : ICommandHandler<CreateQuestionCommand, Guid>
{
    public async Task<Guid> ExecuteAsync(CreateQuestionCommand command, CancellationToken ct)
    {
        var question = Question.Create(command.Title);
        
        foreach (var answer in command.Answers)
        {
            question.AddAnswer(answer.Text, answer.IsCorrect);
        }
        
        // Save question to database
        await dbContext.Questions.AddAsync(question, ct);
        await dbContext.SaveChangesAsync(ct);
        
        
        await new CreatedQuestionEvent
        {
            QuestionId = question.Id
        }.PublishAsync(Mode.WaitForNone, ct);
        
        return question.Id;
    }
} 