namespace FluentVault;

public interface IUpdateFileRequestBuilder
{
    IUpdateFileLifecycleStateRequestBuilder LifecycleState { get; }
    IUpdateFilePropertyDefinitionsRequestBuilder PropertyDefinitions { get; }
}
