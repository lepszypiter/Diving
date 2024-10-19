using Diving.Domain.Course;

namespace Diving.Application.CreateSubject;

public record CreateSubjectCommand(int CourseId, string Name) : ICommand<Subject>;
