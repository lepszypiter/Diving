namespace Diving.Domain.BuildingBlocks;

public interface IBusinessRule
{
    bool IsBroken();

    string Message { get; }
}
