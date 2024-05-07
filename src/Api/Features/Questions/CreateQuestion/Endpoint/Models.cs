using FastEndpoints;
using FluentValidation;

namespace Api.Features.Questions.CreateQuestion.Endpoint;

public record Request(string Title, List<AnswerModel> Answers);

public record Response(Guid Id);

public record AnswerModel(string Text, bool IsCorrect);

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Answers).NotNull();
        RuleFor(x => x.Answers).Must(x => x.Count >= 4);
        RuleFor(x => x.Answers)
                .Must(x => x.Count(answer => answer.IsCorrect ) == 1);
    }
}