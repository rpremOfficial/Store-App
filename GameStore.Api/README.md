# Game Store API

## Satrting SQL Server

    ```powershell
        $sa_password = "[SA Password here]"

        docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -e "MSSQL_PID=Evaluation" -p 1433:1433  --name mssql --hostname sqlpreview -v sqlvolume:/var/opt/mssql -d --rm mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04
    ```
    ## Setting the connection string to secret manager

    ```powershell
        $sa_password = "[SA Password here]"
        dotnet user-secrets set "ConnectionStrings:GameStoreContext" "Server=localhost; Database=GameStore; User Id=sa; Password=$sa_password; TrustServerCertificate=True"
    ```
