namespace FluentVault;

public interface IWithFileMasterId
{
    public IWithComment ToStateWithId(long stateId);
}
