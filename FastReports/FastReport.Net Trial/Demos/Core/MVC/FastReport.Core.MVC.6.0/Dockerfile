#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

RUN apt-get update
RUN apt-get install -y libgif-dev autoconf libtool automake build-essential gettext libglib2.0-dev libcairo2-dev libtiff-dev libexif-dev libpango1.0-dev git

RUN mkdir /libgdiplus

RUN git clone https://github.com/mono/libgdiplus.git /libgdiplus

WORKDIR /libgdiplus
RUN ./autogen.sh --with-pango --prefix=/usr
RUN make
RUN make install 

EXPOSE 80
EXPOSE 443

RUN mkdir /Reports
WORKDIR /Reports
COPY ["Reports/", "."]

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN dotnet restore "Core/MVC/FastReport.Core.MVC.6.0/FastReport.Core.MVC.6.0.csproj"
COPY ["Core/MVC/FastReport.Core.MVC.6.0/", "."]
RUN dotnet build "FastReport.Core.MVC.6.0.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FastReport.Core.MVC.6.0.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastReport.Core.MVC.6.0.dll"]