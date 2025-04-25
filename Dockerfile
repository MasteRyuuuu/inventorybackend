# -------- 编译阶段 --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 复制所有文件并还原依赖
COPY . .
RUN dotnet restore

# 发布应用到 /app/out 目录
RUN dotnet restore "InventorySystemAPI.csproj"
RUN dotnet publish "InventorySystemAPI.csproj" -c Release -o /app/out


# -------- 运行阶段 --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# 复制发布好的文件
COPY --from=build /app/out .

# 启动项目
ENTRYPOINT ["dotnet", "InventorySystemAPI.dll"]
