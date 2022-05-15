namespace FluentVault;

public interface IGetPropertiesRequestBuilder
{
    public IEntitySelector ForEntityClass(VaultEntityClass entityClass);
}
