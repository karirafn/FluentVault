namespace FluentVault;

public interface IUpdateFileRequest
{
    IUpdateFileLifecycleStateRequest LifecycleState { get; }
    IUpdateFilePropertyDefinitionsRequest PropertyDefinitions { get; }
}
