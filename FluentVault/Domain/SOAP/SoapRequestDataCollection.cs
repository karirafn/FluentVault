namespace FluentVault.Domain.SOAP;

internal class SoapRequestDataCollection
{
    public static IEnumerable<SoapRequestData> SoapRequestData => new SoapRequestData[]
    {
        new(
          name: "GetAllLifeCycleDefinitions",
          version: "v26",
          service: "LifeCycleService",
          command: "Connectivity.Explorer.Admin.AdminToolsCommand",
          @namespace: "Services/LifeCycle/1/7/2020"
        ),
        new(
          name: "GetBehaviorConfigurationsByNames",
          version: "v26",
          service: "BehaviorService",
          command: "Connectivity.Explorer.Admin.AdminToolsCommand",
          @namespace: "Services/Behavior/1/7/2020"
        ),
        new(
          name: "GetCategoryConfigurationsByBehaviorNames",
          version: "v26",
          service: "CategoryService",
          command: "Connectivity.Explorer.Admin.AdminToolsCommand",
          @namespace: "Services/Category/1/7/2020"
        ),
        new(
          name: "GetPropertyDefinitionInfosByEntityClassId",
          version: "v26",
          service: "PropertyService",
          command: "Connectivity.Explorer.Admin.AdminToolsCommand",
          @namespace: "Services/Property/1/7/2020"
        ),
        new(
          name: "FindFilesBySearchConditions",
          version: "v26",
          service: "DocumentService",
          command: "",
          @namespace: "Services/Document/1/7/2020"
        ),
        new(
          name: "UpdateFileLifeCycleStates",
          version: "v26",
          service: "DocumentServiceExtensions",
          command: "Connectivity.Explorer.DocumentPS.ChangeLifecycleStateCommand",
          @namespace: "Services/DocumentExtensions/1/7/2020"
        ),
        new(
          name: "UpdateFilePropertyDefinitions",
          version: "v26",
          service: "AuthService",
          command: "Connectivity.Explorer.DocumentPS.ChangeLifecycleStateCommand",
          @namespace: "Services/Document/1/7/2020"
        ),
        new(
          name: "SignIn",
          version: "Filestore/v26_2",
          service: "DocumentService",
          command: "",
          @namespace: "Filestore/Auth/1/8/2021"
        ),
        new(
          name: "SignOut",
          version: "Filestore/v26",
          service: "DocumentService",
          command: "Connectivity.Application.VaultBase.SignOutCommand",
          @namespace: "Filestore/Auth/1/7/2020"
        )
    };
}
