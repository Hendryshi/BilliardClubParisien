# 使用运行时镜像
FROM mcr.microsoft.com/dotnet/aspnet:8.0

COPY Publish/. /app
WORKDIR /app

ENV ASPNETCORE_URLS http://*:8060
ENV ASPNETCORE_ENVIRONMENT Production

EXPOSE 8060
 
ENTRYPOINT ["dotnet", "BCP.Api.dll"]