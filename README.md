# Documentation (For Personal Reference Only)

<img src="https://github.com/shubhmittal07/Orphanage-Management-System/blob/main/Screenshot%20(2).png?raw=true" alt="Login Page">

## What is happening in the project?


### **1. Application Initialization:**
- **First Page Rendered:** When the application starts, the **login page** is the first page rendered. This ensures that only authenticated users can access the system.

### **2. User Authentication:**
- **Login Process:**
  - **User/Admin enters credentials** on the login page.
  - **AuthService** sends a POST request to the `/api/login` endpoint with the email and password.
  - **Backend Validation:** The `AuthenticationController` validates the credentials using the `AuthService`.
  - **JWT Token Generation:** If valid, a JWT token is generated and returned to the client.
  - **Token Storage:** The client stores the JWT token in localStorage for subsequent requests.

### **3. Role-Based Navigation:**
- **Auth Guard Implementation:**
  - **Routing Protection:** The application uses Auth Guard to protect routes. If a user tries to access a protected route without being authenticated, they are redirected to the login page.
  - **Role Check:** Depending on the user role (Admin/User), different navigation options are available.

### **4. Admin Operations:**
- **Dashboard Access:** Admins can access the dashboard to manage orphanages, donations, and feedback.
- **CRUD Operations on Orphanages:**
  - **Add Orphanage:** Admins can add orphanage details via a form. The `OrphanageService` sends a POST request to `/api/orphanages`.
  - **View Orphanages:** Admins can view all orphanages. The `OrphanageService` sends a GET request to `/api/orphanages`.
  - **Edit/Delete Orphanage:** Admins can edit or delete orphanages. The `OrphanageService` sends PUT and DELETE requests to `/api/orphanages/{orphanageId}`.
- **View Donations and Feedback:**
  - **Donations:** Admins can view all donations made by users.
  - **Feedback:** Admins can view all feedback submitted by users.

### **5. User Operations:**
- **Dashboard Access:** Users can access their dashboard to view orphanages, make donations, and provide feedback.
- **View and Donate to Orphanages:**
  - **View Orphanages:** Users can view orphanages and their details. The `OrphanageService` sends a GET request to `/api/orphanages`.
  - **Donate:** Users can donate to orphanages. The `DonationService` sends a POST request to `/api/donations`.
- **View My Donations:** Users can view their donation history. The `DonationService` sends a GET request to `/api/donations/user/{userId}`.
- **Add and View Feedback:**
  - **Add Feedback:** Users can submit feedback. The `FeedbackService` sends a POST request to `/api/feedback`.
  - **View Feedback:** Users can view their submitted feedback. The `FeedbackService` sends a GET request to `/api/feedback/user/{userId}`.

### **6. Backend Structure:**
- **ApplicationDbContext:** Manages database interactions with DbSet properties for Users, Orphanages, Donations, and Feedback.
- **Model Classes:** Define the structure of data entities like User, Orphanage, Donation, and Feedback.
- **Service Classes:** Contain business logic for interacting with the database (e.g., `OrphanageService`, `DonationService`, `FeedbackService`, `AuthService`).
- **Controller Classes:** Handle HTTP requests and responses (e.g., `AuthenticationController`, `OrphanageController`, `DonationController`, `FeedbackController`).

### **7. Error Handling and Validation:**
- **Client-Side Validation:** Ensures user inputs are valid before sending requests to the server.
- **Server-Side Validation:** Validates data integrity and handles exceptions gracefully.
- **Custom Error Pages:** Provide user-friendly error messages for different HTTP status codes.

### **8. Security:**
- **JWT Authentication:** Ensures secure communication between client and server.
- **Role-Based Access Control:** Restricts access to certain endpoints based on user roles.

### **9. Logging Out:**
- **Logout Process:** When a user logs out, they are redirected to the login page, and the JWT token is removed from localStorage.
