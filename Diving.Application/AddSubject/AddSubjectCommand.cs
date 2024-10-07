using Diving.Application.Abstarction.Messaging;
using Diving.Domain;

namespace Diving.Application.AddSubject;

public record AddSubjectCommand(int courseId, int subjectId, string subjectName) : ICommand<Subject>;
