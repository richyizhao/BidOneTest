# BidOneTest

This project implements the requested task:

- Frontend form with 2 simple text fields: `firstName` and `lastName`
- Backend receives the posted object
- Backend saves the posted object to a simple JSON file

## Tech Stack

- Frontend: Angular
- Backend: ASP.NET Core Web API
- Storage: JSON file at `server/Data/data.json`

## Run the Project

### Backend

From `server/`:

```powershell
dotnet restore
dotnet run --launch-profile https
```

### Frontend

From `client/`:

```powershell
npm install
ng serve -o
```

Frontend runs on `http://localhost:4200`.

Backend is configured for `https://localhost:7072`.

## API

Endpoint:

```text
POST /api/user
```

Example request:

```json
{
  "firstName": "John",
  "lastName": "Smith"
}
```

Saved output goes to:

```text
server/Data/data.json
```

## Notes

- The frontend includes simple input validation.
- The backend validates the posted names before saving.
