namespace FluentVault;

public record VaultFileRevision(
    long Id,
    long DefinitionId,
    string Label,
    long MaximumConsumeFileId,
    long MaximumFileId,
    long MaximumRevisionId,
    long Order);
