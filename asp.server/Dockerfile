# Use the official .NET SDK image to take advantage of the SDK's built-in features, including the watch tool
FROM mcr.microsoft.com/dotnet/sdk:6.0

# Set the working directory inside the container
WORKDIR /app

# Copy the csproj file and restore any .NET dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining source code
COPY . ./

# Expose the port your app runs on
EXPOSE 5000

# Start the application using dotnet watch to enable hot reloading
ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://*:5000"]
