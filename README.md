# MCP Server – Weather & Book Tools  

📂 **Project Location:**  
`MVC_Test/ProjectUpdate/NewApp_SK/MCP`  

This project is a **Model Context Protocol (MCP) Server** in .NET, extended with:  

- 🌤 **Weather Tool** – fetches real-time weather info for any city  
- 📚 **Book Tool** – retrieves book details or filters books by category  

---

## 🚀 Features
- Built with **.NET Core** and **Semantic Kernel MCP Server**  
- MCP Tools included:
  - `ExternalWeatherTool` → Get weather for any city  
  - `BookTool` → Query pre-defined computer science books  
- Supports **Stdio transport** for MCP communication  

---

## 📂 Project Structure
MVC_Test/ProjectUpdate/NewApp_SK/MCP
│── MCP.csproj
│── Program.cs
│── appsettings.json (optional, for API key)
│
├── Tool/
│ ├── ExternalWeatherTool.cs (Weather API integration)
│ └── BookTool.cs (Static book database tool)
│
└── Config/
└── WeatherApiConfig.cs (optional, if using appsettings.json)

## ⚙️ Setup

### 1. Clone & Navigate
cd MVC_Test/ProjectUpdate/NewApp_SK/MCP
2. Restore dependencies
dotnet restore
3. Weather API Key Options
You can use one of two approaches:

🔹 Option A – Hard-code API key (quick & simple)
In ExternalWeatherTool.cs:
private readonly string _weatherApiKey = "your-api-key-here";
🔹 Option B – Config-based (recommended)
Create appsettings.json:
{
  "WeatherApi": {
    "ApiKey": "your-api-key-here"
  }
}
Then load it in Program.cs (already configured in sample code).

🔹 Option C – No API Key Needed (wttr.in)
Replace the URL in ExternalWeatherTool.cs:
var apiUrl = $"https://wttr.in/{cityName}?format=j1";
▶️ Running the MCP Server
Run the project:
dotnet run
Expected output:
Hello, MCP Server
The server is now ready to accept MCP requests over stdio.

🌤 Weather Tool Usage
Query:
Let me know weather in Mumbai
The current weather in Mumbai:

Temperature: 29°C (Feels like 34°C)
Condition: Haze
Humidity: 70%
Wind: 13 km/h (WNW)
Pressure: 1008 hPa
Visibility: 4 km
Let me know if you need a forecast or more details.

📚 Book Tool Usage
1. Get All Books
Send all the available books
Response:
Here are the available books:

Introduction to Algorithms
Author: Thomas H. Cormen
ISBN: 9780262033848
Price: $89.99
Category: Algorithms
Description: A comprehensive book covering a broad range of algorithms in depth.

Clean Code
Author: Robert C. Martin
ISBN: 9780132350884
Price: $49.99
Category: Software Engineering
Description: A handbook of agile software craftsmanship with a focus on writing clean code.

Design Patterns
Author: Erich Gamma
ISBN: 9780201633610
Price: $59.99
Category: Architecture
Description: Elements of reusable object-oriented software and design principles.

The Pragmatic Programmer
Author: Andy Hunt
ISBN: 9780135957059
Price: $44.99
Category: Programming
Description: Your journey to mastery in modern software development.

Let me know if you need details about any specific book.


2. Find Books by Category
BookTool.GetBooksByCategory("Programming")
Find books in Programming

Response:
Title: The Pragmatic Programmer
Author: Andy Hunt
Price: $44.99
Description: Your journey to mastery in modern software development.


🛡 Notes
Weather API: Use WeatherAPI.com (with API key) or wttr.in (no key needed).
Book Tool: Uses a static in-memory JSON, fast & offline.
Natural Language Mapping: Phrases like
“Let me know weather in Mumbai” → calls GetWeather("Mumbai")
"Send all the available books" → calls GetAllBooks()
“Find books in Programming” → calls GetBooksByCategory("Programming")
