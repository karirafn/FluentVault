namespace FluentVault;

internal class RequestData
{
    private readonly string _name;
    private readonly string _version;
    private readonly string _service;
    private readonly string _ns;
    private readonly string _command;

    private const string AdminToolsCommand = "Connectivity.Explorer.Admin.AdminToolsCommand";
    private const string ChangeLifecycleStateCommand = "Connectivity.Explorer.DocumentPS.ChangeLifecycleStateCommand";
    private const string SignOutCommand = "Connectivity.Application.VaultBase.SignOutCommand";

    public static readonly RequestData GetAllLifeCycleDefinitions
        = new(nameof(GetAllLifeCycleDefinitions), "LifeCycle", AdminToolsCommand);

    public static readonly RequestData GetBehaviorConfigurationsByNames
        = new(nameof(GetBehaviorConfigurationsByNames), "Behavior", AdminToolsCommand);

    public static readonly RequestData GetCategoryConfigurationsByBehaviorNames
        = new(nameof(GetCategoryConfigurationsByBehaviorNames), "Category", AdminToolsCommand);

    public static readonly RequestData GetPropertyDefinitionInfosByEntityClassId
        = new(nameof(GetPropertyDefinitionInfosByEntityClassId), "Property", AdminToolsCommand);

    public static readonly RequestData FindFilesBySearchConditions
        = new(nameof(FindFilesBySearchConditions), "Document", string.Empty);

    public static readonly RequestData UpdateFileLifeCycleStates
        = new(nameof(UpdateFileLifeCycleStates), "v26", "DocumentServiceExtensions", ChangeLifecycleStateCommand, "Services/DocumentExtensions/1/7/2020/");

    public static readonly RequestData SignIn
        = new(nameof(SignIn), "Filestore/v26", "AuthService", string.Empty, "Filestore/Auth/1/7/2020/");

    public static readonly RequestData SignOut
        = new(nameof(SignOut), "Filestore/v26_2", "AuthService", SignOutCommand, "Filestore/Auth/1/8/2021/");

    private RequestData(string name, string version, string service, string command, string ns)
    {
        _name = name;
        _version = version;
        _service = service;
        _ns = ns;
        _command = GetCommand(name, command);
    }

    private RequestData(string name, string type, string command)
    {
        _name = name;
        _version = "v26";
        _service = $"{type}Service";
        _ns = $"Services/{type}/1/7/2020/";
        _command = GetCommand(name, command);
    }

    public string Name => _name;
    public string SoapAction => $@"""{Namespace}{_service}/{_name}""";
    public string Namespace => $"http://AutodeskDM/{_ns}";
    public Uri GetUri(string server)
        => new($"http://{server}/AutodeskDM/Services/{_version}/{_service}.svc{_command}");

    private static string GetCommand(string name, string command)
        => string.IsNullOrEmpty(command)
        ? string.Empty
        : $"?op={name}&currentCommand={command}";
}
