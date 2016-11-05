FROM microsoft/dotnet:latest

# Set env variables
ENV ASPNETCORE_URLS http://*:5000

COPY /src/TwitterStyleApplication.Web /app/src/TwitterStyleApplication.Web
COPY /src/TwitterStyleApplication.Services /app/src/TwitterStyleApplication.Services
COPY /src/TwitterStyleApplication.Domain /app/src/TwitterStyleApplication.Domain
COPY /src/TwitterStyleApplication.DAL /app/src/TwitterStyleApplication.DAL

# Restore domain
WORKDIR /app/src/TwitterStyleApplication.Domain
RUN ["dotnet", "restore"]

# Restore DAL
WORKDIR /app/src/TwitterStyleApplication.DAL
RUN ["dotnet", "restore"]

# Restore services
WORKDIR /app/src/TwitterStyleApplication.Services
RUN ["dotnet", "restore"]

WORKDIR /app/src/TwitterStyleApplication.Web
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
 
# Open port
EXPOSE 5000/tcp
 
ENTRYPOINT ["dotnet", "run"]