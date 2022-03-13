namespace FluentVault;

public interface IWithFiles
{
    public IWithComment ToStateWithId(VaultLifeCycleStateId stateId);
}
