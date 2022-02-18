namespace FluentVault;

public interface IUpdateFileRequest
{
    IUpdateFileLifecycleStateRequest LifecycleState { get; }
}
