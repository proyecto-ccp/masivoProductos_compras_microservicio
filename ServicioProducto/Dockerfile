
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY compilado/ ./
ENTRYPOINT ["dotnet", "ServicioProducto.dll"]
EXPOSE 8080
