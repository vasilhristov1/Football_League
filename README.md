# Football League Web Application

This is a full-stack football league management application with:

- **Backend**: ASP.NET Core Web API + EF Core + SQL Server
- **Frontend**: React + TypeScript + Material UI
- **Database**: SQL Server via Docker
- **Auth**: JWT-based login/signup with Admin/User roles

---

## Project Structure

```
.
├── backend/       # ASP.NET Core backend
    └── docker-compose.yml
|
└── frontend/      # React frontend (Vite + MUI)
```

---

## Getting Started

### 1. Clone the repo

```bash
git clone <your-repo-url>
cd <project-root>
```

---

### 2. Run backend & database via Docker

```bash
cd backend

docker compose up --build
```

- API runs on: `http://localhost:8080`
- SQL Server on: `localhost:1433`
- Swagger: `http://localhost:8080/swagger`

---

### 3. Run the frontend

```bash
cd frontend
npm install
npm run dev
```

Visit: `http://localhost:5173`

Ensure `axiosConfig.ts` points to `http://localhost:8080`.

---

## Default Admin User

On first run, seeded user:

```
Username: admin
Password: admin123
Role: Admin
```

Signup creates users with role `User`.

---

## API Docs

- Swagger: `http://localhost:8080/swagger`

---

## EF Core & Migrations

Migrations run on container startup via:

```csharp
db.Database.Migrate();
```

To apply manually:

```bash
cd backend
dotnet ef database update
```

---

## License

MIT (or your applicable license)