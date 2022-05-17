namespace FluentVault;

public interface IGetPropertiesRequestBuilder
{
    public IGetPropertiesEntitySelector ForEntityClass(VaultEntityClass entityClass);
}
