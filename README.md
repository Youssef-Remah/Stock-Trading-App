# Stock Trading App

The Stock Trading App is an **ASP.NET Core 8 web application** that displays real-time stock prices using data from Finnhub.io. It fetches and displays real-time stock prices using the Finnhub.io API, providing a user-friendly interface to view market data. The app is designed with the **MVC architecture**, follows the **repository pattern** for data management, and includes **unit tests** to ensure reliability. The application uses **SQL Server** for data storage and **Entity Framework Core** for database operations, and includes logging capabilities to monitor and debug the application effectively.

## Features

- **Real-Time Stock Prices:** Live updates of stock prices fetched from Finnhub.io.

- **MVC Architecture:** Structured using the Model-View-Controller design pattern for organized code management.

- **Repository Pattern:** Implements the repository pattern to manage data access, promoting separation of concerns.

- **Unit Testing:** Comprehensive tests using xUnit to ensure application reliability.

- **Integration Testing:** Integration tests to verify end-to-end functionality and ensure proper functioning of all components.

- **Logging with Serilog:** Application events and errors are logged using Serilog, providing detailed insights into application behavior.

- **Seq Integration:** Logs are routed to Seq for centralized and structured log analysis, enabling easier debugging and monitoring.

## Technologies Used

- **ASP.NET Core 8:** Framework for building the web application.

- **Entity Framework Core 8:** Object-relational mapper for database interactions.

- **SQL Server:** Relational database for storing application data.

- **Razor Views:** For dynamic server-side rendering of HTML content.

- **JavaScript, HTML, CSS:** Frontend technologies for user interface and interactivity.

## Getting Started

### Prerequisites

- **.NET SDK** 8.0 or later

- **Finnhub.io API Key:** Obtain an API key from [Finnhub.io](https://finnhub.io/) to fetch stock data.

- **Seq:** For logging, Seq should be installed and running on `http://localhost:5341`. You can download it from [Seq's website](https://docs.datalust.co/docs/getting-started).

### Installation

1. **Clone the repository:**

    ```
    git clone https://github.com/Youssef-Remah/Stock-Trading-App.git
    ```

2. **Navigate to the project directory:**
    
    ```
    cd Stock-Trading-App
    ```

3. **Add your Finnhub API key to user secrets:**

    ```
    dotnet user-secrets set "FinnhubToken" "<Your_Finnhub_API_Token>"
    ```

4. **Restore dependencies and build the project:**

    ```
    dotnet build
    ```

5. **Run the application:**

    ```
    dotnet run --project StockTradingApp
    ```
