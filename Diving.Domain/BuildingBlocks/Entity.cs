namespace Diving.Domain.BuildingBlocks;

public abstract class Entity
{
    protected static void CheckRule(IBusinessRule rule)
    {
        if (!rule.IsBroken())
        {
            return;
        }

        throw new BusinessRuleValidationException(rule);
    }
}
