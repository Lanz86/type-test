

using Api.Entities.Common;

namespace Api.Entities;

public class Answer : AuditableBaseEntity<Guid>
{
    public string Text { get; private set; }
    public Guid QuestionId { get; private set; }
    public bool IsCorrect { get; private set; }
    
    private Answer(Guid id, string text, Guid questionId, bool isCorrect) : base(id)
    {
        Text = text;
        QuestionId = questionId;
        IsCorrect = isCorrect;
    }
    
    public static Answer Create(string text, Guid questionId, bool isCorrect)
    {
        return new Answer(Guid.NewGuid(), text, questionId, isCorrect);
    }
}