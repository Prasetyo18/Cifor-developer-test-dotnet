# CIFOR Developer Test 
The app is configured to serve on `http://localhost:5000`.
Open `http://localhost:5000` in your browser. The static frontend is at `/index.html` (served automatically) and talks to the API endpoints:
- `GET /Get` - list employees
- `GET /Get/{employeeId}` - detail
- `GET /Get/employees/search?name=&department=` - search
- `POST /employees` - create
- `PUT /employees/{employeeId}` - update
- `DELETE /employees/{employeeId}` - delete
