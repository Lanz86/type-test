

using Api.Entities.Common;

namespace Api.Entities;

public class Question : AuditableBaseEntity<Guid>
{
    private Question(Guid id, string title) : base(id)
    {
        Id = id;
        Title = title;
        Answers = new List<Answer>();
    }
    
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public List<Answer> Answers { get; private set; }
    
    public static Question Create(string title)
    {
        return new Question(Guid.NewGuid(), title);
    }
    
    public void AddAnswer(string text, bool isCorrect)
    {
        Answers.Add(Answer.Create(text, this.Id, isCorrect));
    }
}