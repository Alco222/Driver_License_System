Driver License Management System (DVLD)
A comprehensive desktop application built to automate and manage the government driver licensing process, including local and international licenses, test management, and license detention.

🚀 Actual Features
N-Tier Architecture: Robust separation between Business Logic Layer (BLL) and Data Access Layer (DAL) for better maintainability.

Application Management: Handles various application types (New, Renew, Replace for Lost/Damaged).

Comprehensive Testing System: Full modules for managing Vision, Written, and Street Tests, including appointment scheduling and result recording.

Local & International Licenses: Ability to issue local driving licenses and process international license applications.

License Detention System: Modules to manage Detaining and Releasing licenses with fee management.

Advanced User & Person Management: Centralized system to manage people, drivers, and system users with secure login and password management.

Global Helpers: Specialized classes for formatting (clsFormat), validation (clsValidation), and security.

🛠 Technologies Used
Language: C# (Windows Forms).

Architecture: 3-Layer Architecture (Presentation, BLL, DAL).

Database: Microsoft SQL Server with ADO.NET for efficient data operations.

Tools: Visual Studio, Git/GitHub.

📂 Project Structure
DriverLicense: The Presentation Layer containing all UI Forms.

DriverLicenseBusinessLayer: Handles all business rules and logic.

DriverLicenseDataAccessLayer: Manages direct database communication and CRUD operations.

⚙️ How to Run
Clone the repository.

Execute the provided SQL Script in your SQL Server instance to create the database schema.

Update the connection string in DataAccessSettings.cs.

Open the .sln file in Visual Studio and run the project.
