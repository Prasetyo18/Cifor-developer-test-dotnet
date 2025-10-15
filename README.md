# CIFOR Developer Test - Ready-to-run (ASP.NET Core + Static Frontend)

This repository contains a single ASP.NET Core (NET 8) project with an in-memory employee API and a static frontend served from `wwwroot`.

## Requirements
- .NET 8 SDK installed. Download from https://dotnet.microsoft.com if needed.

## Run
```bash
git clone <repo-or-zip>
cd cifor-developer-test-dotnet8
dotnet restore
dotnet run
```

The app is configured to serve on `http://localhost:5000`.

Open `http://localhost:5000` in your browser. The static frontend is at `/index.html` (served automatically) and talks to the API endpoints:

- `GET /Get` - list employees
- `GET /Get/{employeeId}` - detail
- `GET /Get/employees/search?name=&department=` - search
- `POST /employees` - create
- `PUT /employees/{employeeId}` - update
- `DELETE /employees/{employeeId}` - delete

Notes:
- Data is stored in memory and will reset on server restart.
- Swagger is enabled in Development mode at `/swagger`.

Good luck with the test — kalau mau saya juga bisa push langsung ke GitHub jika kamu beri nama repo atau akses token.
