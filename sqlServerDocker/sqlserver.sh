docker pull microsoft/mssql-server-linux
docker run -d --name sql_server_qarfa -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=reallyStrongPwd123' -p 1433:1433 microsoft/mssql-server-linux
