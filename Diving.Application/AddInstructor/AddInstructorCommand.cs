using Diving.Application.Abstarction.Messaging;
using Diving.Domain.Instructor;

namespace Diving.Application.AddInstructor;

public record AddInstructorCommand(string Name, string Surname) : ICommand<Instructor>;
