# Stock Trading App

The Stock Trading App is an **ASP.NET Core 8 web application** that displays real-time stock prices using data from Finnhub.io. It provides a user-friendly interface for viewing market data and is structured using a **4-Tier Architecture**, where the **MVC pattern** is applied at the Presentation Layer.

The application follows the **Repository Pattern** for data management, ensuring separation of concerns. It includes **unit and integration tests** to enhance reliability. **SQL Server** is used for data storage, managed via **Entity Framework Core**. The application provides **logging** capabilities using **Serilog**.

## Features

- **Real-Time Stock Prices:** Live updates of stock prices fetched from Finnhub.io.

- **4-Tier Architecture – Structured into:**
    - Presentation Layer (MVC for UI and Controllers)
    - Business Logic Layer (BLL)
    - Repository Layer (Interaction with DAL)
    - Data Access Layer (DAL)

- **Repository Pattern:** Implements the repository pattern to manage data access, promoting separation of concerns.

- **Unit Testing:** Comprehensive unit tests using xUnit to ensure application reliability.

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
