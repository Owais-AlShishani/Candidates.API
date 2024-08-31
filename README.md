Candidates API

This is a .NET 8 Web API project for managing job candidate.

 Features

- REST API to create or update candidate information
- Uses a SQL database to store candidate details
- Caching implemented for email existence checks to improve performance
- Extensible architecture to support future migration to different storage solutions
- Unit tests included to ensure code quality

Assumptions:
The candidate's email is used as the unique identifier
If a candidate with the provided email exists, their record will be updated; otherwise, a new record will be created
The application uses an in-memory cache for checking email existence to improve performance

Improvements
Validation: Enhance validation to handle more complex scenarios using fluent validation specially for Email comaprison
Enhanced Caching: Implement distributed caching like Redis for scalability across multiple instances
Security Enhancements: Implement authentication and authorization to secure the API
Error Handling: Improve error handling and logging for better diagnostics and maintenance
In general, it's bad practice to expose the model for responses so it should add a new class library named as Contracts for mapping the objects for requests and responses using (Extention Methods) and mapping them inside the controller.

Task completed in 6 hours.
