# B2B.API - Razor Website 🚀

Welcome to the ClientManagementRazor repository! This project is designed to leverage the B2B API for performing CRUD operations on clients, accompanied by a sleek login screen.

## Project Structure 📂

1. **`/Web`**: Dive into the Razor-based frontend source code here.

2. **`/API`**: Explore the API responsible for client CRUD operations and user authentication/login management.

## System Requirements ⚙️

Make sure you have the following installed before running the application:

- [.NET Core SDK](https://dotnet.microsoft.com/download)

## Configuration and Execution 🛠️

1. **Frontend (Razor)**:
   - Navigate to the `/Web` directory.
   - Run the command `dotnet run` to start the server.

2. **API**:
   - Head to the `/Api` directory.
   - Execute `dotnet run` to launch the API.

Ensure correct configuration of connection strings and other settings in the `appsettings.json` file for each project as needed.

## Accessing the Site 🌐

After starting the servers, the site will be available at `http://localhost:XXXX` by default. Dive into this URL to interact with the application.

## Site Screenshots 📸


## API Endpoints 🚀

### Client CRUD (ApiClient)
- **GET /api/clients**: Fetch the list of clients.
- **GET /api/clients/{id}**: Obtain details of a specific client.
- **POST /api/clients**: Add a new client.
- **PUT /api/clients/{id}**: Update details of an existing client.
- **DELETE /api/clients/{id}**: Remove a client.

### Authentication (LoginApi)
- **POST /api/login**: Authenticate a user and receive an access token.

## Contributions 🤝

Contributions are highly encouraged! If you come across issues or have improvement suggestions, feel free to open an issue or submit a pull request. Let's make this project even better together! 🌟
