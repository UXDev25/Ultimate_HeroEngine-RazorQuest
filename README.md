# Ultimate HeroEngine - Razor Quest

Welcome to the **Ultimate HeroEngine - Razor Quest**! This is a web application built with ASP.NET Core Razor Pages to manage heroes, their abilities, and combat history statistics.

## Prerequisites

Before you begin, ensure you have the following installed on your machine:
- [.NET SDK](https://dotnet.microsoft.com/download) (Version 6.0 or higher)
- [Node.js & npm](https://nodejs.org/) (for frontend package management)
- **Git**
- An IDE such as **JetBrains Rider**, **Visual Studio**, or **Visual Studio Code**.

---

## Getting Started

Follow these steps to get the project up and running on your local machine:

### 1. Clone the repository
Open your terminal and run the following commands:
```bash
git clone [https://github.com/UXDev25/Ultimate_HeroEngine-RazorQuest.git](https://github.com/UXDev25/Ultimate_HeroEngine-RazorQuest.git)
cd Ultimate_HeroEngine-RazorQuest
```

### 2. Install Frontend Dependencies
Since this project includes a `package.json` for frontend assets, install the required packages by running:
```bash
npm install
```

### 3. Run the Application

#### Option A: Using an IDE (Recommended)
1. Open the `HeroEngineRazor.sln` solution file in your preferred IDE (Rider or Visual Studio).
2. Ensure that **`HeroEngineRazor`** is set as the Startup Project.
3. Click the **Run** (or Play) button.

#### Option B: Using the .NET CLI
If you prefer using the command line, navigate to the web project directory and run it:
```bash
cd HeroEngineRazor
dotnet run
```

### 4. Access the Web App
Once the application starts, open your web browser and navigate to the local URL provided in your terminal output (typically `https://localhost:7000` or `http://localhost:5000`, depending on your configuration).

---

## Project Structure

- **`HeroEngineRazor/`**: The ASP.NET Core Razor Pages web application. It contains the UI, Pages, and web configurations.
- **`Ultimate_HeroEngine/`**: The core C# class library. It holds all the business logic, entities (Heroes, Abilities), and data analytics.
- **`HeroEngineRazor.sln`**: The main solution file that connects both the Core engine and the Web interface.
