namespace FluentVault;

internal class RequestData
{
    private const string GetAllLifeCycleDefinitions = nameof(GetAllLifeCycleDefinitions);
    private const string GetBehaviorConfigurationsByNames = nameof(GetBehaviorConfigurationsByNames);
    private const string GetCategoryConfigurationsByBehaviorNames = nameof(GetCategoryConfigurationsByBehaviorNames);
    private const string GetPropertyDefinitionInfosByEntityClassId = nameof(GetPropertyDefinitionInfosByEntityClassId);
    private const string FindFilesBySearchConditions = nameof(FindFilesBySearchConditions);
    private const string UpdateFileLifeCycleStates = nameof(UpdateFileLifeCycleStates);
    private const string SignIn = nameof(SignIn);
    private const string SignOut = nameof(SignOut);

    private const string AdminToolsCommand = "Connectivity.Explorer.Admin.AdminToolsCommand";
    private const string ChangeLifecycleStateCommand = "Connectivity.Explorer.DocumentPS.ChangeLifecycleStateCommand";
    private const string SignOutCommand = "Connectivity.Application.VaultBase.SignOutCommand";

    private static readonly IEnumerable<RequestData> _data = new RequestData[]
    {
        new(GetAllLifeCycleDefinitions, "LifeCycle", AdminToolsCommand),
        new(GetBehaviorConfigurationsByNames, "Behavior", AdminToolsCommand),
        new(GetCategoryConfigurationsByBehaviorNames, "Category", AdminToolsCommand),
        new(GetPropertyDefinitionInfosByEntityClassId, "Property", AdminToolsCommand),
        new(FindFilesBySearchConditions, "Document", string.Empty),
        new(UpdateFileLifeCycleStates, "v26", "DocumentServiceExtensions", ChangeLifecycleStateCommand, "Services/DocumentExtensions/1/7/2020/"),
        new(SignIn, "Filestore/v26", "AuthService", string.Empty, "Filestore/Auth/1/7/2020/"),
        new(SignOut, "Filestore/v26_2", "AuthService", SignOutCommand, "Filestore/Auth/1/8/2021/"),
    };

    private readonly string _name;
    private readonly string _version;
    private readonly string _service;
    private readonly string _ns;
    private readonly string _command;

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

    public RequestData(string name)
    {
        var data = _data.FirstOrDefault(x => x._name == name)
            ?? throw new KeyNotFoundException($@"No data exists for ""{name}""");

        _name = data._name;
        _version = data._version;
        _service= data._service;
        _ns= data._ns;
        _command = data._command;
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
