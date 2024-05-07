using Api.Entities;
using AutoMapper;

namespace Api.Features.Questions.GetQuestions.Endpoint;

public record Response(Guid Id, string Title);

public record Request(int PageSize = 10, int PageIndex = 1);

public class ResponseProfile : Profile
{
    public ResponseProfile()
    {
        CreateMap<Question, Response>();
    }
}

