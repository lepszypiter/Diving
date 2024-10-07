using Diving.Domain.Models;
using MediatR;

namespace Diving.API.Controllers;

public record AddCourseCommand(string Name, string Instructor, int HoursOnOpenWater, int HoursOnPool, int HoursOfLectures, decimal Price) : IRequest<Course>;
