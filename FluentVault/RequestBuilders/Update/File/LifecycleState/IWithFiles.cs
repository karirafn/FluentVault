namespace FluentVault;

public interface IWithFiles
{
    public IWithComment ToStateWithId(LifeCycleStateId stateId);
}
