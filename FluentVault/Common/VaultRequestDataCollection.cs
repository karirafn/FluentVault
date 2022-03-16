namespace FluentVault.Common;

internal class VaultRequestDataCollection
{
    public static IEnumerable<VaultRequestData> SoapRequestData => new VaultRequestData[]
    {
        new(
          operation: "GetAllLifeCycleDefinitions",
          version: "v26",
          service: "LifeCycleService",
          command: "Connectivity.Explorer.Admin.AdminToolsCommand",
          @namespace: "Services/LifeCycle/1/7/2020"
        ),
        new(
          operation: "GetBehaviorConfigurationsByNames",
          version: "v26",
          service: "BehaviorService",
          command: "Connectivity.Explorer.Admin.AdminToolsCommand",
          @namespace: "Services/Behavior/1/7/2020"
        ),
        new(
          operation: "GetCategoryConfigurationsByBehaviorNames",
          version: "v26",
          service: "CategoryService",
          command: "Connectivity.Explorer.Admin.AdminToolsCommand",
          @namespace: "Services/Category/1/7/2020"
        ),
        new(
          operation: "GetPropertyDefinitionInfosByEntityClassId",
          version: "v26",
          service: "PropertyService",
          command: "Connectivity.Explorer.Admin.AdminToolsCommand",
          @namespace: "Services/Property/1/7/2020"
        ),
        new(
          operation: "GetUserInfosByUserIds",
          version: "v26",
          service: "AdminService",
          command: "Connectivity.Explorer.Admin.SecurityCommand",
          @namespace: "Services/Admin/1/7/2020"
        ),
        new(
          operation: "FindFilesBySearchConditions",
          version: "v26",
          service: "DocumentService",
          command: "",
          @namespace: "Services/Document/1/7/2020"
        ),
        new(
          operation: "UpdateFileLifeCycleStates",
          version: "v26",
          service: "DocumentServiceExtensions",
          command: "Connectivity.Explorer.DocumentPS.ChangeLifecycleStateCommand",
          @namespace: "Services/DocumentExtensions/1/7/2020"
        ),
        new(
          operation: "UpdateFilePropertyDefinitions",
          version: "v26",
          service: "DocumentService",
          command: "Connectivity.Explorer.DocumentPS.ChangeLifecycleStateCommand",
          @namespace: "Services/Document/1/7/2020"
        ),
        new(
          operation: "SignIn",
          version: "Filestore/v26_2",
          service: "AuthService",
          command: "Connectivity.Application.VaultBase.SignInCommand",
          @namespace: "Filestore/Auth/1/8/2021"
        ),
        new(
          operation: "SignOut",
          version: "Filestore/v26",
          service: "AuthService",
          command: "Connectivity.Application.VaultBase.SignOutCommand",
          @namespace: "Filestore/Auth/1/7/2020"
        )
    };
}
