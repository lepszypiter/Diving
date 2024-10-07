using Diving.Application.Abstarction.Messaging;
using Diving.Domain.Models;

namespace Diving.Application.AddInstructor;

public record AddInstructorCommand(string Name, string Surname) : ICommand<Instructor>;
