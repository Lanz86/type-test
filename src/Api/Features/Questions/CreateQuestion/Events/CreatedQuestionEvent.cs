using FastEndpoints;

namespace Api.Features.Questions.CreateQuestion.Events;

public class CreatedQuestionEvent : IEvent
{
    public Guid QuestionId { get; set; }
}