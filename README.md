# TravelPlanner

A Windows Forms desktop app for planning trips and tracking expenses. Data is stored locally in SQLite.

## Features

- Login with a local user account
- Add, edit, and delete trips (destination, dates, budget, status, notes, image)
- Dashboard with trip list, duration, and total budget
- Option to hide completed trips
- Per-trip expenses (Food, Transport, Stay, Activity, Other)
- Expense totals compared against the trip budget

## Requirements

- Windows
- .NET 8 SDK or Visual Studio 2022+
- Visual Studio workload: **.NET desktop development**

## How to run

1. Clone this repository.
2. Open `TravelPlanner/TravelPlanner.csproj` in Visual Studio.
3. Press **F5** (or **Debug → Start Debugging**).

From a terminal:

```bash
cd TravelPlanner
dotnet restore
dotnet run
```

The first run creates `travelplanner.db` in the working directory.

## Default login

| Username | Password |
|----------|----------|
| `huda`   | `huda123` |

This account is created automatically if the Users table is empty.

## Project structure

```
TravelPlanner/
  Program.cs              App entry and login session
  Data/DatabaseHelper.cs  SQLite setup, login, password hashing
  Forms/                  Login, dashboard, trips, expenses
  Models/                 Trip and Expense
```
