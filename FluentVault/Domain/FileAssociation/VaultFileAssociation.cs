namespace FluentVault;
public record VaultFileAssociation(
    VaultFileAssociationId Id,
    VaultFileAssociationType Type,
    string? Source,
    string? ReferenceId,
    string ExpectedVaultPath,
    bool HasVaultPathChanged,
    bool IsCorrectRevision,
    VaultFile Parent,
    VaultFile Child);
