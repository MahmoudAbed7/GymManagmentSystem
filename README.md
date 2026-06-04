# 🏋️‍♂️ Gym Management System (.NET Desktop Application)

An enterprise-grade, desktop-based **Gym Management System** designed to streamline gym operations, member subscriptions, staff management, and payment tracking. Built using modern software engineering principles like **3-Tier Architecture (Layered Architecture)**, object-oriented design (OOP), and secure data handling.

---

## 🚀 Key Features

*   **Member Management:** Full CRUD operations for gym members, including personal details, medical notes, and status tracking.
*   **Subscription & Membership Plans:** Flexible creation of membership tiers (e.g., Monthly, Yearly, VIP) with automatic expiration and renewal tracking.
*   **Payment & Invoicing Ledger:** Secure logging of financial transactions, dynamic receipt generation, and payment history.
*   **Secure Authentication & Authorisation:** Multi-level user access control (Admin vs. Trainer/Staff) using hashed credentials.
*   **System Event Logging:** Built-in auditing mechanism that logs critical system events and errors directly into the Windows Event Viewer for debugging and security compliance.
*   **Dynamic UI with User Controls:** A clean, modern Dashboard layout utilising reusable Windows Forms `User Controls` for seamless navigation without flickering.

---

## 📐 Architecture & Design Patterns

The project strictly adheres to clean code practices and architectural separation of concerns:
*   **Presentation Layer:** Handles UI rendering, user input validation, and invokes the Business Layer.
*   **Business Logic Layer (BLL):** Bridges the UI and Data layers, implementing core business rules and data transformations.
*   **Data Access Layer (DAL):** Executes parameterized SQL queries via **ADO.NET** to ensure maximum performance and bulletproof protection against **SQL Injection**.

---

## 🛠️ Tech Stack & Tools

*   **Language:** C# (.NET Framework)
*   **Database:** Microsoft SQL Server (MS SQL)
*   **Data Access:** ADO.NET (With Parameterized Queries)
*   **Architecture:** 3-Tier Architecture / Object-Oriented Programming (OOP)
*   **Security:** Cryptographic Password Hashing & Windows Event Logger

---

## 📸 Database Design (ERD)

The database is fully normalized to eliminate redundancy. Core entities include:
*   `Members` ↔ `Subscriptions` ↔ `Payments`
*   `Users` (System Admins/Staff)

*(Tip: You can add an image link of your database diagram here!)*

---

## ⚙️ Setup & Installation

1.  **Clone the Repository:**
```bash
    git clone [https://github.com/MahmoudAbed7/GymManagmentSystem.git](https://github.com/MahmoudAbed7/GymManagmentSystem.git)
    ```
2.  **Database Configuration:**
    *   Import the SQL script into your **SQL Server Management Studio (SSMS)**.
    *   Update the connection string inside the `DataAccessLayer` or Global Classes configuration to match your local SQL Server instance.
3.  **Run the Application:**
    *   Open `GymManagmentSystem.sln` in **Visual Studio**.
    *   Restore any missing NuGet packages.
    *   Press `F5` to build and run the system.

---

## 📈 Future Roadmap (To-Do)
- [ ] Migrate the architecture from .NET Framework to **.NET 8/9 Modern Core**.
- [ ] Transition the Data Access Layer from ADO.NET to **Entity Framework Core (EF Core)** using Code-First migration.
- [ ] Convert the business logic into a scalable **ASP.NET Core Web API** to allow integration with mobile/web frontends.
- [ ] Upgrade password hashing to industry-standard **BCrypt**.

---

### 👨‍💻 Developed By
*   **Mahmoud Abed** - *Backend & .NET Developer*
*   (https://github.com/MahmoudAbed7)
