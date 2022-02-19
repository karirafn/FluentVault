namespace FluentVault;

internal static class RequestData
{
    private const string GetAllLifeCycleDefinitions = nameof(GetAllLifeCycleDefinitions);
    private const string GetBehaviorConfigurationsByNames = nameof(GetBehaviorConfigurationsByNames);
    private const string GetCategoryConfigurationsByBehaviorNames = nameof(GetCategoryConfigurationsByBehaviorNames);
    private const string GetPropertyDefinitionInfosByEntityClassId = nameof(GetPropertyDefinitionInfosByEntityClassId);
    private const string FindFilesBySearchConditions = nameof(FindFilesBySearchConditions);
    private const string UpdateFileLifeCycleStates = nameof(UpdateFileLifeCycleStates);
    private const string SignIn = nameof(SignIn);
    private const string SignOut = nameof(SignOut);

    public static string GetSoapAction(string name)
        => $@"""{GetNamespace(name)}{GetService(name)}/{name}""";

    public static Uri GetRequestUri(string name, string server)
        => new($"http://{server}/AutodeskDM/Services/{GetVersion(name)}/{GetService(name)}.svc{GetCommand(name)}");

    public static string GetNamespace(string name)
    {
        string ns = name switch
        {
            GetAllLifeCycleDefinitions => "Services/LifeCycle/1/7/2020/",
            GetBehaviorConfigurationsByNames => "Services/Behavior/1/7/2020/",
            GetCategoryConfigurationsByBehaviorNames => "Services/Category/1/7/2020/",
            GetPropertyDefinitionInfosByEntityClassId => "Services/Property/1/7/2020/",
            FindFilesBySearchConditions => "Services/Document/1/7/2020/",
            UpdateFileLifeCycleStates => "Services/DocumentExtensions/1/7/2020/",
            SignIn => "Filestore/Auth/1/7/2020/",
            SignOut => "Filestore/Auth/1/8/2021/",
            _ => throw new KeyNotFoundException($@"Unable to find namespace for ""{name}""")
        };

        return $"http://AutodeskDM/{ns}";
    }

    private static string GetService(string name) => name switch
    {
        GetAllLifeCycleDefinitions => "LifeCycleService",
        GetBehaviorConfigurationsByNames => "BehaviorService",
        GetCategoryConfigurationsByBehaviorNames => "CategoryService",
        GetPropertyDefinitionInfosByEntityClassId => "PropertyService",
        FindFilesBySearchConditions => "DocumentService",
        UpdateFileLifeCycleStates => "DocumentServiceExtensions",
        SignIn or SignOut => "AuthService",
        _ => throw new KeyNotFoundException($@"Unable to find service for ""{name}""")
    };

    private static string GetVersion(string name)
    {
        string version = name switch
        {
            GetAllLifeCycleDefinitions => "v26",
            GetBehaviorConfigurationsByNames => "v26",
            GetCategoryConfigurationsByBehaviorNames => "v26",
            GetPropertyDefinitionInfosByEntityClassId => "v26",
            FindFilesBySearchConditions => "v26",
            UpdateFileLifeCycleStates => "v26",
            SignIn => "v26",
            SignOut => "v26_2",
            _ => throw new KeyNotFoundException($@"Unable to find service version for ""{name}""")
        };

        return new[] { SignIn, SignOut }.Contains(name)
            ? $"Filestore/{version}"
            : version;
    }

    private static string GetCommand(string name)
    {
        string command = name switch
        {
            GetAllLifeCycleDefinitions => "Connectivity.Explorer.Admin.AdminToolsCommand",
            GetBehaviorConfigurationsByNames => "Connectivity.Explorer.Admin.AdminToolsCommand",
            GetCategoryConfigurationsByBehaviorNames => "Connectivity.Explorer.Admin.AdminToolsCommand",
            GetPropertyDefinitionInfosByEntityClassId => "Connectivity.Explorer.Admin.AdminToolsCommand",
            FindFilesBySearchConditions => string.Empty,
            UpdateFileLifeCycleStates => "Connectivity.Explorer.DocumentPS.ChangeLifecycleStateCommand",
            SignIn => string.Empty,
            SignOut => "Connectivity.Application.VaultBase.SignOutCommand",
            _ => throw new KeyNotFoundException($@"Unable to find command for ""{name}""")
        };

        return string.IsNullOrEmpty(command)
            ? string.Empty
            : $"?op={name}&currentCommand={command}";
    }
}
