namespace FluentVault;

public record VaultFileRevision(
    VaultRevisionId Id,
    VaultRevisionDefinitionId DefinitionId,
    string Label,
    long MaximumConsumeFileId,
    long MaximumFileId,
    long MaximumRevisionId,
    long Order);
