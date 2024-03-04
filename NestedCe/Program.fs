type DaprConfig = {
    AppId: string
    AppPort: int
    AppProtocol: string
}

type DaprConfigBuilder() =

    member inline _.Yield(()) = {
        AppId = ""
        AppPort = 0
        AppProtocol = "http"
    }

    member inline _.Run(state) = state

    [<CustomOperation("appId")>]
    member inline _.SetAppId(state, value) = { state with AppId = value }

    [<CustomOperation("appPort")>]
    member inline _.SetAppPort(state, value) = { state with AppPort = value }

    [<CustomOperation("appProtocol")>]
    member inline _.SetProtocol(state, value) = { state with AppProtocol = value }

let dapr = DaprConfigBuilder()


type ContainerConfig = { Name: string; DaprConfig: DaprConfig }

type ContainerBuilder() =

    member inline _.Yield(()) = {
        Name = ""
        DaprConfig = {
            AppId = ""
            AppPort = 0
            AppProtocol = ""
        }
    }

    member inline _.Run(state) = state

    [<CustomOperation("name")>]
    member inline _.SetName(state, value) = { state with Name = value }

    [<CustomOperation("dapr")>]
    member inline _.SetDaprConfig(state, value) = { state with DaprConfig = value }


let container = ContainerBuilder()

let containerCe =
    container {
        name "My Container"

        dapr {
            appId "myApp-123"
            appPort 5000
            appProtocol "gprs"
        }
    }

containerCe
|> System.Text.Json.JsonSerializer.Serialize
|> printfn "%s"
