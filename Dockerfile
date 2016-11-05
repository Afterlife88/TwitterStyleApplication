FROM microsoft/dotnet:latest

# Set env variables
ENV ASPNETCORE_URLS http://*:5000

COPY /src/FileStorage.Web /app/src/FileStorage.Web
COPY /src/FileStorage.Domain /app/src/FileStorage.Domain
COPY /src/FileStorage.Utils /app/src/FileStorage.Utils
COPY /src/FileStorage.Services /app/src/FileStorage.Services
COPY /src/FileStorage.DAL /app/src/FileStorage.DAL

# Restore domain
WORKDIR /app/src/FileStorage.Domain
RUN ["dotnet", "restore"]

# Restore DAL
WORKDIR /app/src/FileStorage.DAL
RUN ["dotnet", "restore"]

# Restore services
WORKDIR /app/src/FileStorage.Services
RUN ["dotnet", "restore"]

# Restore utils
WORKDIR /app/src/FileStorage.Utils
RUN ["dotnet", "restore"]

WORKDIR /app/src/FileStorage.Web
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
 
# Open port
EXPOSE 5000/tcp
 
ENTRYPOINT ["dotnet", "run"]